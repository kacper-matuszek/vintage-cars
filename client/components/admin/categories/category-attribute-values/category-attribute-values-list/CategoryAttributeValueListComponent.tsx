import { Guid } from "guid-typescript";
import { useContext, useEffect, useRef, useState } from "react";
import LoadingContext from "../../../../../contexts/LoadingContext";
import NotificationContext from "../../../../../contexts/NotificationContext";
import CategoryAttributeValueMapper from "../../../../../core/mappers/category/CategoryAttributeValueMapper";
import PagedList from "../../../../../core/models/paged/PagedList";
import useAuhtorizedPagedList from "../../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import useSendSubmitWithNotification from "../../../../../hooks/fetch/SendSubmitHook";
import useLocale from "../../../../../hooks/utils/LocaleHook";
import ExtendedTable from "../../../../base/table-list/extended-table/ExtendedTableComponent";
import TableContent from "../../../../base/table-list/table-content/TableContentComponent";
import { HeadCell } from "../../../../base/table-list/table-head/HeadCell";
import CategoryAttributeValueDialogForm from "../category-attribute-values-form/CategoryAttributeValueDialogFormComponent";
import CategoryAttributeValue from "../../models/CategoryAttributeValue";
import CategoryAttributeValueView from "../../models/CategoryAttributeValueView";

interface ICategoryAttributeValueListProps {
    categoryId: Guid,
    categoryAttributeId: Guid,
    onListChanged?: (list: Array<CategoryAttributeValueView>) => void
}

const CategoryAttributeValueList = (props: ICategoryAttributeValueListProps) => {
    const loc = useLocale('common', ['admin','categories', 'category-attribute-values', 'list'])
    const headers: HeadCell<CategoryAttributeValueView>[] = [
        {id: 'name', label: loc.trans(['table', 'headers', 'name'])},
        {id: 'isPreSelected', label: loc.trans(['table', 'headers', 'isPreSelected'])},
        {id: 'displayOrder', label: loc.trans(['table', 'headers', 'displayOrder'])}
    ]

    const {categoryId, categoryAttributeId, onListChanged} = props;
    const categoryAttributeValueForm = useRef(null);
    const {showLoading, hideLoading} = useContext(LoadingContext);
    const notification = useContext(NotificationContext);
    const categoryAttributeValueMapper = new CategoryAttributeValueMapper();
    const [_, fetchCategoryAttributeValue, isLoading, readCategoryAttrValues, refresh] = useAuhtorizedPagedList<CategoryAttributeValueView>("/admin/v1/category/attribute-value/list");
    const [categoryAttributeValues, setCategoryAttributeValues] = useState(new PagedList<CategoryAttributeValueView>())
    const [sendDelete] = useSendSubmitWithNotification("/admin/v1/category/attribute-value/delete", showLoading, hideLoading, loc.trans(['delete', 'message', 'success']))
    
    const openForm = () => categoryAttributeValueForm.current.openForm();
    const handleEdit = (categoryAttributeValueView: CategoryAttributeValueView) => {
        categoryAttributeValueForm.current.editForm(categoryAttributeValueMapper.toDestination(categoryAttributeValueView));
    }
    const handleFormSubmit = (model: CategoryAttributeValue, isNew: boolean) => {
        if(model.isPreSelected && categoryAttributeValues.source.some(ca => ca.isPreSelected)) {
            model.isPreSelected = false;
            notification.showWarningMessage(loc.trans(['submit', 'message', 'warning']))
        }
        const categoryAttributeValueView = categoryAttributeValueMapper.toSource(model);
        categoryAttributeValueView.isNew = isNew;
        setCategoryAttributeValues(prevState => prevState.addOrUpdateAndGenerateRef(categoryAttributeValueView))
    }
    const handleDelete = async(ids: Guid[]) => {
        const toDelete = categoryAttributeValues.source.filter(x => ids.some(id => id === x.id));
        const arrayWithNewObjs = toDelete.filter(x => x.isNew);
        if(arrayWithNewObjs) {
            categoryAttributeValues.source = categoryAttributeValues.source.filter(x => !arrayWithNewObjs.some(newObj => newObj.id === x.id));
        }
        const newObjs = categoryAttributeValues.source.filter(x => x.isNew);
        (async () => toDelete.filter(x => !x.isNew).forEach(x => {
            (async () => {
                await sendDelete(x)
            })();
        }))().finally(() => {
            categoryAttributeValues.source = [];
            refresh();
            setCategoryAttributeValues(prevState => {
                const list = new PagedList<CategoryAttributeValueView>();
                list.source.push(...prevState.source);
                list.source.push(...newObjs);
                list.totalCount += newObjs.length;
                return list;
            })
        })
    }
    useEffect(() => {
        setCategoryAttributeValues(readCategoryAttrValues);
    }, [readCategoryAttrValues]);
    useEffect(() => {
        if(onListChanged)
            onListChanged(categoryAttributeValues.source);
    }, [categoryAttributeValues])
    return (
        <>
            <ExtendedTable<CategoryAttributeValueView>
                fetchData={(paged) => {
                    fetchCategoryAttributeValue(paged, {categoryId, categoryAttributeId})
                }}
                rows={categoryAttributeValues}
                onAddClick={openForm}
                onEditClick={handleEdit}
                onDeleteClick={handleDelete}
                title={loc.trans(['table', 'title'])}
                >
                {headers.map((obj, index) => {
                    return (
                        <TableContent key={index} name={obj.id} headerName={obj.label}/>
                    )
                })}
            </ExtendedTable>
            <CategoryAttributeValueDialogForm onSubmit={handleFormSubmit} ref={categoryAttributeValueForm}/>
        </>
    )
}
export default CategoryAttributeValueList;
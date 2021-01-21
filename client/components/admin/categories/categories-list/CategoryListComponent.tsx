import { Guid } from "guid-typescript";
import { useContext, useRef } from "react";
import LoadingContext from "../../../../contexts/LoadingContext";
import CategoryMapper from "../../../../core/mappers/category/CategoryMapper";
import useAuhtorizedPagedList from "../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import useSendSubmitWithNotification from "../../../../hooks/fetch/SendSubmitHook";
import useLocale from "../../../../hooks/utils/LocaleHook";
import ExtendedTable from "../../../base/table-list/extended-table/ExtendedTableComponent";
import TableContent from "../../../base/table-list/table-content/TableContentComponent";
import { HeadCell } from "../../../base/table-list/table-head/HeadCell";
import CategoryDialogForm from "../category-form/CategoryDialogFormComponent";
import CategoryView from "./models/CategoryView";

const CategoryList = () => {
    const loc = useLocale('common', ['admin', 'categories', 'categories-list']);
    const headers: HeadCell<CategoryView>[] = [
        {id: 'name', label: loc.trans(['table', 'headers', 'name'])},
        {id: 'description', label: loc.trans(['table', 'headers', 'description'])},
        {id: 'isPublished', label: loc.trans(['table', 'headers', 'published'])},
        {id: 'isArchival', label: loc.trans(['table', 'headers', 'archival'])}
    ]

    const categoryForm = useRef(null);
    const categoryMapper = new CategoryMapper();
    const {showLoading, hideLoading} = useContext(LoadingContext);
    const [fetchCategories, fetchCategoryWithParam, isLoading, categories, refresh] = useAuhtorizedPagedList<CategoryView>('/admin/v1/Category/list');
    const [sendDelete] = useSendSubmitWithNotification("/admin/v1/category/delete", showLoading, hideLoading, loc.trans(['delete', 'message', 'success']));

    const handleDelete = async (ids: Guid[]) => {
        (async () => ids.forEach(id => {
            (async () => {
                await sendDelete({id: id});
            })();
        }))().finally(() => {
            categories.source = [];
            refresh();
        });
    }
    const openCategoryForm = () => categoryForm.current.openForm();
    const handleEdit = (categoryView: CategoryView) => {
        const category = categoryMapper.toDestination(categoryView);
        categoryForm.current.openFormWithEditModel(category, categoryView.attributes, categoryView.isArchival);
    }
    return (
        <>
            <ExtendedTable<CategoryView>
                fetchData={fetchCategories}
                rows={categories}
                title={loc.trans(['table', 'title'])}
                onDeleteClick={(items) => handleDelete(items)}
                onAddClick={openCategoryForm}
                onEditClick={(model) => handleEdit(model)}
                showSelection={true}
            >
                {headers.map((obj, index) => {
                    return (
                        <TableContent key={index} name={obj.id} headerName={obj.label}/>
                    )
                })}
            </ExtendedTable>
            <CategoryDialogForm ref={categoryForm} onSubmit={refresh}/>
        </>
    )
}
export default CategoryList;
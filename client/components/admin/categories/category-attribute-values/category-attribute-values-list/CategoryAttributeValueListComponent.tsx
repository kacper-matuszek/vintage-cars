import { Guid } from "guid-typescript";
import { useRef } from "react";
import CategoryAttributeValueMapper from "../../../../../core/mappers/category/CategoryAttributeValueMapper";
import Paged from "../../../../../core/models/paged/Paged";
import useAuhtorizedPagedList from "../../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import ExtendedTable from "../../../../base/table-list/extended-table/ExtendedTableComponent";
import TableContent from "../../../../base/table-list/table-content/TableContentComponent";
import { HeadCell } from "../../../../base/table-list/table-head/HeadCell";
import CategoryAttributeValueDialogForm from "../category-attribute-values-form/CategoryAttributeValueDialogFormComponent";
import CategoryAttributeValue from "../models/CategoryAttributeValue";
import CategoryAttributeValueView from "../models/CategoryAttributeValueView";

interface ICategoryAttributeValueListProps {
    categoryId: Guid,
    categoryAttributeId: Guid
}
const headers: HeadCell<CategoryAttributeValueView>[] = [
    {id: 'name', label: 'Nazwa'},
    {id: 'isPreSelected', label: 'Domyślnie wybrany'},
    {id: 'displayOrder', label: 'Pozycja'}
]
const CategoryAttributeValueList = (props: ICategoryAttributeValueListProps) => {
    const {categoryId, categoryAttributeId} = props;
    const categoryAttributeValueForm = useRef(null);
    const categoryAttributeValueMapper = new CategoryAttributeValueMapper();
    const [_, fetchCategoryAttributeValue, isLoading, categoryAttributeValues] = useAuhtorizedPagedList<CategoryAttributeValueView>("/v1/category/attribute-value/list");

    const handleEdit = (categoryAttributeValueView: CategoryAttributeValueView) => {
        categoryAttributeValueForm.current.editForm(categoryAttributeValueMapper.toDestination(categoryAttributeValueView));
    }
    const handleFormSubmit = (model: CategoryAttributeValue) => {
        const categoryAttributeValueView = categoryAttributeValueMapper.toSource(model);
        categoryAttributeValues.source.push(categoryAttributeValueView);
    }
    return (
        <>
            <ExtendedTable<CategoryAttributeValueView>
                fetchData={(paged) => {
                    fetchCategoryAttributeValue(paged, {categoryId, categoryAttributeId})
                }}
                rows={categoryAttributeValues}
                onAddClick={categoryAttributeValueForm.current.openForm}
                onEditClick={handleEdit}
                title="Wartości"
                >
                {headers.map((obj, index) => {
                    return (
                        <TableContent key={index} name={obj.id} headerName={obj.label}/>
                    )
                })}
            </ExtendedTable>
            <CategoryAttributeValueDialogForm onSubmit={handleFormSubmit}/>
        </>
    )
}
export default CategoryAttributeValueList;
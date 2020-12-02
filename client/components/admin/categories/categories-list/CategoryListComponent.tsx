import useAuhtorizationPagedList from "../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import ExtendedTable from "../../../base/table-list/extended-table/ExtendedTableComponent";
import TableContent from "../../../base/table-list/table-content/TableContentComponent";
import { HeadCell } from "../../../base/table-list/table-head/HeadCell";
import CategoryView from "./models/CategoryView";

const headers: HeadCell<CategoryView>[] = [
    {id: 'name', label: 'nazwa'},
    {id: 'description', label: 'Opis'},
    {id: 'isPublished', label: 'Opublikowane'},
    {id: 'isArchival', label: 'Archiwalny'}
]

const CategoryList = () => {
    const [fetchCategories, isLoading, categories, refresh] = useAuhtorizationPagedList<CategoryView>('/v1/Category/list');
    return (
        <>
            <ExtendedTable<CategoryView>
                fetchData={fetchCategories}
                rows={categories}
                title="Kategorie"
            >
                {headers.map((obj, index) => {
                    return (
                        <TableContent key={index} name={obj.id} headerName={obj.label}/>
                    )
                })}
            </ExtendedTable>
        </>
    )
}
export default CategoryList;
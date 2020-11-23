import ExtendedTable from "../../../../base/table-list/extended-table/ExtendedTableComponent"
import CategoryAttributeView from "../models/CategoryAttributeView"
import TableContent from "../../../../base/table-list/table-content/TableContentComponent";
import { HeadCell } from "../../../../base/table-list/table-head/HeadCell";
import { Guid } from "guid-typescript";
import useAuhtorizationPagedList from "../../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";

const rows: Array<CategoryAttributeView> = [
    {id: Guid.create(), name: "tst", description: "desc"},
    {id: Guid.create(), name: "tst2", description: "desc2"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},

]
const headers: HeadCell<CategoryAttributeView>[] = [
    {id: 'name', label: 'nazwa'},
    {id: 'description', label: 'Opis'},
    
]
const CategoryAttributeList = () => {
    const [fetchCategoryAttributes, isLoading, categoryAttributes] = useAuhtorizationPagedList<CategoryAttributeView>('/v1/category/attribute/list');
    return (
        <ExtendedTable<CategoryAttributeView>
            fetchData={fetchCategoryAttributes}
            rows={categoryAttributes}
            title="Atrybuty Kategorii"
        >
            {headers.map((obj, index) => {
                return (
                    <TableContent key={index} name={obj.id} headerName={obj.label}/>
                )
            })}
        </ExtendedTable>
    )
}
export default CategoryAttributeList;
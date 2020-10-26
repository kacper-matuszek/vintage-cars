export default class PagedList<T> {
    constructor()
    {
        this.source = new Array<T>();
    }
    public source: Array<T>;
    public pageIndex: number;
    public pageSize: number;
    public totalCount: number;
    public totalPages: number;
    public hasPreviousPage: boolean;
    public hasNextPage: boolean;
}
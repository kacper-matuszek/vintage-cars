export default class PagedList<IModel> {
    constructor()
    {
        this.source = new Array<IModel>();
    }
    public source: Array<IModel>;
    public pageIndex: number;
    public pageSize: number;
    public totalCount: number;
    public totalPages: number;
    public hasPreviousPage: boolean;
    public hasNextPage: boolean;
}
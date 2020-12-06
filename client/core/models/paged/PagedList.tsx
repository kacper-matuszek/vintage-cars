export default class PagedList<IModel> {
    constructor()
    {
        this.source = new Array<IModel>();
        this.totalCount = 0;
        this.totalPages = 0;
    }
    public source: Array<IModel>;
    public pageIndex: number;
    public pageSize: number;
    public totalCount: number;
    public totalPages: number;
    public hasPreviousPage: boolean;
    public hasNextPage: boolean;
}
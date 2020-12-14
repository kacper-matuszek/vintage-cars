import {IModel} from "../base/IModel";

export default class PagedList<TModel extends IModel> {
    constructor()
    {
        this.source = new Array<TModel>();
        this.totalCount = 0;
        this.totalPages = 0;
    }
    public source: Array<TModel>;
    public pageIndex: number;
    public pageSize: number;
    public totalCount: number;
    public totalPages: number;
    public hasPreviousPage: boolean;
    public hasNextPage: boolean;

    addOrUpdateAndGenerateRef(model: TModel): PagedList<TModel> {
        const list = new PagedList<TModel>();
        list.source.push(...this.source);
        let isUpdate = false;
        if(list.source.some(x => x.id === model.id))
        {
            isUpdate = true;
            list.source = list.source.filter(x => x.id !== model.id);
            list.totalCount = this.totalCount;
            list.pageIndex = this.pageIndex;
            list.pageSize = this.pageSize;
            list.hasNextPage = this.hasNextPage;
            list.hasPreviousPage = this.hasPreviousPage;
        }
        list.source.push(model);
        if(!isUpdate)
            list.totalCount = this.totalCount + 1;
        return list;
    }
}
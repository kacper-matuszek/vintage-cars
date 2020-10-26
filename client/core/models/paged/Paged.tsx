export default class Paged {
    constructor(pageIndex: number, pageSize: number)
    {
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
    }
    pageIndex: number;
    pageSize: number;

    public increment(): void {
        this.pageIndex = this.pageIndex + 1;
    }
}
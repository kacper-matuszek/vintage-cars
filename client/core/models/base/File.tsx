export default class File {
    name: string;
    path: string;
    type: string;
    dataAsBase64: string;
    size: number;
    lastModifiedDate: Date;

    constructor(name: string, path: string, type: string, dataAsBase64: string, size: number, lastModifiedDate: Date) {
        this.name = name;
        this.path = path;
        this.type = type;
        this.dataAsBase64 = dataAsBase64;
        this.size = size;
        this.lastModifiedDate = lastModifiedDate;
    }
}
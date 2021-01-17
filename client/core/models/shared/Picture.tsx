import { Guid } from "guid-typescript";
import { IModel } from "../base/IModel";
import IRequestModel from "../base/IRequestModel";

export default class Picture implements IRequestModel {
    id: Guid | string;
    mimeType: string;
    altAttribute: string;
    titleAttribute: string;
    dataAsBase64: string;
    isMain: boolean;

    constructor(id: Guid | string, mimeType: string, altAttribute: string, titleAttribute: string, dataAsBase64: string, isMain: boolean = false) {
        this.id = id;
        this.mimeType = mimeType;
        this.altAttribute = altAttribute;
        this.titleAttribute = titleAttribute;
        this.dataAsBase64 = dataAsBase64;
        this.isMain = isMain;
    }
}
import { Guid } from "guid-typescript";
import { IModel } from "../base/IModel";
import IRequestModel from "../base/IRequestModel";

export default class Picture implements IRequestModel {
    id: Guid | string;
    mimeType: string;
    altAttribute: string;
    titleAttribute: string;

    constructor(id: Guid | string, mimeType: string, altAttribute: string, titleAttribute: string) {
        this.id = id;
        this.mimeType = mimeType;
        this.altAttribute = altAttribute;
        this.titleAttribute = titleAttribute;
    }
}
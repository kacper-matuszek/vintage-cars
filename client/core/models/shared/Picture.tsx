import { Guid } from "guid-typescript";
import { IModel } from "../base/IModel";

export default class Picture implements IModel {
    id: Guid;
    mimeType: string;
    altAttribute: string;
    titleAttribute: string;
}
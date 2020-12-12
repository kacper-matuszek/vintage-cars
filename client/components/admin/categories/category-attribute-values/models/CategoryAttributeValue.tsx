import { Guid } from "guid-typescript";
import { IModel } from "../../../../../core/models/base/IModel";

export default class CategoryAttributeValue implements IModel {
    id: Guid;
    name: string;
    isPreSelected: boolean;
    displayOrder: number;
}
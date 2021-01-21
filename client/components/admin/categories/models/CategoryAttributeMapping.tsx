import { Guid } from "guid-typescript";
import { IModel } from "../../../../../core/models/base/IModel";
import { AttributeControlType } from "../../../../../core/models/enums/AttributeControlType";

export default class CategoryAttributeMapping implements IModel {
    id: Guid;
    categoryAttributeId: Guid;
    attributeControlType: AttributeControlType
    displayOrder: number;
}
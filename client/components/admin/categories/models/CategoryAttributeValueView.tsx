import { Guid } from "guid-typescript";
import ISelectable from "../../../../core/models/base/ISelectable";

export default class CategoryAttributeValueView implements ISelectable {
    id: Guid;
    name: string;
    isPreSelected: boolean;
    displayOrder: number;
    isSelected: boolean;
    cantSelect: boolean;
    isNew: boolean;
}
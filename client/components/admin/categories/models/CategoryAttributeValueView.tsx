import { Guid } from "guid-typescript";
import { IExtSelectable } from "../../../../core/models/base/ISelectable";

export default class CategoryAttributeValueView implements IExtSelectable {
    id: Guid;
    name: string;
    isPreselected: boolean;
    displayOrder: number;
    isSelected: boolean;
    cantSelect: boolean;
    isNew: boolean;
}
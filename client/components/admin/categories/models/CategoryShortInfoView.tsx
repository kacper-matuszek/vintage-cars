import { Guid } from "guid-typescript";
import ISelectable from "../../../../core/models/base/ISelectable";

export default class CategoryShortInfoView implements ISelectable {
    id: Guid;
    name: string;
    isSelected: boolean;
    cantSelect: boolean = false;
}
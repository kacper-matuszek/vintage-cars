import { Guid } from "guid-typescript";

export default interface ISelectable {
    id: Guid;
    name: string;
    isSelected: boolean;
    cantSelect: boolean
}
export interface IExtSelectable extends ISelectable {
    isPreselected: boolean;
}
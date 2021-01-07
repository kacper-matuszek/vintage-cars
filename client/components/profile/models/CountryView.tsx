import { Guid } from "guid-typescript";
import ISelectable from "../../../core/models/base/ISelectable";

export default class CountryView implements ISelectable {
    id: Guid;
    name: string;
    isSelected: boolean;
    cantSelect;
}
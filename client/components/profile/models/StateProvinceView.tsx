import { Guid } from "guid-typescript";
import ISelectable from "../../../core/models/base/ISelectable";

export default class StateProvinceView implements ISelectable {
    id: Guid;
    name: string;
    cantSelect;
}
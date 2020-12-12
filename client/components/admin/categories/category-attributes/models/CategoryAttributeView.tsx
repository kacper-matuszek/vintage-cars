import { Guid } from "guid-typescript";
import ISelectable from "../../../../../core/models/base/ISelectable";

export default class CategoryAttributeView implements ISelectable {
    private _canSelect: boolean = true;
    
    id: Guid;
    name: string;
    description: string;
    get canSelect() {
        return this._canSelect === undefined ? true : this._canSelect;
    }
    set canSelect(value: boolean) {
        this._canSelect = value;
    }
}
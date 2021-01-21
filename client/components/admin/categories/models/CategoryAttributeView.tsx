import { Guid } from "guid-typescript";
import ISelectable from "../../../../../core/models/base/ISelectable";

export default class CategoryAttributeView implements ISelectable {
    private _canSelect: boolean = true;
    
    id: Guid;
    name: string;
    description: string;
    isSelected: boolean;
    get cantSelect() {
        return this._canSelect === undefined ? true : this._canSelect;
    }
    set cantSelect(value: boolean) {
        this._canSelect = value;
    }
}
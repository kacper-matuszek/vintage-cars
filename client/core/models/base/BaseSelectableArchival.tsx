import { Guid } from "guid-typescript";
import ISelectableArchival from "./ISelectableArchival";

export default abstract class BaseSelectableArchival implements ISelectableArchival {
    id: Guid;
    name: string;
    isArchival: boolean = false;
    get canSelect() {
        return this.isArchival;
    }
}
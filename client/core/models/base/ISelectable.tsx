import { Guid } from "guid-typescript";

export default interface ISelectable {
    id: Guid;
    name: string;
    cantSelect: boolean
}
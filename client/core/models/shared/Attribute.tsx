import { Guid } from "guid-typescript";
import { IModel } from "../base/IModel";

export default class Attribute implements IModel {
    id: Guid;
    name: string;
    value: string;
}
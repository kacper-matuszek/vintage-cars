import { Guid } from "guid-typescript";
import { IModel } from "../../../../../core/models/base/IModel";

export default class CategoryAttribute implements IModel {
    id: Guid;
    name: string;
    description: string
}
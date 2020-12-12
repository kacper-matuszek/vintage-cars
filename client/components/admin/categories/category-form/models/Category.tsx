import { Guid } from "guid-typescript";
import { IModel } from "../../../../../core/models/base/IModel";
import CategoryAttributeMapping from "./CategoryAttributeMapping";

export default class Category implements IModel {
    id: Guid;
    name: string;
    description: string;
    isPublished: boolean;
    attributeMappings: Array<CategoryAttributeMapping>;

    constructor() {
        this.attributeMappings = new Array<CategoryAttributeMapping>();
    }
}
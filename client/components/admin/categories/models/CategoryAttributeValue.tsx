import { Guid } from "guid-typescript";
import { IModel } from "../../../../../core/models/base/IModel";

export default class CategoryAttributeValue implements IModel {
    id: Guid;
    name: string;
    isPreSelected: boolean;
    displayOrder: number;

    constructor() {
        this.id = Guid.create();
        this.displayOrder = 0;
        this.isPreSelected = false;
        this.name = '';
    }
}
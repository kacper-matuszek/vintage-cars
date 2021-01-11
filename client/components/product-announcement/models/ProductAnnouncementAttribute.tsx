import { Guid } from "guid-typescript";
import { IModel } from "../../../core/models/base/IModel";

export default class ProductAnnouncementAttribute implements IModel {
    id: Guid;
    categoryAttributeId: Guid;
    categoryAttributeValueId: Guid;
    value: string;

    constructor(categoryAttributeId: Guid, value: string, categoryAttributeValueId: Guid) {
        this.categoryAttributeId = categoryAttributeId;
        this.value = value;
        this.categoryAttributeValueId = categoryAttributeValueId;
    }
}
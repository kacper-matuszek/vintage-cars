import { Guid } from "guid-typescript";
import CategoryAttributeValue from "../../category-attribute-values/models/CategoryAttributeValue";

export default class CategoryAttributeLinkAttributeValueSendData {
    categoryId: Guid;
    categoryAttributeValues: Array<CategoryAttributeValueChild>;
}

class CategoryAttributeValueChild extends CategoryAttributeValue {
    categoryAttributeId: Guid;
}
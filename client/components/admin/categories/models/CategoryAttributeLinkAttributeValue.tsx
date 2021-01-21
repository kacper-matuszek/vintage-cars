import { Guid } from "guid-typescript";
import CategoryAttributeValue from "./CategoryAttributeValue";

export default class CategoryAttributeLinkAttributeValueSendData {
    categoryId: Guid;
    categoryAttributeValues: Array<CategoryAttributeValueChild>;
}

class CategoryAttributeValueChild extends CategoryAttributeValue {
    categoryAttributeId: Guid;
}
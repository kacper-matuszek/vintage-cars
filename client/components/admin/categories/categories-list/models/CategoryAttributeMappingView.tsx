import { Guid } from "guid-typescript";
import { AttributeControlType } from "../../../../../core/models/enums/AttributeControlType";
import CategoryAttributeView from "../../category-attributes/models/CategoryAttributeView";

export default class CategoryAttributeMappingView extends CategoryAttributeView {
    public attributeControlType: AttributeControlType;
    public categoryAttributeId: Guid;
}
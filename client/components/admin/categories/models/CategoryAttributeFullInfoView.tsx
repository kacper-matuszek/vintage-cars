import { AttributeControlType } from "../../../../core/models/enums/AttributeControlType";
import CategoryAttributeValueView from "./CategoryAttributeValueView";
import CategoryAttributeView from "./CategoryAttributeView";

export default class CategoryAttributeFullInfoView extends CategoryAttributeView {
    attributeControlType: AttributeControlType;
    values: Array<CategoryAttributeValueView>;
}
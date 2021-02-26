import { Guid } from "guid-typescript";
import CategoryAttributeValue from "../../../components/admin/categories/models/CategoryAttributeValue";
import CategoryAttributeValueView from "../../../components/admin/categories/models/CategoryAttributeValueView";
import IMapper from "../../models/utils/IMapper";

export default class CategoryAttributeValueMapper implements IMapper<CategoryAttributeValueView, CategoryAttributeValue> {
    toDestination(source: CategoryAttributeValueView): CategoryAttributeValue {
        const categoryAttributeValue = new CategoryAttributeValue();
        categoryAttributeValue.id = source.id;
        categoryAttributeValue.name = source.name;
        categoryAttributeValue.isPreselected = source.isPreselected;
        categoryAttributeValue.displayOrder = source.displayOrder;
        return categoryAttributeValue;
    }

    toSource(destination: CategoryAttributeValue): CategoryAttributeValueView {
        const categoryAttributeValueView = new CategoryAttributeValueView();
        categoryAttributeValueView.id = destination.id;
        categoryAttributeValueView.name = destination.name;
        categoryAttributeValueView.isPreselected = destination.isPreselected;
        categoryAttributeValueView.displayOrder = destination.displayOrder;
        return categoryAttributeValueView;
    }
}
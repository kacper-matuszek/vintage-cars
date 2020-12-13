import CategoryAttributeValue from "../../../components/admin/categories/category-attribute-values/models/CategoryAttributeValue";
import CategoryAttributeValueView from "../../../components/admin/categories/category-attribute-values/models/CategoryAttributeValueView";
import IMapper from "../../models/utils/IMapper";

export default class CategoryAttributeValueMapper implements IMapper<CategoryAttributeValueView, CategoryAttributeValue> {
    toDestination(source: CategoryAttributeValueView): CategoryAttributeValue {
        const categoryAttributeValue = new CategoryAttributeValue();
        categoryAttributeValue.id = source.id;
        categoryAttributeValue.name = source.name;
        categoryAttributeValue.isPreSelected = source.isPreSelected;
        categoryAttributeValue.displayOrder = source.displayOrder;
        return categoryAttributeValue;
    }

    toSource(destination: CategoryAttributeValue): CategoryAttributeValueView {
        const categoryAttributeValueView = new CategoryAttributeValueView();
        categoryAttributeValueView.id = destination.id;
        categoryAttributeValueView.name = destination.name;
        categoryAttributeValueView.isPreSelected = destination.isPreSelected;
        categoryAttributeValueView.displayOrder = destination.displayOrder;
        return categoryAttributeValueView;
    }
}
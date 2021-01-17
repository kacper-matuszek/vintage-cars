import CategoryAttributeMappingView from "../../../components/admin/categories/categories-list/models/CategoryAttributeMappingView";
import CategoryView from "../../../components/admin/categories/categories-list/models/CategoryView";
import CategoryAttributeView from "../../../components/admin/categories/category-attributes/models/CategoryAttributeView";
import Category from "../../../components/admin/categories/category-form/models/Category";
import CategoryAttributeMapping from "../../../components/admin/categories/category-form/models/CategoryAttributeMapping";
import IMapper from "../../models/utils/IMapper";

export default class CategoryMapper implements IMapper<CategoryView, Category>{
    toDestination(source: CategoryView): Category {
        const category = new Category();
        category.id = source.id;
        category.name = source.name;
        category.description = source.description;
        category.isPublished = source.isPublished;
        return category;
    }
    toSource(destination: Category): CategoryView {
        const categoryView = new CategoryView();
        categoryView.id = destination.id;
        categoryView.name = destination.name;
        categoryView.description = destination.description;
        categoryView.isPublished = destination.isPublished;
        return categoryView;
    }
}
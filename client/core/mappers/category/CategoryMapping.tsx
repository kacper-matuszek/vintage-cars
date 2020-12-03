import CategoryView from "../../../components/admin/categories/categories-list/models/CategoryView";
import Category from "../../../components/admin/categories/category-form/models/Category";
import IMapper from "../../models/utils/IMapper";

export default class CategoryMapping implements IMapper<CategoryView, Category> {
    toDestination(source: CategoryView): Category {
        const category = new Category();
        category.id = source.id;
        category.name = source.name;
        category.description = source.description;
        return category;
    }
    toSource(destination: Category): CategoryView {
        const categoryView = new CategoryView();
        categoryView.id = destination.id;
        categoryView.name = destination.name;
        categoryView.description = destination.description;
        return categoryView;
    }
}
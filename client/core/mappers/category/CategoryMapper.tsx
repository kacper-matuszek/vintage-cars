import CategoryView from "../../../components/admin/categories/models/CategoryView";
import Category from "../../../components/admin/categories/models/Category";
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
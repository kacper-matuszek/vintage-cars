import { Guid } from "guid-typescript";
import ISelectableArchival from "../../../../../core/models/base/ISelectableArchival";
import CategoryAttributeMappingView from "./CategoryAttributeMappingView";

export default class CategoryView implements ISelectableArchival {
    id: Guid;
    name: string;
    description: string;
    isPublished: boolean;
    showOnHomePag: boolean;
    isArchival: boolean;
    isSelected: boolean;
    cantSelect: boolean;
    attributes: Array<CategoryAttributeMappingView>;
}
import { Guid } from "guid-typescript";
import BaseSelectableArchival from "../../../../../core/models/base/BaseSelectableArchival";
import ISelectableArchival from "../../../../../core/models/base/ISelectableArchival";
import CategoryAttributeMappingView from "./CategoryAttributeMappingView";

export default class CategoryView extends BaseSelectableArchival {
    id: Guid;
    name: string;
    description: string;
    isPublished: boolean;
    showOnHomePag: boolean;
    isArchival: boolean;
    attributes: Array<CategoryAttributeMappingView>;
}
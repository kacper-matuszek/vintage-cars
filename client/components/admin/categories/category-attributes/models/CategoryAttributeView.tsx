import { Guid } from "guid-typescript";
import ISelectable from "../../../../../core/models/base/ISelectable";

export default class CategoryAttributeView implements ISelectable {
    id: Guid;
    name: string;
    description: string;
}
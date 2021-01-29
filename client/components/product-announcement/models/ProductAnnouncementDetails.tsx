import Attribute from "../../../core/models/shared/Attribute";
import Picture from "../../../core/models/shared/Picture";

export default class ProductAnnouncementDetail {
    name: string;
    shortDescription: string;
    description: string;
    attributes: Array<Attribute>;
    pictures: Array<Picture>;
}
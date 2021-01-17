import Picture from "../../../core/models/shared/Picture";
import ProductAnnouncementAttribute from "./ProductAnnouncementAttribute";

export default class CreateProductAnnouncement {
    name: string;
    shortDescription: string;
    description: string;
    attributes: Array<ProductAnnouncementAttribute>;
    pictures: Array<Picture>;

    constructor() {
        this.attributes = new Array<ProductAnnouncementAttribute>();
        this.pictures = new Array<Picture>();
    }
}
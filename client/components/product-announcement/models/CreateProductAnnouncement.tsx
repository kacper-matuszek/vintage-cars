import PictureModel from "./PictureModel";
import ProductAnnouncementAttribute from "./ProductAnnouncementAttribute";

export default class CreateProductAnnouncement {
    name: string;
    shortDescription: string;
    description: string;
    attributes: Array<ProductAnnouncementAttribute>;
    pictures: Array<PictureModel>;
}
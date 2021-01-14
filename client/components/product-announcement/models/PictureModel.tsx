import Picture from "../../../core/models/shared/Picture"

export default class PictureModel {
    picture: Picture;
    dataAsBase64: string;

    constructor(picture: Picture, dataAsBase64: string) {
        this.picture = picture;
        this.dataAsBase64 = dataAsBase64;
    }
}
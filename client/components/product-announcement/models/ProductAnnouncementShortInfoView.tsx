import { Guid } from 'guid-typescript'
import { IModel } from '../../../core/models/base/IModel'
import Picture from '../../../core/models/shared/Picture'

export default class ProductAnnouncementShortInfoView implements IModel {
    id: Guid;
    name: string;
    shortDescription: string;
    mainPicture: Picture;
}
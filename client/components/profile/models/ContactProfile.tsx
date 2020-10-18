import { Guid } from "guid-typescript";
import UserRequest from "../../../core/models/base/BaseUserRequest";

export default class ContactProfile extends UserRequest {
    public id: Guid;
    public firstName: string;
    public lastName: string;
    public company: string;
    public countryId:  Guid;
    public stateProvinceId: Guid;
    public city: string;
    public address1: string;
    public zipPostalCode: string;
    public phoneNumber: string;
}
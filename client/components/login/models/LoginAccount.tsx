import isEmpty from "../../../core/models/utils/StringExtension";

export default class LoginAccount {
    constructor() {
        this.email = "";
        this.password = "";
    }
    public email: string;
    public password: string;
}
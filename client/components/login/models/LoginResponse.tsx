import { LoginResult } from "./enums/LoginResult";

export default class LoginResponse {
    public result: LoginResult;
    public token: string;
}
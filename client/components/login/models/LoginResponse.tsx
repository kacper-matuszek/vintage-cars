import { LoginResult } from "./enums/LoginResult";

export default class LoginResponse {
    public loginResult: LoginResult;
    public token: string;
}
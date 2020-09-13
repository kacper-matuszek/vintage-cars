export class RegisterAccount {
    constructor(){
        this.googleRecaptchaResponse = "";
        this.isEnabled = false;
        this.username = "";
        this.email = "";
        this.password = "";
        this.repeatedPassword = "";
    }
    public googleRecaptchaResponse: string;
    public isEnabled: boolean;
    public username: string;
    public email: string;
    public password: string;
    public repeatedPassword: string;
}
import isStringNullOrEmpty from "../../../../core/models/utils/StringExtension";

export default class RegisterValidator {
    constructor(){
    }
    public username: string;
    public email: string;
    public password: string;
    public repeatedPassword: string;
    public canContinue: boolean = true;

    public validateEmail(value: string): void {
        if(isStringNullOrEmpty(value))
        {
            this.email = "Email nie może być pusty.";
            this.canContinue = false;
            return;
        }

        if(!value.isEmail())
        {
            this.email = "Podaj poprawny adres e-mail.";
            this.canContinue = false;
            return;
        }
    }

    public validatePassword(value: string): void {
        if(isStringNullOrEmpty(value))
        {
            this.password = "Hasło nie może być puste.";
            this.canContinue = false;
            return;
        }

        if((value.length <= 7) || !(/[A-Z]/.test(value.toString())) || !(/[0-9]/.test(value.toString())) || !(/[ `!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?~]/.test(value.toString())))
        {
            this.password = "Hasło musi zawierać co najmniej 8 znaków, jedną wielką literę, jedną liczbę oraz jeden znak specjalny.";
            this.canContinue = false;
            return;
        }
    }

    public validatePasswords(password: string, repeatedPassword: string)
    {
        if(isStringNullOrEmpty(password) || isStringNullOrEmpty(repeatedPassword))
        {
            this.repeatedPassword = "Hasło nie może być puste.";
            this.canContinue = false;
            return;
        }

        if(password !== repeatedPassword)
        {
            this.repeatedPassword = "Wprowadź poprawne powtórzone hasło.";
            this.canContinue = false;
            return;
        }
    }

    public validateUserName(username: string)
    {
        if(isStringNullOrEmpty(username)) {
            this.username = "Login nie może być pusty.";
            this.canContinue = false;
            return;
        }
    }
}
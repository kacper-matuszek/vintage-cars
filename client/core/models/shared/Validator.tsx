import IDictionary from "../utils/IDictionary";

export class ValidatorManager {
    private _validators : IValidatorDictionary;
    get validators(): IValidatorDictionary {
        return this._validators;
    }

    public setValidators(validators: IValidatorDictionary) {
        this._validators = validators;
    }

    public isValid<T>(model: T): void {
        const propsNames = Object.getOwnPropertyNames(model);
        for(let key in this._validators) {
            const propName = propsNames.find(name => name == key);

            if(propName == undefined || propName == null || propName == "")
                continue;
            
            const value = model[propName];
            const validators = this._validators[key]
            for(let validator of validators) {

                switch(validator.type) {
                    case ValidatorType.NotEmpty:
                        if(value == null || value == undefined || value == ""){
                            validator.isValid = false;
                        }
                        break;
                    case ValidatorType.ZipCode:
                        if(!(/^(?:[0-9]{2}-[0-9]{3})$/.test(value ))) {
                            validator.isValid = false
                        }
                        break;
                }

                if(!validator.isValid)
                    continue;
            }
        }
    }

    public getMessageByKey(key: string): string {
        const validators = this._validators[key].filter(v => !v.isValid);
        let result: string = "";
        if(validators != null && validators != undefined && validators.length > 0)
            result = validators[0].message;
        return result;
    }

    public isAllValid(): boolean {
        if(this._validators == null || this._validators == undefined)
            return true;
        for(let key in this.validators) {
            const validators = this._validators[key].some(v => !v.isValid);

            if(validators)
                return false;
        }

        return true;
    }
}
export class Validator {
    public type: ValidatorType;
    public paramValue: any;
    public message: string;
    public isValid: boolean = true;
}

export enum ValidatorType {
    NotEmpty = 1,
    ZipCode = 2
}

export interface IValidatorDictionary extends IDictionary<Validator> {
}
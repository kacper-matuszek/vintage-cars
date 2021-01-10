export {}
declare global{
    interface String {
        isEmail(): boolean;
    }
}
String.prototype.isEmail = function(): boolean {
    const regexEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return regexEmail.test(this);
};
export function isStringNullOrEmpty(value: string): boolean {
    return value === undefined || value === null || value === ""
}
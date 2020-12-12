export default class UrlHelper {
    static generateParameters(url: string, parameters: any): string {
        const properties = Object.getOwnPropertyNames(parameters).map(name => `${name}=${parameters[name]}`);
        return `${url}?${properties.join("&")}`;
    }
}
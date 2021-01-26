import { TranslationQuery } from "next-translate";
import useTranslation from "next-translate/useTranslation";

interface ILocale {
    trans: (name: string | string[]) => string,
    transQuery: (name: string | string[], query: TranslationQuery) => string,
    transModel: <T>(propertyName: keyof T, name: string | string[]) => string
}

const useLocale = (namespace: string, sections: string[]): ILocale => {
    const { t } = useTranslation(namespace);

    const prepareTranslate = (name: string | string[]) => `${sections.join('.')}.${Array.isArray(name) ? name.join('.') : name}`;
    const translate = (name: string | string[]) => t(prepareTranslate(name));
    const translateQuery = (name: string | string[], query: TranslationQuery) => t(prepareTranslate(name), query);
    function translateModel<T>(propertyName: keyof T, name: string | string[]): string 
    {
        const property = propertyName as string;
        sections.push(property);
        const result = translate(name);
        sections.pop();

        return result;
    }
    
    return {
        trans: translate,
        transQuery: translateQuery,
        transModel: translateModel
    }
}
export default useLocale;
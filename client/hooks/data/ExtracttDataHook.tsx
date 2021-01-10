import { useState, useEffect, Dispatch, SetStateAction, useRef, useCallback, useMemo } from "react";
import { isStringNullOrEmpty } from "../../core/models/utils/StringExtension";

const useExtractData = <T extends object>(object: T): [injectData: (model: T) => void, filledValue: T, extractData: (fieldName: keyof T, derivedValue: any) => void, extractFromDerivedValue: (fieldName: keyof T, derivedValue: any) => void] => {
    const [value, setValue] = useState(object);

    const setValueBase = (fieldName: keyof T, derivedValue: any, isEvent: boolean = true) => {
        // if(isEmpty(fieldName))
        //     return;
        if(isEvent && (derivedValue === undefined || derivedValue.target === undefined))
            return;

        const newValue = chooseDerivedValue(derivedValue, isEvent);
        setValue(prevState => {
            const isNumeric = (typeof object[fieldName]) === "number";
            return {
                ...prevState,
                [fieldName]: isNumeric ? Number(newValue) : newValue
            }
        })
    }

    const chooseDerivedValue = (derivedValue: any, isEvent: boolean) => {
        if(isEvent) {
            derivedValue.persist();
            return derivedValue.target.value;
        }

        return derivedValue;
    }

    const setValueFromDerivedValue = (fieldName: keyof T, derivedValue: any) => setValueBase(fieldName, derivedValue, false);

    const setValueFromEvent = (fieldName: keyof T, derivedValue: any) => setValueBase(fieldName, derivedValue);

    const injectData = (model: T) => setValue(model);
    
    return [injectData, value, setValueFromEvent, setValueFromDerivedValue];
}
export default useExtractData;
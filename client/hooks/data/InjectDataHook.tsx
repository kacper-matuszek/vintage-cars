import { useState, useEffect, Dispatch, SetStateAction, useRef } from "react";
import isEmpty from "../../core/models/utils/StringExtension";

const useInjectData = <T extends object>(setState: Dispatch<SetStateAction<T>> ): [injectData: (fieldName: string, event: any) => void] => {
    const [value, setValue] = useState(null);
    const currentFieldName = useRef("")

    const setValueFromEvent = (fieldName: string, event: any) => {
        if(isEmpty(fieldName))
            return;
        currentFieldName.current = fieldName;
        setValue(event);
    }

    useEffect(() => {
        if(isEmpty(currentFieldName.current)) return;
        const newValue = value.target.value;
        setState(prevState => {
            return {
                ...prevState,
                [currentFieldName.current]: newValue
            }
        });
    }, [value])

    return [setValueFromEvent]
}
export default useInjectData;
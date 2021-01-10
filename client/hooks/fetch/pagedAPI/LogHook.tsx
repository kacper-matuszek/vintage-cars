import { useEffect, useState } from "react"
import { isStringNullOrEmpty } from "../../../core/models/utils/StringExtension";

const useLog = (): [(content: string) => void, string, boolean, (event?: React.SyntheticEvent, reason?: string) => void, () => void] => {
    const [isShow, setIsShow] = useState(false);
    const [message, setMessage] = useState('');

    const handleClose = (event, reason) => {
        if(reason === 'clickaway')
            return;
        setIsShow(false);
        onClosed();
    }
    const showMessage = (content: string) => setMessage(content);
    const onClosed = () => setMessage('');
    useEffect(() => {
        if(isStringNullOrEmpty(message)) return;
        setIsShow(true);
        
    }, [message]);

    return [showMessage, message, isShow, handleClose, onClosed]
}

export default useLog;
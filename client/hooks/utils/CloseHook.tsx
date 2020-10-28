import { Dispatch, SetStateAction, useState } from "react";

const useOpenClose = (): [isOpen: boolean, setOpen: Dispatch<SetStateAction<boolean>>, close: (event: any, reason: any) => void] => {
    const [open, setOpen] = useState(false);

    const handleClose = (event, reason) => {
        if(reason === 'clickaway') return;
        setOpen(false);
    }

    return [open, setOpen, handleClose]
}

export default useOpenClose;
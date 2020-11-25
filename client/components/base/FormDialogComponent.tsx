import { Button, Dialog, DialogActions, DialogContent, DialogContentText, Link, TextField } from "@material-ui/core";
import { forwardRef, ReactNode, useImperativeHandle, useState } from "react";
import DialogTitle from '@material-ui/core/DialogTitle';
import { BaseProps } from "../../core/models/base/BaseProps";

interface Props extends BaseProps {
    showLink?: boolean,
    caption: string,
    variantCaption?: 'text' | 'outlined' | 'contained',
    title: string,
    actions?: ReactNode,
    showCancel?: boolean,
    forceOpen?: boolean
}

const FormDialog = forwardRef(({children, showLink, caption, title, actions, showCancel = true, variantCaption = 'outlined', forceOpen = false}: Props,ref) => {
    const [open, setOpen] = useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    }

    useImperativeHandle(ref, () => ({
        closeForm() {
            handleClose();
        },
        openForm() {
            handleClickOpen();
        }
    }));

    return (
        <div>
            {showLink ? 
                <Link component="button" variant="body2" color="primary" onClick={handleClickOpen}>
                    {caption}
                </Link> :
                <Button variant={variantCaption} color="primary" onClick={handleClickOpen}>
                    {caption}
                </Button> }
            <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title" fullWidth={true}>
                <DialogTitle id="form-dialog-title">{title}</DialogTitle>
                <DialogContent>
                    {children}
                </DialogContent>
                <DialogActions>
                    {actions}
                    { showCancel ? <Button onClick={handleClose} color="primary" variant="outlined">
                        Anuluj
                    </Button> : ''}
                </DialogActions>
            </Dialog>
        </div>
    )
})
export default FormDialog;
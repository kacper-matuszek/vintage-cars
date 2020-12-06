import { Box, Button, Dialog, DialogActions, DialogContent, DialogContentText, IconButton, Link, TextField, Tooltip } from "@material-ui/core";
import { forwardRef, ReactNode, useImperativeHandle, useState } from "react";
import DialogTitle from '@material-ui/core/DialogTitle';
import { BaseProps } from "../../core/models/base/BaseProps";
import CallMadeIcon from '@material-ui/icons/CallMade';
import CallReceivedIcon from '@material-ui/icons/CallReceived';

interface Props extends BaseProps {
    showLink?: boolean,
    caption?: string,
    variantCaption?: 'text' | 'outlined' | 'contained',
    title: string,
    actions?: ReactNode,
    showCancel?: boolean,
    disableOpenButton?: boolean,
    fullScreen?: boolean,
    maxWidth?: 'lg' | 'md' | 'sm' | 'xl'| 'xs',
    onCancel?: () => void,
    showChangeScreen?: boolean
}

const FormDialog = forwardRef(({children, showLink, caption, title, actions, showCancel = true, variantCaption = 'outlined', disableOpenButton = false, 
    fullScreen = false, maxWidth = 'sm', onCancel, showChangeScreen}: Props,ref) => {
    const [open, setOpen] = useState(false);
    const [isFullScreen, setFullScreen] = useState(fullScreen);
    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        if(onCancel !== undefined)
            onCancel();
    }

    const handleFullScreen = (event) => {
        event.preventDefault();
        setFullScreen(!isFullScreen);
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
            {!disableOpenButton ? (showLink ? 
                <Link component="button" variant="body2" color="primary" onClick={handleClickOpen}>
                    {caption}
                </Link> :
                <Button variant={variantCaption} color="primary" onClick={handleClickOpen}>
                    {caption}
                </Button> ) : <></>}
            <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title" fullWidth={true} maxWidth={maxWidth} fullScreen={isFullScreen}>
                <DialogTitle id="form-dialog-title">
                    <Box display="flex">
                        <Box flexGrow={3}>
                            {title}
                        </Box>
                        {fullScreen ? <></> :
                        showChangeScreen ? 
                            <Box>
                                <Tooltip title={isFullScreen ? "Pomniejsz" : "Powiększ"}>
                                    <IconButton aria-label="maximize" onClick={handleFullScreen}>
                                        {isFullScreen ? <CallReceivedIcon color="primary" fontSize="small"/> 
                                        : <CallMadeIcon color="primary" fontSize="small"/>}
                                    </IconButton>
                                </Tooltip>
                            </Box> : <></>}
                    </Box>
                </DialogTitle>
                <DialogContent dividers>
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
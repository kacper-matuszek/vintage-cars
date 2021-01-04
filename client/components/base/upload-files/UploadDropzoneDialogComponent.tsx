import { useState } from "react"
import Button from "@material-ui/core/Button";
import useOpenClose from "../../../hooks/utils/CloseHook";
import { DropzoneDialog } from "material-ui-dropzone";

const UploadDropzoneDialog = (props) => {
    const [open, setOpen] = useState(false);
    const [files, setFiles] = useState([]);

    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const handleSave = (files) => setFiles(files);

    return (
        <>
            <Button variant="contained" onClick={handleOpen}/>
            <DropzoneDialog
                open={open}
                onSave={handleSave}
                acceptedFiles={["image/png"]}
                showPreviews={true}
                cancelButtonText={"anuluj"}
                submitButtonText={"zapisz"}
                maxFileSize={5000000}
                onClose={handleClose}
                />
        </>
    )
}

export default UploadDropzoneDialog;
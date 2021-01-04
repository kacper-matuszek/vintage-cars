import {DropzoneArea} from "material-ui-dropzone";
import { Dispatch, SetStateAction, useState } from "react";

interface ImagesDropzoneAreaProps {
    setFiles: Dispatch<SetStateAction<any[]>>
}

const ImagesDropzoneArea = (props: ImagesDropzoneAreaProps) => {
    const handleChange = (files) => {
        props.setFiles(files);
    }
    return (
        <DropzoneArea 
            onChange={handleChange}
            showFileNames
            showAlerts={false}
            dropzoneText={"Przeciągnij i upuść obrazek lub kliknij"}
            acceptedFiles={["image/png"]}
            showPreviewsInDropzone={true}/>
    )
}

export default ImagesDropzoneArea;
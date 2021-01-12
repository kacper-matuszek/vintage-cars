import {DropzoneArea} from "material-ui-dropzone";
import { Dispatch, SetStateAction, useState } from "react";
import File from "../../../core/models/base/File"

interface ImagesDropzoneAreaProps {
    setFiles: Dispatch<SetStateAction<File[]>>
}

const ImagesDropzoneArea = (props: ImagesDropzoneAreaProps) => {
    const onDropHandler = (files) => {
        let file = files[0];
        const reader = new FileReader();
        reader.onload = (event) => {
            const data = event.target.result as string;
            props.setFiles(prevState => {
                prevState.push(new File(file.name, file.path, file.type, data, file.size, new Date(file.lastModified)))
                return prevState;
            })
        };
      reader.readAsDataURL(file);
    }
    return (
        <DropzoneArea 
            onDrop={onDropHandler}
            showFileNames
            showAlerts={false}
            dropzoneText={"Przeciągnij i upuść obrazek lub kliknij"}
            acceptedFiles={["image/png"]}
            showPreviewsInDropzone={true}/>
    )
}

export default ImagesDropzoneArea;
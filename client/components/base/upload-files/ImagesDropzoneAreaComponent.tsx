import {DropzoneArea} from "material-ui-dropzone";
import { Dispatch, SetStateAction, useState } from "react";
import File from "../../../core/models/base/File"

interface ImagesDropzoneAreaProps {
    setFiles: Dispatch<SetStateAction<File[]>>,
    imageLimit?: number,
}

const ImagesDropzoneArea = (props: ImagesDropzoneAreaProps) => {
    const {setFiles, imageLimit } = props;

    const toBase64 = (data: string) => data.split(',')[1];
    const onDropHandler = (files) => {
        let promises = [];
        for(let file of files) {
            let filePromise = new Promise(resolve => {
                let reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => resolve(new File(file.name, file.path, file.type, toBase64(reader.result as string), file.size, new Date(file.lastModified)));
            });
            promises.push(filePromise);
        }
        Promise.all(promises).then(fileContents => {
            setFiles(prevState => {
                prevState.push(...fileContents);
                return prevState;
            })
        })
    }
    const onDeleteHandler = (file) => {
        const reader = new FileReader();
        reader.onload = (event) => {
            const data = event.target.result as string;
            setFiles(prevState => {
                return prevState.filter(x => x.dataAsBase64 !== toBase64(data));
            });
        };
        reader.readAsDataURL(file);
    }
    return (
        <DropzoneArea 
            onDrop={onDropHandler}
            onDelete={onDeleteHandler}
            filesLimit={imageLimit}
            showFileNames
            showAlerts={false}
            dropzoneText={"Przeciągnij i upuść obrazek lub kliknij"}
            acceptedFiles={["image/png"]}
            showPreviewsInDropzone={true}/>
    )
}

export default ImagesDropzoneArea;
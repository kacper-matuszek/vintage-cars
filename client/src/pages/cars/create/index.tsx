import { useState } from "react";
import ImagesDropzoneArea from "../../../../components/base/upload-files/ImagesDropzoneAreaComponent";

const CreateCar = () => {
    const [files, setFiles] = useState([]);

    return (
        <>
            <ImagesDropzoneArea setFiles={setFiles}/>
        </>
    )
}

export async function getStaticProps() {
    return {
        props: {
            title: "Tworzenie samochodu"
        }
    }
}

export default CreateCar;
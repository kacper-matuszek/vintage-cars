import { Box, Button } from "@material-ui/core";
import { useRef } from "react";
import CategoryAttributeList from "../../../../../components/admin/categories/category-attributes/category-attributes-list/CategoryAttributeList";
import FormDialog from "../../../../../components/base/FormDialogComponent";

const Attributes = (props) => {
    return (
        <CategoryAttributeList/>
    )
}
export async function getStaticProps() {
    return {
        props: {
            title: "Atrybuty Kategorii",
        }
    }
}
export default Attributes;
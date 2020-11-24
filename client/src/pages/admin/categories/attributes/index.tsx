import { Box, Button } from "@material-ui/core";
import CategoryAttributeList from "../../../../../components/admin/categories/category-attributes/category-attributes-list/CategoryAttributeList";
import FormDialog from "../../../../../components/base/FormDialogComponent";

const Attributes = (props) => {
    const addActions = () => {
        return (
            <Button variant="contained" color="primary">
                Zapisz
            </Button>
        )
    }
    return (
        <>
            <Box display="flex" flexDirection="row-reverse" p={1} m={1}>
                <FormDialog
                    showLink={false}
                    caption="Dodaj atrybut"
                    title="Dodawanie atrybutu"
                    showCancel={true}
                    variantCaption="contained"
                    actions={addActions()}>
                        <div>test</div>
                </FormDialog>
            </Box>
            <CategoryAttributeList/>
        </>
    )
}
export default Attributes;
import { Button, TextField } from "@material-ui/core"
import { forwardRef, useImperativeHandle, useRef, useState } from "react";
import isEmpty from "../../../../core/models/utils/StringExtension";
import useExtractData from "../../../../hooks/data/ExtracttDataHook";
import useAuhtorizationPagedList from "../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import FormDialog from "../../../base/FormDialogComponent"
import ExtendedTable from "../../../base/table-list/extended-table/ExtendedTableComponent";
import { ValidatorManage, ValidatorType } from "../../../login/models/validators/Validator";
import CategoryAttributeView from "../category-attributes/models/CategoryAttributeView";
import Category from "./models/Category";

const CategoryDialogForm = forwardRef((props, ref) => {
    const formDialogRef = useRef(null);
    const modelValidator = new ValidatorManage();
    modelValidator.setValidators({
        ["name"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Nazwa jest wymagana.",
            isValid: true
        }]
    });
    const [modelErrors, setModelErrors]  = useState({
        name: ""
    });

    const [injectData, model, extractData] = useExtractData<Category>(new Category())
    const [fetchCategoryAttributes, isLoading, categoryAttributes, refresh] = useAuhtorizationPagedList<CategoryAttributeView>('/v1/category/attribute/list');

    const addActions = () => {
        return (
            <Button variant="contained" color="primary" type="submit" onClick={handleSubmit}>
                Zapisz
            </Button>
        )
    }
    const newModelForm = () => {
        if(model.id  !== undefined)
            injectData(new Category());
        formDialogRef.current.openForm();
    }
    const handleSubmit = async (event) => {
        //TODO
    }

    useImperativeHandle(ref, () => ({
        openForm() {
            newModelForm();
        },
        openFormWithEditModel(model: Category) {
            injectData(model);
            formDialogRef.current.openForm();
        }
    }));

    return (
       <>
            <FormDialog
                title="Dodawanie kategorii"
                showCancel={true}
                showLink={false}
                actions={addActions()}
                ref={formDialogRef}
            >
                <form noValidate>
                <TextField
                    InputLabelProps={{shrink: !isEmpty(model?.name)}}
                    error={!!modelErrors.name}
                    helperText={modelErrors.name}
                    value={model?.name}
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="name"
                    label="Nazwa"
                    name="name"
                    autoComplete="name"
                    onChange={(name)  => extractData("name", name)}/>
                <TextField
                    InputLabelProps={{shrink: !isEmpty(model?.description)}}
                    value={model?.description}
                    variant="outlined"
                    margin="normal"
                    fullWidth
                    id="description"
                    label="Opis"
                    name="description"
                    autoComplete="description"
                    onChange={(description) => extractData("description", description)}/>
            </form>
            </FormDialog>
            <ExtendedTable<CategoryAttributeView>
                fetchData={fetchCategoryAttributes}
                rows={categoryAttributes}
                title="Atrybuty Kategorii"
                >
            </ExtendedTable>
        </>
    )
})

export default CategoryDialogForm
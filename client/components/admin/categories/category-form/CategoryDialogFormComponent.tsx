import { Box, Button, Checkbox, Divider, FormControlLabel, TextField } from "@material-ui/core"
import { Label } from "@material-ui/icons";
import { forwardRef, useImperativeHandle, useRef, useState } from "react";
import PagedList from "../../../../core/models/paged/PagedList";
import isEmpty from "../../../../core/models/utils/StringExtension";
import useExtractData from "../../../../hooks/data/ExtracttDataHook";
import useAuhtorizationPagedList from "../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import FormDialog from "../../../base/FormDialogComponent"
import ExtendedTable from "../../../base/table-list/extended-table/ExtendedTableComponent";
import TableContent from "../../../base/table-list/table-content/TableContentComponent";
import { HeadCell } from "../../../base/table-list/table-head/HeadCell";
import { ValidatorManage, ValidatorType } from "../../../login/models/validators/Validator";
import CategoryAttributeMappingView from "../categories-list/models/CategoryAttributeMappingView";
import CategoryAttributeView from "../category-attributes/models/CategoryAttributeView";
import useStyles from "./category-dialog-style";
import CategoryLinkAttribute from "./CategoryLinkAttributeFormComponent";
import Category from "./models/Category";

const categoryAttributeHeaders: HeadCell<CategoryAttributeMappingView>[] = [
    {id: 'name', label: 'nazwa'},
    {id: 'description', label: 'Opis'},
    {id: 'attributeControlType', label: 'Kontrolka'}
]
const CategoryDialogForm = forwardRef((props, ref) => {
    const classes = useStyles();
    const formDialogRef = useRef(null);
    const [categoryAttributeMapping, setCategoryAttributeMapping]= useState(new PagedList<CategoryAttributeMappingView>())
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
                disableOpenButton={true}
                actions={addActions()}
                onCancel={() => setCategoryAttributeMapping(null)}
                ref={formDialogRef}
                maxWidth="md"
            >
                <form noValidate>
                <Box display="flex" flexDirection="row">
                    <Box flexGrow={3}>
                        <TextField 
                            className={classes.nameField}
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
                            onChange={(name)  => extractData("name", name)}/>
                    </Box>
                    <FormControlLabel
                        control={
                            <Checkbox/>
                        }
                        label="Opublikowane"
                    />
                </Box>
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
            <Box className={classes.linkAttribute}>
                <CategoryLinkAttribute
                    onSubmit={(attrMapping) => 
                        setCategoryAttributeMapping(prevState => {
                            const list = new PagedList<CategoryAttributeMappingView>();
                            if(prevState.source !== undefined)
                                list.source.push(...prevState.source);
                            list.source.push(attrMapping);
                            list.totalCount = prevState.totalCount + 1;
                            return list;
                        })
                    }
                />
            </Box>
            <ExtendedTable<CategoryAttributeMappingView>
                rows={categoryAttributeMapping}
                title="Atrybuty Kategorii"
                >
                    {categoryAttributeHeaders.map((obj, index) => {
                        return (
                            <TableContent key={index} name={obj.id} headerName={obj.label}/>
                        )
                    })}
            </ExtendedTable>
            </FormDialog>
        </>
    )
})

export default CategoryDialogForm
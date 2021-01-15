import { Box, Button, Checkbox, Divider, FormControlLabel, TextField } from "@material-ui/core"
import { Label } from "@material-ui/icons";
import { forwardRef, useContext, useImperativeHandle, useRef, useState } from "react";
import LoadingContext from "../../../../contexts/LoadingContext";
import NotificationContext from "../../../../contexts/NotificationContext";
import { AttributeControlType } from "../../../../core/models/enums/AttributeControlType";
import PagedList from "../../../../core/models/paged/PagedList";
import { isStringNullOrEmpty } from "../../../../core/models/utils/StringExtension";
import useExtractData from "../../../../hooks/data/ExtracttDataHook";
import useAuhtorizedPagedList from "../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import useSendSubmitWithNotification from "../../../../hooks/fetch/SendSubmitHook";
import SaveButton from "../../../base/controls/SaveButtonComponent";
import FormDialog from "../../../base/FormDialogComponent"
import ExtendedTable from "../../../base/table-list/extended-table/ExtendedTableComponent";
import TableContent from "../../../base/table-list/table-content/TableContentComponent";
import { ValidatorManager, ValidatorType } from "../../../../core/models/shared/Validator";
import CategoryAttributeMappingView from "../categories-list/models/CategoryAttributeMappingView";
import CategoryAttributeLinkAttributeValue from "../category-attributes/category-attributes-form/CategoryAttributeLinkAttributeValueComponent";
import useStyles from "./category-dialog-style";
import CategoryLinkAttribute from "./CategoryLinkAttributeFormComponent";
import Category from "./models/Category";
import CategoryAttributeMapping from "./models/CategoryAttributeMapping";

interface CategoryDialogProps {
    onSubmit?: () => void,
}
const CategoryDialogForm = forwardRef((props: CategoryDialogProps, ref) => {
    const classes = useStyles();
    const notification = useContext(NotificationContext);
    const [isReadonly, setIsReadonly] = useState(false);
    const [isEdit, setIsEdit] = useState(false);
    const formDialogRef = useRef(null);
    const [categoryAttributeMapping, setCategoryAttributeMapping]= useState(new PagedList<CategoryAttributeMappingView>());
    const modelValidator = new ValidatorManager();
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

    const [send] = useSendSubmitWithNotification("/admin/v1/category");
    const [injectData, model, extractData, extractDataFromDerivedValue] = useExtractData<Category>(new Category())
    const addActions = () => <SaveButton onSubmit={handleSubmit} disabled={isReadonly}/>  
    const newModelForm = () => {
        if(model.id  !== undefined)
            injectData(new Category());
        formDialogRef.current.openForm();
    }
    const handleSubmit = async () => {
        categoryAttributeMapping.source.forEach((cam, index) => {
            const categoryAttributeMapp = new CategoryAttributeMapping();
            categoryAttributeMapp.id = cam.id;
            categoryAttributeMapp.categoryAttributeId = cam.categoryAttributeId;
            categoryAttributeMapp.attributeControlType = cam.attributeControlType;
            categoryAttributeMapp.displayOrder = index + 1;
            model.attributeMappings.push(categoryAttributeMapp);
        })
        modelValidator.isValid(model);
        setModelErrors({...modelErrors, name: modelValidator.getMessageByKey("name")});
        if(modelValidator.isAllValid()){
            await send(model).finally(() => 
            {
                injectData(new Category());
                if(props.onSubmit !== undefined && props.onSubmit !== null)
                    props.onSubmit();
                formDialogRef.current.closeForm();
            });
        }
    }

    useImperativeHandle(ref, () => ({
        openForm() {
            newModelForm();
        },
        openFormWithEditModel(model: Category, attributeMapping: Array<CategoryAttributeMappingView>, isPreview: boolean = false) {
            injectData(model);
            setIsReadonly(isPreview);
            const pagedAttributeMappings = new PagedList<CategoryAttributeMappingView>();
            pagedAttributeMappings.source = attributeMapping;
            pagedAttributeMappings.totalCount = attributeMapping.length;
            pagedAttributeMappings.source.forEach(x => x.cantSelect = true);
            setCategoryAttributeMapping(pagedAttributeMappings);
            setIsEdit(true);
            formDialogRef.current.openForm();
        },
    }));

    return (
       <>
            <FormDialog
                title={`${isEdit ? "Edycja" : "Dodawanie"} kategorii`}
                showChangeScreen
                showCancel={true}
                showLink={false}
                disableOpenButton={true}
                actions={addActions()}
                onCancel={() => setCategoryAttributeMapping(new PagedList<CategoryAttributeMappingView>())}
                ref={formDialogRef}
                maxWidth="md"
            >
                <form noValidate>
                <Box sx={{display:  'flex'}}>
                    <Box sx={{flexGrow: 3}}>
                        <TextField 
                            className={classes.nameField}
                            InputLabelProps={{shrink: !isStringNullOrEmpty(model?.name)}}
                            error={!!modelErrors.name}
                            helperText={modelErrors.name}
                            disabled={isReadonly}
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
                        disabled={isReadonly}
                        control={
                            <Checkbox
                             checked={model?.isPublished}
                             onChange={() => extractDataFromDerivedValue("isPublished", !model?.isPublished)}/>
                        }
                        label="Opublikowane"
                    />
                </Box>
                <TextField
                    InputLabelProps={{shrink: !isStringNullOrEmpty(model?.description)}}
                    value={model?.description}
                    disabled={isReadonly}
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
                            const attrIsExist = prevState.source.some(x => x.categoryAttributeId === attrMapping.categoryAttributeId);
                            if(attrIsExist) {
                                notification.showWarningMessage("Dany atrybut już istnieje. Jeśli chcesz go zastąpić usuń aktualny.");
                                return prevState;
                            }
                            const list = new PagedList<CategoryAttributeMappingView>();
                            if(prevState.source !== undefined)
                                list.source.push(...prevState.source);
                            list.source.push(attrMapping);
                            list.totalCount = prevState.totalCount + 1;
                            return list;
                        })
                    }
                    disabled={isReadonly}
                />
            </Box>
            <ExtendedTable<CategoryAttributeMappingView>
                rows={categoryAttributeMapping}
                onDeleteClick={(items) => {
                    setCategoryAttributeMapping(prevState => {
                        prevState.source = prevState.source.filter(i => !items.includes(i.id));
                        prevState.totalCount -= items.length;
                        return prevState; 
                    })
                }}
                title="Atrybuty Kategorii"
                >
                    <TableContent key="category-attribute-mapping-name" name="name" headerName="Nazwa"/>
                    <TableContent key="category-attribute-mapping-description" name="description" headerName="Opis"/>
                    <TableContent 
                        key="category-attribute-mapping-attributeControl" name="attributeControlType"
                        headerName="Kontrolka"
                        content={(model) => {
                            const isValue = parseInt(model.attributeControlType, 10) >= 0
                            return isValue ?  (<>{AttributeControlType[model.attributeControlType]}</>) : (<>{model.attributeControlType}</>)
                        }}/>
                    {isEdit ? <TableContent 
                        key="link-attribute-value" 
                        name="linkAttributeValue" 
                        headerName=""
                        content={(rowModel) => {
                            return rowModel.attributeControlType != AttributeControlType.TextBox && !isStringNullOrEmpty(rowModel.id) ? <CategoryAttributeLinkAttributeValue
                                        categoryAttributeId={rowModel.categoryAttributeId}
                                        categoryId={model.id}
                                        categoryAttributeName={rowModel.name}
                                    /> : <></>
                        }}/> : <></>}
            </ExtendedTable>
            </FormDialog>
        </>
    )
})

export default CategoryDialogForm
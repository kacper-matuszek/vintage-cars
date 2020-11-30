import ExtendedTable from "../../../../base/table-list/extended-table/ExtendedTableComponent"
import CategoryAttributeView from "../models/CategoryAttributeView"
import TableContent from "../../../../base/table-list/table-content/TableContentComponent";
import { HeadCell } from "../../../../base/table-list/table-head/HeadCell";
import { Guid } from "guid-typescript";
import useAuhtorizationPagedList from "../../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import FormDialog from "../../../../base/FormDialogComponent";
import { Button, TextField } from "@material-ui/core";
import { useContext, useRef, useState } from "react";
import useExtractData from "../../../../../hooks/data/ExtracttDataHook";
import CategoryAttribute from "../models/CategoryAttribute";
import { ValidatorManage, ValidatorType } from "../../../../login/models/validators/Validator";
import isEmpty from "../../../../../core/models/utils/StringExtension";
import useSendSubmitWithNotification from "../../../../../hooks/fetch/SendSubmitHook";
import LoadingContext from "../../../../../contexts/LoadingContext";

const rows: Array<CategoryAttributeView> = [
    {id: Guid.create(), name: "tst", description: "desc"},
    {id: Guid.create(), name: "tst2", description: "desc2"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},
    {id: Guid.create(), name: "tst3", description: "desc3"},

]
const headers: HeadCell<CategoryAttributeView>[] = [
    {id: 'name', label: 'nazwa'},
    {id: 'description', label: 'Opis'},
]
const CategoryAttributeList = () => {
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
    const [modelErrors, setModelErrors] = useState({
        name: ""
    });

    const {showLoading, hideLoading} = useContext(LoadingContext);
    const [fetchCategoryAttributes, isLoading, categoryAttributes] = useAuhtorizationPagedList<CategoryAttributeView>('/v1/category/attribute/list');
    const [send] = useSendSubmitWithNotification("/v1/category/attribute", showLoading, hideLoading);
    const [sendDelete] = useSendSubmitWithNotification("/v1/category/attribute/delete", showLoading, hideLoading, "Usunięto pomyślnie.");
    const [injectData, model, extractData]  = useExtractData<CategoryAttribute>(new CategoryAttribute());
    
    const addActions = () => {
        return (
            <Button variant="contained" color="primary" type="submit" onClick={handleSubmit}>
                Zapisz
            </Button>
        )
    }
    const openForm = () => {
        if(model.id !== undefined)
            injectData(new CategoryAttribute());
         formDialogRef.current.openForm();
    }
    const handleSubmit = async (event) => {
        event.preventDefault();
        modelValidator.isValid(model);
        setModelErrors({...modelErrors, name: modelValidator.getMessageByKey("name")});
        if(modelValidator.isAllValid()) {
            await send(model);
            injectData(new CategoryAttribute());
            formDialogRef.current.closeForm();
        }
    }
    const handleEdit = (categoryAttribute: CategoryAttributeView) => {
        injectData(categoryAttribute)
        formDialogRef.current.openForm();
    }
    const handleDelete = async (ids: Guid[]) => {
        ids.forEach(id => {
            (async () => {
                await sendDelete({id: id})
            })()
        });
    }
    return (
        <>
        <ExtendedTable<CategoryAttributeView>
            fetchData={fetchCategoryAttributes}
            rows={categoryAttributes}
            title="Atrybuty Kategorii"
            onDeleteClick={(items) => handleDelete(items)}
            onEditClick={(obj) => handleEdit(obj)}
            onAddClick={openForm}
        >
            {headers.map((obj, index) => {
                return (
                    <TableContent key={index} name={obj.id} headerName={obj.label}/>
                )
            })}
        </ExtendedTable>
        <FormDialog
            showLink={false}
            showCancel={true}
            disableOpenButton={true}
            title="Dodawanie atrybutu"
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
        </>
    )
}
export default CategoryAttributeList;
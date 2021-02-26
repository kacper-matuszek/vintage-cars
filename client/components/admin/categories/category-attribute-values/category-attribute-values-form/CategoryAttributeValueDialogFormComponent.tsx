import { Box, Checkbox, FormControlLabel, Input, TextField, Tooltip } from "@material-ui/core"
import { forwardRef, useState, useRef, useImperativeHandle } from "react"
import { isEmpty } from "../../../../../core/models/utils/ObjectExtension"
import { isStringNullOrEmpty } from "../../../../../core/models/utils/StringExtension"
import useExtractData from "../../../../../hooks/data/ExtracttDataHook"
import SubmitDialogForm from "../../../../base/form-dialog/SubmitDialogFormComponent"
import { ValidatorManager, ValidatorType } from "../../../../../core/models/shared/Validator"
import CategoryAttributeValue from "../../models/CategoryAttributeValue"
import useLocale from "../../../../../hooks/utils/LocaleHook"

interface ICategoryAttributeValueDialogFormProps {
    onSubmit: (model: CategoryAttributeValue, isNew: boolean) => void
}
const CategoryAttributeValueDialogForm = forwardRef((props: ICategoryAttributeValueDialogFormProps, ref) => {
    const formDialog = useRef(null);
    const loc = useLocale('common', ['admin','categories', 'category-attribute-values', 'form']);
    const [isEdit, setIsEdit] = useState(false);
    const [injectData, model, extractData, extractFromDerivedValue] = useExtractData<CategoryAttributeValue>(new CategoryAttributeValue());
    const [errors, setErrors] = useState({
        name: ""
    });
    const categoryAttributeValueValidator = new ValidatorManager();
    categoryAttributeValueValidator.setValidators({
        ["name"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: loc.transModel<CategoryAttributeValue>("name", ['validators', 'not-empty']),
            isValid: true
        }]
    });

    const handleSubmit = async () => {
        categoryAttributeValueValidator.isValid(model);
        setErrors({...errors, name: categoryAttributeValueValidator.getMessageByKey("name")});
        if(categoryAttributeValueValidator.isAllValid()) {
            props.onSubmit(model, !isEdit);
            formDialog.current.closeForm();
            injectData(new CategoryAttributeValue());
            setErrors({name: ""});
        }
    }
    const handleCancel = () => {
        injectData(new CategoryAttributeValue());
        setErrors(prevState => prevState.name = "");
    }
    useImperativeHandle(ref, () => ({
        openForm() {
            injectData(new CategoryAttributeValue());
            formDialog.current.openForm();
        },
        editForm(model: CategoryAttributeValue) {
            setIsEdit(true);
            injectData(model);
            formDialog.current.openForm();
        }
    })) 
    return (
        <SubmitDialogForm
            title={loc.transQuery(['title', 'name'], {mode: loc.trans(isEdit ? ['title', 'edit'] : ['title', 'create'])})}
            handleSubmit={handleSubmit}
            onCancel={handleCancel}
            disableOpenButton={true}
            ref={formDialog}
            >
            <form noValidate>
                <TextField
                    InputLabelProps={{shrink: !isStringNullOrEmpty(model?.name)}}
                    error={!!errors.name}
                    helperText={errors.name}
                    value={model?.name}
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="name"
                    label={loc.transModel<CategoryAttributeValue>("name", 'label')}
                    name="name"
                    onChange={(name)  => extractData("name", name)}/>
                <Box sx={{display: 'flex', alignItems: 'center'}}>
                    <Box sx={{flexGrow: 3}}>
                        <Tooltip title={loc.transModel<CategoryAttributeValue>("isPreselected", 'tooltip')}>
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={isEmpty(model?.isPreselected) ? false : model.isPreselected}
                                        onChange={() => extractFromDerivedValue("isPreselected", !model?.isPreselected)}
                                    />
                                }
                                label={loc.transModel<CategoryAttributeValue>("isPreselected", 'label')}
                            />
                        </Tooltip>
                    </Box>
                    <Box>
                        <TextField 
                            InputProps={{
                                inputProps: {
                                    min: 0
                                }
                            }}
                            variant="outlined"
                            type="number"
                            margin="normal"
                            fullWidth
                            id="displayOrder"
                            label={loc.transModel<CategoryAttributeValue>("displayOrder", 'label')}
                            name="displayOrder"
                            value={model?.displayOrder === undefined ? () => {extractData("displayOrder", 0); return 0} : model.displayOrder}
                            onChange={(displayOrder) => extractData("displayOrder", displayOrder)}
                        />
                    </Box>
                </Box>
            </form>
        </SubmitDialogForm>

    )
})
export default CategoryAttributeValueDialogForm;
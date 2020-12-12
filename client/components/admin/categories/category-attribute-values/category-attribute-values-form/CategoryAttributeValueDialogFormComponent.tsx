import { Box, Checkbox, FormControlLabel, Input, TextField } from "@material-ui/core"
import { forwardRef, useState, useRef, useImperativeHandle } from "react"
import isEmpty from "../../../../../core/models/utils/StringExtension"
import useExtractData from "../../../../../hooks/data/ExtracttDataHook"
import SubmitDialogForm from "../../../../base/form-dialog/SubmitDialogFormComponent"
import { ValidatorManage, ValidatorType } from "../../../../login/models/validators/Validator"
import CategoryAttributeValue from "../models/CategoryAttributeValue"

interface ICategoryAttributeValueDialogFormProps {
    onSubmit: (model: CategoryAttributeValue) => void
}
const CategoryAttributeValueDialogForm = forwardRef((props: ICategoryAttributeValueDialogFormProps, ref) => {
    const formDialog = useRef(null);
    const [injectData, model, extractData, extractFromDerivedValue] = useExtractData<CategoryAttributeValue>(new CategoryAttributeValue());
    const [errors, setErrors] = useState({name: ""});
    const categoryAttributeValueValidator = new ValidatorManage();
    categoryAttributeValueValidator.setValidators({
        ["name"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Nazwa jest wymagana.",
            isValid: true
        }]
    });

    const handleSubmit = async () => {
        categoryAttributeValueValidator.isValid(model);
        setErrors({...errors, name: categoryAttributeValueValidator.getMessageByKey("name")});
        if(categoryAttributeValueValidator.isAllValid()) {
            props.onSubmit(model);
            formDialog.current.closeForm();
            injectData(new CategoryAttributeValue());
            setErrors(prevState => prevState.name = "");
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
            injectData(model);
            formDialog.current.openForm();
        }
    }))
    return (
        <SubmitDialogForm
            title="Łączenie wartości z atrybutem"
            handleSubmit={handleSubmit}
            onCancel={handleCancel}
            ref={formDialog}
            >
            <form noValidate>
                <TextField
                    InputLabelProps={{shrink: !isEmpty(model?.name)}}
                    error={!!errors.name}
                    helperText={errors.name}
                    value={model?.name}
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="name"
                    label="Nazwa"
                    name="name"
                    onChange={(name)  => extractData("name", name)}/>
                <Box sx={{display: 'flex'}}>
                    <Box>
                        <FormControlLabel
                            control={
                                <Checkbox
                                    checked={model?.isPreSelected}
                                    onChange={() => extractFromDerivedValue("isPreSelected", !model?.isPreSelected)}
                                />
                            }
                            label="Domyślnie wybrany"
                        />
                    </Box>
                    <Box>
                        <TextField 
                            variant="outlined"
                            type="number"
                            margin="normal"
                            fullWidth
                            id="displayOrder"
                            label="Pozycja"
                            name="displayOrder"
                            onChange={(displayOrder) => extractData("displayOrder", displayOrder)}
                        />
                    </Box>
                </Box>
            </form>
        </SubmitDialogForm>

    )
})
export default CategoryAttributeValueDialogForm;
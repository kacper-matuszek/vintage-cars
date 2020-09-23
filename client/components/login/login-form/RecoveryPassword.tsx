import { Button, TextField } from "@material-ui/core";
import { useRef, useState } from "react";
import FormDialog from "../../base/FormDialogComponent";
import { ValidatorManage, ValidatorType } from "../models/validators/Validator";

const RecoveryPassword = (props) => {
    const formDialogRef = useRef(null);
    /*errors*/
    const [errors, setErrors] = useState({
        email: "",
    });

    /*validators*/
    const validatorManager = new ValidatorManage();
    validatorManager.setValidators({
        ["email"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Email jest wymagany.",
            isValid: true
        }]
    });

    /*data*/
    let email: string;
    const saveHandle = (event) => {
        event.preventDefault();
        validatorManager.isValid({email});
        setErrors({...errors, email: validatorManager.getMessageByKey("email")})
        if(validatorManager.isAllValid()) {
            props.sendRecoveryPassword(email);
            formDialogRef.current.closeForm();
        }
    }
    const save = () => {
        return(
            <Button color="primary" variant="contained" onClick={saveHandle}>
                Wyślij
            </Button>
        )
    }

    return (
        <FormDialog 
            ref={formDialogRef}
            caption="Zapomniałeś hasła ?" 
            title="Resetowanie hasła" showLink={true}
            actions={save()}>
            <TextField
                error={!!errors.email}
                autoFocus
                margin="dense"
                id="email"
                label="E-mail"
                type="email"
                value={email}
                onChange={(emailEv) => email = emailEv.target.value}
                helperText={errors.email}
                fullWidth
            />
        </FormDialog>
    )
}

export default RecoveryPassword;
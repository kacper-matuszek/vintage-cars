import { IconWithContent } from "../../core/models/base/IconWithContent"
import PermContactCalendarIcon from '@material-ui/icons/PermContactCalendar';
import ProfileSection from "./profile-section/ProfileSectionComponent";
import { NavigationFormDialog } from "../base/form-dialog/NavigationFormDialogComponent";
import { useState } from "react";
import { ValidatorManage, ValidatorType } from "../login/models/validators/Validator";

const NavigationProfileDialog = (props) => {
        /*profile section errors*/
    const [errors, setErrors] = useState({
        firstName: "",
        lastName: "",
        phoneNumber: "",
    });

    const profileSectionValidatorManager = new ValidatorManage();
    profileSectionValidatorManager.setValidators({
        ["firstName"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Imię jest wymagane.",
            isValid: true
        }],
        ["lastName"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Nazwisko jest wymagane.",
            isValid: true
        }],
        ["phoneNumber"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Nr. telefnu jest wymagany.",
            isValid: true,
        }]
    });


    const navigation: Array<IconWithContent> = [
        {title: "Dane kontaktowe", icon: <PermContactCalendarIcon/>, content: <ProfileSection errors={errors}/> }
    ];

    return (
        <NavigationFormDialog
        title="Dane profilu"
        titleIsDynamic={true}
        iconsWithContent={navigation}
        open={props.open}
        onClose={props.onClose}
        showSave={props.showSave}
        />
    )
}
export default NavigationProfileDialog;
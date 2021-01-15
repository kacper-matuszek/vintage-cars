import { IconWithContent } from "../../core/models/base/IconWithContent"
import PermContactCalendarIcon from '@material-ui/icons/PermContactCalendar';
import ProfileSection from "./profile-section/ProfileSectionComponent";
import { NavigationFormDialog } from "../base/form-dialog/NavigationFormDialogComponent";
import { useState } from "react";
import { ValidatorManager, ValidatorType } from "../../core/models/shared/Validator";
import ContactProfile from "./models/ContactProfile";

const NavigationProfileDialog = (props) => {
    const navigation: Array<IconWithContent> = [
        {title: "Dane kontaktowe", icon: <PermContactCalendarIcon/>, content: <ProfileSection /> }
    ];

    return (
        <NavigationFormDialog
        title="Dane profilu"
        titleIsDynamic={true}
        iconsWithContent={navigation}
        open={props.open}
        onClose={props.onClose}
        />
    )
}
export default NavigationProfileDialog;
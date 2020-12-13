import { forwardRef, useImperativeHandle, useRef } from "react";
import { BaseProps } from "../../../core/models/base/BaseProps";
import SaveButton from "../controls/SaveButtonComponent";
import FormDialog from "../FormDialogComponent";
interface ISubmitDialogForm extends BaseProps {
    showLink?: boolean,
    caption?: string,
    variantCaption?: 'text' | 'outlined' | 'contained',
    title: string,
    disableOpenButton?: boolean,
    fullScreen?: boolean,
    maxWidth?: 'lg' | 'md' | 'sm' | 'xl'| 'xs',
    onCancel?: () => void,
    showChangeScreen?: boolean,
    handleSubmit: () => Promise<void>
}
const SubmitDialogForm = forwardRef((props: ISubmitDialogForm, ref) => {
    const formDialog = useRef(null);
    const addActions = () => <SaveButton onSubmit={props.handleSubmit}/>
    
    useImperativeHandle(ref, () => ({
        closeForm() {
            formDialog.current.closeForm();
        },
        openForm() {
            formDialog.current.openForm();
        }
    }));
    return (
        <FormDialog
            showLink={props.showLink}
            caption={props.caption}
            variantCaption={props.variantCaption}
            disableOpenButton={props.disableOpenButton}
            fullScreen={props.fullScreen}
            maxWidth={props.maxWidth}
            onCancel={props.onCancel}
            showChangeScreen={props.showChangeScreen}
            showCancel={true}
            title={props.title}
            actions={addActions()}
            ref={formDialog}
            >
            {props.children}
        </FormDialog>
    )
})
export default SubmitDialogForm;
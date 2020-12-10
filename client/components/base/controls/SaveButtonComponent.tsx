import SaveIcon from '@material-ui/icons/Save';
import LoadingButton from '@material-ui/lab/LoadingButton'
import { useState } from 'react';
interface SaveButtonProps {
    onSubmit: () => Promise<void>,
    disabled?: boolean
}
const SaveButton = (props : SaveButtonProps) => {
    const {onSubmit, disabled} = props;
    const [loading, setLoading] = useState(false);
    const handleClick = async (event) => {
        event.preventDefault();
        setLoading(true);
        await onSubmit().finally(() => setLoading(false));
    }
    return (
        <LoadingButton
            pending={loading}
            pendingPosition="start"
            startIcon={<SaveIcon/>}
            variant="contained"
            color="primary"
            onClick={handleClick}
            disabled={disabled === undefined ? false : disabled}
        >
            Zapisz
        </LoadingButton>
    )
}
export default SaveButton;
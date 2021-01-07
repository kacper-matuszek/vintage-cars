import { Checkbox, FormControl, FormControlLabel, InputLabel, MenuItem, Select, TextField } from "@material-ui/core";
import { Guid } from "guid-typescript";
import ISelectable from "../../../core/models/base/ISelectable";
import { AttributeControlType } from "../../../core/models/enums/AttributeControlType";
import { isEmpty } from "../../../core/models/utils/ObjectExtension";
import isStringNullOrEmpty from "../../../core/models/utils/StringExtension";

interface ExtendedControlProps {
    id: string,
    label: string,
    attributeControlType: AttributeControlType,
    value?: any,
    onChangeValue: (value: any) => void,
    multipleOptions?: Array<ISelectable>
}
const ExtendedControl = (props: ExtendedControlProps) => {
    const {attributeControlType, label, id, value, onChangeValue, multipleOptions} = props;

    const handleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        const value: any = event.target.value;
        onChangeValue(value);
    };

    const renderSwitch = (attributeControlType: AttributeControlType) => {
        switch(attributeControlType)
        {
            case AttributeControlType.TextBox: {
                return (
                    <TextField
                        InputLabelProps={{shrink: !isStringNullOrEmpty(value)}}
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        id={id}
                        key={`${id}-${Guid.create()}`}
                        label={label}
                        name={id}
                        onChange={handleChange}
                    />
                )
            }
            case AttributeControlType.Checkboxes: {
                return isEmpty(multipleOptions) ? <></> :
                    multipleOptions.map((obj, index) => {
                        return (
                            <FormControlLabel
                                key={`control-label-${Guid.create()}`}
                                control={
                                    <Checkbox
                                        checked={isEmpty(obj?.isSelected) ? false : obj.isSelected}
                                        onChange={() => 
                                            {
                                                obj.isSelected = !obj.isSelected;
                                                onChangeValue(obj);
                                            }}
                                    />
                                }
                                label={label}
                                id={id}
                            />
                        )
                    })
            }
            case AttributeControlType.DropdownList: {
                return (
                    <FormControl
                        fullWidth
                        key={`${id}-${Guid.create()}`}
                    >
                        <InputLabel
                            key={`label-${id}-${Guid.create()}`}
                            id={id}
                            htmlFor={id}
                        >
                            {label}
                        </InputLabel>
                        <Select
                            key={`select-${id}-${Guid.create()}`}
                            id={`select-${id}`}
                            fullWidth
                            value={value}
                            onChange={handleChange}
                        >
                            {
                                isEmpty(multipleOptions) ? 
                                <MenuItem key={`empty-menu-item-${Guid.create()}`} value={null}>{"Brak"}</MenuItem> : 
                                multipleOptions.map((obj, index) => {
                                    return (
                                        <MenuItem key={`menu-item-${index}-${Guid.create()}`}
                                            value={obj.id.toString()}
                                        >
                                            {obj.name}
                                        </MenuItem>
                                    )
                                })
                            }
                        </Select>
                    </FormControl>
                )
            }
        }
    }

    return (<>{renderSwitch(attributeControlType)}</>);
}
export default ExtendedControl;
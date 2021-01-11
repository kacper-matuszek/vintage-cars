import { Box, Checkbox, FormControl, FormControlLabel, InputLabel, MenuItem, Select, TextField } from "@material-ui/core";
import { Guid } from "guid-typescript";
import { useEffect, useState } from "react";
import ISelectable from "../../../core/models/base/ISelectable";
import { AttributeControlType } from "../../../core/models/enums/AttributeControlType";
import { ExtendedControlChangeValueType } from "../../../core/models/enums/ExtendedControlChangeValueType";
import { isEmpty } from "../../../core/models/utils/ObjectExtension";
import Caption from "./CaptionComponent";

interface ExtendedControlProps {
    id: string,
    label: string,
    attributeControlType: AttributeControlType,
    value?: any,
    onChangeValue: (value: any, type: ExtendedControlChangeValueType) => void,
    multipleOptions?: Array<ISelectable>
}
const ExtendedControl = (props: ExtendedControlProps) => {
    const {attributeControlType, label, id, value, onChangeValue, multipleOptions} = props;
    
    const [checkedChexkBoxes, setCheckedCheckBoxes] = useState([]);
    const [valueOneOption, setValueOneOption] = useState<string>('')
    const handleChange = (event: React.ChangeEvent<{ value: unknown }>, type: ExtendedControlChangeValueType) => {
        const value: any = event.target.value;
        if(type === ExtendedControlChangeValueType.Id || type === ExtendedControlChangeValueType.Text) {
            setValueOneOption(value);
        }
        onChangeValue(value, type);
    };

    const renderSwitch = (attributeControlType: AttributeControlType) => {
        switch(attributeControlType)
        {
            case AttributeControlType.TextBox: {
                return (
                    <TextField
                        InputLabelProps={{shrink: true}}
                        variant="outlined"
                        margin="normal"
                        fullWidth
                        id={id}
                        key={id}
                        label={label}
                        name={id}
                        value={valueOneOption}
                        onChange={(e) => handleChange(e, ExtendedControlChangeValueType.Text)}
                    />
                )
            }
            case AttributeControlType.Checkboxes: {
                return isEmpty(multipleOptions) ? <></> :
                    <>
                        <Box sx={{display: 'flex', flexDirection: 'row'}}>
                            <Caption text={`${label}:`} key={`caption-${id}`}/>
                            {checkedChexkBoxes.map((obj, index) => {
                                return (
                                        <FormControlLabel
                                            key={`control-label-${index}`}
                                            control={
                                                <Checkbox
                                                    //checked={obj?.isSelected} error uncontrolled -> controlled (vice versa)
                                                    key={`control-label-checkbox-${index}`}
                                                    onChange={() => 
                                                        {
                                                            setCheckedCheckBoxes(prevState => {
                                                                let checkedCheckBox = prevState.find(x => x.id === obj.id);
                                                                checkedCheckBox.isSelected = !checkedCheckBox.isSelected;
                                                                return prevState;
                                                            });
                                                            onChangeValue(obj, ExtendedControlChangeValueType.Object);
                                                        }}
                                                />
                                            }
                                            label={obj.name}
                                            id={id}
                                        />
                                )
                            })}
                        </Box>
                    </>
                    
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
                            value={valueOneOption}
                            onChange={(e) => handleChange(e, ExtendedControlChangeValueType.Id)}
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

    useEffect(() => {
        if(checkedChexkBoxes?.length === 0) {
            multipleOptions.forEach(obj => {
                setCheckedCheckBoxes(prevState => {
                    prevState.push(obj);
                    return prevState;
                })
            });
        }
    }, []);

    return (<>{renderSwitch(attributeControlType)}</>);
}
export default ExtendedControl;
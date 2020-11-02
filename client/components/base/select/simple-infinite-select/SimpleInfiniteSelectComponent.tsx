import { CircularProgress, FormControl, InputLabel, MenuItem, Select, TextField } from "@material-ui/core"
import { Autocomplete } from "@material-ui/lab";
import { makeStyles } from "@material-ui/styles";
import { Guid } from "guid-typescript";
import { useEffect, useRef, useState } from "react"
import ISelectable from "../../../../core/models/base/ISelectable";
import Paged from "../../../../core/models/paged/Paged";

interface SimpleInfiniteSelectProps {
    id: string,
    label: string,
    maxHeight: string,
    data: Array<ISelectable>,
    totalCount: number,
    pageSize: number,
    isLoading: boolean,
    fetchData: (paged: Paged) => void,
    onChangeValue: (value: Guid) => void,
    fullWidth?: boolean,
    displayEmpty?: boolean,
    disabled?: boolean,
}
const useStyles = makeStyles(theme => ({
    menuPaper: {
        maxHeight: 100
    }
}))
const SimpleInfiniteSelect = (props: SimpleInfiniteSelectProps) => {
    const classes = useStyles();
    const [selectValue, setSelectValue] = useState('');
    const paged = useRef(new Paged(-1, props.pageSize));
    const [open, setOpen] = useState(false);
    const loadMoreItems = (e) => {
        const bottom = e.target.scrollHeight - e.target.scrollTop === e.target.clientHeight;
        if(bottom)
        {
            pingToGetData();
        }
    };
    const pingToGetData = () => {
        if(props.data.length === props.totalCount) return;
        paged.current.increment();
        props.fetchData(paged.current);
    };

    useEffect(() => {
        pingToGetData();
    }, []);
    return (
        <FormControl variant="outlined" fullWidth disabled={props.disabled} margin="normal">
            {/* <InputLabel id={`infinite-select-label-${props.id}`}>{props.label}</InputLabel>
            <Select
                displayEmpty={props.displayEmpty}
                fullWidth={props.fullWidth}
                labelId={`infinite-select-label-${props.id}`}
                label={props.label}
                id={`infinite-select-${props.id}`}
                value={selectValue}
                onChange={(event, child) => {
                    setSelectValue(event.target.value as string);
                    props.onChangeValue(child.props.id);
                }}
                MenuProps={{
                 onScroll: loadMoreItems,
                 classes: {
                     paper: classes.menuPaper
                 }
                }}
            >
            {props.data === undefined || props.data.length === 0 ? 
                <MenuItem key={'menu-item-empty'} disabled>Brak</MenuItem>
                : props.data.map((value, index) => {
                    return(
                        <MenuItem id={`${value?.id}`} key={`menu-item-${index}-${value?.id}`} value={value?.name}>{value?.name}</MenuItem>
                    )
                })}
            </Select> */}
            <Autocomplete
                id={`infinite-select-label-${props.id}`}
                className={classes.menuPaper}
                disabled={props.disabled}
                fullWidth={props.fullWidth}
                open={open}
                onOpen={() => setOpen(true)}
                onClose={() => setOpen(false)}
                onChange={(event, value) => props.onChangeValue(props.data.filter(v => v.id === (value as ISelectable).id)[0].id)}
                getOptionSelected={(option, value) => option.id === value.id}
                getOptionLabel={(option) => option.name}
                options={props.data}
                noOptionsText="Brak"
                loading={props.isLoading}
                ListboxProps={{
                    onScroll: loadMoreItems,
                    style: {
                        maxHeight: 100
                    }
                }}
                renderInput={(params) => (
                    <TextField
                    {...params}
                    label={props.label}
                    variant="outlined"
                    InputProps={{
                      ...params.InputProps,
                      endAdornment: (
                        <>
                          {props.isLoading ? <CircularProgress color="inherit" size={20} /> : null}
                          {params.InputProps.endAdornment}
                        </>
                      ),
                    }}
                    />
                )}
            />
        </FormControl>
    )
}

export default SimpleInfiniteSelect;
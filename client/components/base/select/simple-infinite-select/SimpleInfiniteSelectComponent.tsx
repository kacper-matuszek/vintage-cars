import { CircularProgress, FormControl, InputLabel, MenuItem, Select, TextField } from "@material-ui/core"
import { Autocomplete } from "@material-ui/core";
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
    value?: Guid,
    totalCount: number,
    pageSize: number,
    isLoading: boolean,
    fetchData: (paged: Paged) => void,
    onChangeValue: (value: Guid) => void,
    fullWidth?: boolean,
    displayEmpty?: boolean,
    disabled?: boolean,
    required?: boolean,
    error?: boolean,
    errorText?: string
}
const useStyles = makeStyles(theme => ({
    menuPaper: {
        maxHeight: 100
    }
}))
const SimpleInfiniteSelect = (props: SimpleInfiniteSelectProps) => {
    const classes = useStyles();
    const paged = useRef(new Paged(-1, props.pageSize));
    const [open, setOpen] = useState(false);
    const [defaultValue, setDefaultValue] = useState(null);
    const loadMoreItems = (e) => {
        const bottom = e.target.scrollHeight - e.target.scrollTop === e.target.clientHeight;
        if(bottom)
        {
            pingToGetData();
        }
    };
    const pingToGetData = () => {
        if(props.data.length > 0 && props.data.length === props.totalCount) return;
        paged.current.increment();
        props.fetchData(new Paged(paged.current.pageIndex, paged.current.pageSize));
    };
    const searchValue = () => {
        if(defaultValue !== null || props.value === null || props.value === undefined || props.value.toString() === Guid.EMPTY || props.data.length === 0)
            return;
        
        let foundedValue = props.data.find(x => x.id == props.value);
        if(foundedValue !== null && foundedValue !== undefined)
        {
            setDefaultValue(foundedValue);
            props.onChangeValue(foundedValue.id);
            return;
        } else
            pingToGetData();
        searchValue();
    }
    useEffect(() => {
        pingToGetData();
    }, []);
    useEffect(() => {
        searchValue();
    }, [props.value, props.data])
    return (
        <FormControl variant="outlined" fullWidth disabled={props.disabled} margin="normal">
            <Autocomplete
                id={`infinite-select-label-${props.id}`}
                className={classes.menuPaper}
                disabled={props.disabled}
                fullWidth={props.fullWidth}
                open={open}
                onOpen={() => setOpen(true)}
                onClose={() => setOpen(false)}
                onChange={(event, value) => 
                    {
                        setDefaultValue(value);
                        let id = props.data.filter(v => v.id === (value as ISelectable)?.id)[0]?.id;
                        if(id === undefined || id === null)
                            id = Guid.createEmpty();
                        props.onChangeValue(id);
                    }}
                //getOptionSelected={(option, value) => option.id === value.id}
                getOptionLabel={(option) => option.name}
                value={defaultValue}
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
                    required={props.required}
                    error={props.error}
                    helperText={props.errorText}
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
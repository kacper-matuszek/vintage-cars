import { Container, FormControl, Grid, InputLabel, Paper, Select, TextField } from "@material-ui/core";
import { useCallback, useEffect, useRef, useState } from "react";
import { Virtuoso } from "react-virtuoso";
import { profileSectionStyle } from "./profile-section-style";

const ProfileSection = (props) => {
    const classes = profileSectionStyle();
    const [totalCountry, setTotalCountry] = useState(0);
    const [totalStateProvince, setTotalStateProvince] = useState(0);

    const countryItems = useRef([]);
    const stateProvinceItems = useRef([]);

    const countryLoading = useRef(false);
    const stateProvinceLoading = useRef(false);

    const loadMoreCountries = useCallback(() => {
        if(countryLoading.current)
            return;
        countryLoading.current = true;
        //get from api
        countryLoading.current = false
        setTotalCountry(countryItems.current.length);
    }, []);
    const loadMoreStateProvinces = useCallback(() => {
        if(stateProvinceLoading.current)
            return;
        stateProvinceLoading.current = true;
        //get from api
        stateProvinceLoading.current = false;
        setTotalStateProvince(stateProvinceItems.current.length);
    }, [])

    useEffect(() => {
        loadMoreCountries();
        loadMoreStateProvinces();
    }, [])
    return (
        <Container>
            <form className={classes.form} noValidate method="POST">
                <TextField
                error={!!props.errors.firstName}
                helperText={props.errors.firstName}
                variant="outlined"
                margin="normal"
                required
                fullWidth
                id="firstName"
                label="Imię"
                name="firstName"
                autoComplete="firstName" />
                <TextField
                error={!!props.errors.lastName}
                helperText={props.errors.lastName}
                variant="outlined"
                margin="normal"
                required
                fullWidth
                id="lastName"
                label="Nazwisko"
                name="lastName"
                autoComplete="lastName" />
                <TextField
                error={!!props.errors.phoneNumber}
                helperText={props.errors.phoneNumber}
                variant="outlined"
                margin="normal"
                fullWidth
                required
                id="phoneNumber"
                label="Numer telefonu"
                name="phoneNumber"
                autoComplete="phoneNumber" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="company"
                label="Nazwa firmy"
                name="company"
                autoComplete="company" />
                <FormControl variant="outlined">
                    <InputLabel id="label-select-outlined-country">Country</InputLabel>
                    <Select labelId="label-select-outlined-country"
                    id="select-outlined-country"
                    label="Country">
                        <Virtuoso
                        overscan={500}
                        totalCount={totalCountry}
                        footer={() => {
                            return (
                                <div style={{padding: '2rem', textAlign: 'center'}}>
                                    Loading...
                                </div>
                            )
                        }}
                        />
                    </Select>
                </FormControl>
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="country"
                label="Kraj"
                name="country"
                autoComplete="country" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="stateProvince"
                label="Województwo"
                name="stateProvince"
                autoComplete="stateProvince" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="city"
                label="Miejscowość"
                name="city"
                autoComplete="city" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="address"
                label="Adres"
                name="address"
                autoComplete="address" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="postalCode"
                label="Kod pocztowy"
                name="postalCode"
                autoComplete="postalCode" />
            </form>
        </Container>
    )
}

export default ProfileSection;
import { Box, Button, Container, Divider, TextField } from "@material-ui/core";
import { Guid } from "guid-typescript";
import { useState } from "react";
import Paged from "../../../core/models/paged/Paged";
import usePagedListAPI from "../../../hooks/fetch/pagedAPI/PagedAPIHook";
import SimpleInfiniteSelect from "../../base/select/simple-infinite-select/SimpleInfiniteSelectComponent";
import CountryView from "../models/CountryView";
import StateProvinceView from "../models/StateProvinceView";
import { profileSectionStyle } from "./profile-section-style";

const ProfileSection = (props) => {
    const classes = profileSectionStyle();
    const [countryId, setCountryId] = useState(Guid.createEmpty());
    const [stateProvinceId, setStateProvinceId] = useState(Guid.createEmpty());
    const [fetchCountry, isLoadingCountry, responseCountry] = usePagedListAPI<CountryView>("/v1/country/all");
    const [fetchStateProvince, isLoadingStateProvince, responseStateProvince] = usePagedListAPI<StateProvinceView>(`/v1/country/state-province/all/${countryId}`);

    return (
        <Container>
            <form className={classes.form} noValidate method="POST" onSubmit={props.onSubmit}>
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
                <SimpleInfiniteSelect
                    id="country"
                    label="Kraj"
                    maxHeight="200px"
                    fullWidth={true}
                    pageSize={10}
                    isLoading={isLoadingCountry}
                    fetchData={fetchCountry}
                    data={responseCountry?.source}
                    onChangeValue={(value) => { 
                        setCountryId(value);
                        fetchStateProvince(new Paged(0, 10));
                    }}
                    totalCount={responseCountry?.totalCount}
                /> 
                <SimpleInfiniteSelect
                    id="state-province"
                    label="Województwo"
                    maxHeight="200px"
                    disabled={countryId.toString() === Guid.EMPTY}
                    fullWidth={true}
                    pageSize={10}
                    isLoading={isLoadingStateProvince}
                    fetchData={fetchStateProvince}
                    data={responseStateProvince?.source}
                    onChangeValue={(value) => setStateProvinceId(value)}
                    totalCount={responseStateProvince?.totalCount}
                />
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
                <Box p={5}></Box>
                <Box display="flex" justifyContent="flex-end" width="100%" m={2}>
                    <Button
                        type="submit"
                        variant="contained"
                        color="primary">
                            Zapisz
                    </Button>
                </Box>
            </form>
        </Container>
    )
}

export default ProfileSection;
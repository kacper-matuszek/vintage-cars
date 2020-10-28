import { Box, Button, Container, Divider, TextField } from "@material-ui/core";
import { Guid } from "guid-typescript";
import { createRef, Dispatch, SetStateAction, useState } from "react";
import Paged from "../../../core/models/paged/Paged";
import useExtractData from "../../../hooks/data/ExtracttDataHook";
import usePagedListAPI from "../../../hooks/fetch/pagedAPI/PagedAPIHook";
import SimpleInfiniteSelect from "../../base/select/simple-infinite-select/SimpleInfiniteSelectComponent";
import { ValidatorManage, ValidatorType } from "../../login/models/validators/Validator";
import ContactProfile from "../models/ContactProfile";
import CountryView from "../models/CountryView";
import StateProvinceView from "../models/StateProvinceView";
import { profileSectionStyle } from "./profile-section-style";

const ProfileSection = () => {
    const classes = profileSectionStyle();
     /*profile section errors*/
     const [errors, setErrors] = useState({
        firstName: "",
        lastName: "",
        phoneNumber: "",
        postalCode: "",
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
        }],
        ["postalCode"]: [{
            type: ValidatorType.ZipCode,
            paramValue: null,
            message: "Wprowadź poprawny kod pocztowy.",
            isValid:  true,
        }]
    });
    const [model, extractData, extractDataFromDerivedValue] = useExtractData(new ContactProfile());
    
    const [countryId, setCountryId] = useState(Guid.createEmpty());
    const [fetchCountry, isLoadingCountry, responseCountry] = usePagedListAPI<CountryView>("/v1/country/all");
    const [fetchStateProvince, isLoadingStateProvince, responseStateProvince] = usePagedListAPI<StateProvinceView>(`/v1/country/state-province/all/${countryId}`);
    const handleSubmit = (event) =>{
        event.preventDefault();
        console.log(model);
    }
    return (
        <Container>
            <form className={classes.form} noValidate method="POST" onSubmit={handleSubmit}>
                <TextField
                error={!!errors.firstName}
                helperText={errors.firstName}
                variant="outlined"
                margin="normal"
                required
                fullWidth
                id="firstName"
                label="Imię"
                name="firstName"
                autoComplete="firstName"
                onChange={(firstName) => extractData("firstName", firstName)}/>
                <TextField
                error={!!errors.lastName}
                helperText={errors.lastName}
                variant="outlined"
                margin="normal"
                required
                fullWidth
                id="lastName"
                label="Nazwisko"
                name="lastName"
                autoComplete="lastName" 
                onChange={(lastName) => extractData("lastName", lastName)}/>
                <TextField
                error={!!errors.phoneNumber}
                helperText={errors.phoneNumber}
                variant="outlined"
                margin="normal"
                fullWidth
                required
                id="phoneNumber"
                label="Numer telefonu"
                name="phoneNumber"
                autoComplete="phoneNumber" 
                onChange={(phoneNumber) => extractData("phoneNumber", phoneNumber)}/>
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="company"
                label="Nazwa firmy"
                name="company"
                autoComplete="company"
                onChange={(company) => extractData("company", company)} />
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
                        extractDataFromDerivedValue("countryId", value);
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
                    onChangeValue={(value) => extractDataFromDerivedValue("stateProvinceId", value)}
                    totalCount={responseStateProvince?.totalCount}
                />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="city"
                label="Miejscowość"
                name="city"
                autoComplete="city"
                onChange={(city) => extractData("city", city)} />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="address"
                label="Adres"
                name="address"
                autoComplete="address"
                onChange={(address) => extractData("address1", address)} />
                <TextField
                error={!!errors.postalCode}
                helperText={errors.postalCode}
                variant="outlined"
                margin="normal"
                fullWidth
                id="postalCode"
                label="Kod pocztowy"
                name="postalCode"
                autoComplete="postalCode"
                onChange={(postalCode) => extractData("zipPostalCode", postalCode)} />
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
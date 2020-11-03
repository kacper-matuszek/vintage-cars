import { Box, Button, Container, Divider, TextField, CircularProgress, Backdrop, } from "@material-ui/core";
import { Guid } from "guid-typescript";
import { createRef, Dispatch, SetStateAction, useEffect, useState } from "react";
import Paged from "../../../core/models/paged/Paged";
import isEmpty from "../../../core/models/utils/StringExtension";
import useExtractData from "../../../hooks/data/ExtracttDataHook";
import useGetData from "../../../hooks/fetch/GetDataHook";
import usePagedListAPI from "../../../hooks/fetch/pagedAPI/PagedAPIHook";
import useSendSubmitWithNotification from "../../../hooks/fetch/SendSubmitHook";
import useLoading from "../../../hooks/utils/LoadingHook";
import withLoading from "../../base/loading/LoadingComponent";
import SimpleInfiniteSelect from "../../base/select/simple-infinite-select/SimpleInfiniteSelectComponent";
import { ValidatorManage, ValidatorType } from "../../login/models/validators/Validator";
import ContactProfile from "../models/ContactProfile";
import CountryView from "../models/CountryView";
import StateProvinceView from "../models/StateProvinceView";
import { profileSectionStyle } from "./profile-section-style";

const ProfileSection = (props) => {
    const {showLoading, hideLoading} = props;
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
            message: "Nr. telefonu jest wymagany.",
            isValid: true,
        }],
        ["zipPostalCode"]: [{
            type: ValidatorType.ZipCode,
            paramValue: null,
            message: "Wprowadź poprawny kod pocztowy.",
            isValid:  true,
        }]
    });
    const [receivedModel, isLoading] = useGetData<ContactProfile>("/v1/account/details");
    const [injectData, model, extractData, extractDataFromDerivedValue] = useExtractData<ContactProfile>(receivedModel);
    const [send] = useSendSubmitWithNotification("/v1/account/details", showLoading, hideLoading)
    const [countryId, setCountryId] = useState(Guid.createEmpty());
    const [fetchCountry, isLoadingCountry, responseCountry] = usePagedListAPI<CountryView>("/v1/country/all");
    const [fetchStateProvince, isLoadingStateProvince, responseStateProvince] = usePagedListAPI<StateProvinceView>(`/v1/country/state-province/all/${countryId}`);
    const handleSubmit = async (event) => {
        event.preventDefault();
        profileSectionValidatorManager.isValid(model);
        setErrors({...errors,
             firstName: profileSectionValidatorManager.getMessageByKey("firstName"), 
             lastName: profileSectionValidatorManager.getMessageByKey("lastName"),
            phoneNumber: profileSectionValidatorManager.getMessageByKey("phoneNumber"),
            postalCode: profileSectionValidatorManager.getMessageByKey("zipPostalCode"),
        });
        if(profileSectionValidatorManager.isAllValid())
            await send(model);
    }

    useEffect(() => {
        injectData(receivedModel);
    }, [receivedModel]);
    
    useLoading([isLoading], showLoading, hideLoading);
    return (
        <Box>
            <form className={classes.form} noValidate method="POST" onSubmit={handleSubmit}>
                <TextField
                InputLabelProps={{shrink: !isEmpty(model?.firstName)}}
                error={!!errors.firstName}
                helperText={errors.firstName}
                value={model?.firstName}
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
                InputLabelProps={{shrink: !isEmpty(model?.lastName)}}
                error={!!errors.lastName}
                helperText={errors.lastName}
                value={model?.lastName}
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
                InputLabelProps={{shrink: !isEmpty(model?.phoneNumber)}}
                error={!!errors.phoneNumber}
                helperText={errors.phoneNumber}
                value={model?.phoneNumber}
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
                InputLabelProps={{shrink: !isEmpty(model?.company)}}
                value={model?.company}
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
                    value={model?.countryId}
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
                    disabled={countryId?.toString() === Guid.EMPTY}
                    fullWidth={true}
                    pageSize={10}
                    value={model?.stateProvinceId}
                    isLoading={isLoadingStateProvince}
                    fetchData={fetchStateProvince}
                    data={responseStateProvince?.source}
                    onChangeValue={(value) => extractDataFromDerivedValue("stateProvinceId", value)}
                    totalCount={responseStateProvince?.totalCount}
                />
                <TextField
                InputLabelProps={{shrink: !isEmpty(model?.city)}}
                value={model?.city}
                variant="outlined"
                margin="normal"
                fullWidth
                id="city"
                label="Miejscowość"
                name="city"
                autoComplete="city"
                onChange={(city) => extractData("city", city)} />
                <TextField
                InputLabelProps={{shrink: !isEmpty(model?.address1)}}
                value={model?.address1}
                variant="outlined"
                margin="normal"
                fullWidth
                id="address"
                label="Adres"
                name="address"
                autoComplete="address"
                onChange={(address) => extractData("address1", address)} />
                <TextField
                InputLabelProps={{shrink: !isEmpty(model?.zipPostalCode)}}
                error={!!errors.postalCode}
                helperText={errors.postalCode}
                value={model?.zipPostalCode}
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
        </Box>
    )
}

export default ProfileSection;
import { Button, FormControl, FormHelperText, InputLabel, MenuItem, OutlinedInput, Select, TextField } from "@material-ui/core";
import { useEffect, useRef, useState } from "react";
import { AttributeControlType } from "../../../../core/models/enums/AttributeControlType";
import useExtractData from "../../../../hooks/data/ExtracttDataHook";
import useAuhtorizedPagedList from "../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import FormDialog from "../../../base/FormDialogComponent";
import SimpleInfiniteSelect from "../../../base/select/simple-infinite-select/SimpleInfiniteSelectComponent";
import { ValidatorManager, ValidatorType } from "../../../../core/models/shared/Validator";
import CategoryAttributeMappingView from "../categories-list/models/CategoryAttributeMappingView";
import CategoryAttributeView from "../category-attributes/models/CategoryAttributeView";
import CategoryAttributeMapping from "./models/CategoryAttributeMapping";
import useLocale from "../../../../hooks/utils/LocaleHook";

interface CategoryLinkAttributeProps {
    onSubmit: (categoryAttributeMapping: CategoryAttributeMappingView) => void,
    disabled?: boolean
}

const CategoryLinkAttribute = (props: CategoryLinkAttributeProps) => {
    const formRef = useRef(null);
    const loc = useLocale('common', ['categories', 'category', 'link-attribute']);
    const [fetchCategoryAttribute, fetchCategoryWithParam, isLoading, categoryAttribtue] = useAuhtorizedPagedList<CategoryAttributeView>('/admin/v1/category/attribute/list');
    const [injectData, model, extractData, extractDataFromDerivedValue] = useExtractData<CategoryAttributeMapping>(new CategoryAttributeMapping());
    const [errors, setErrors] = useState({
        attributeControlType: "",
        categoryAttributeId: "",
    });
    const clearErrors = () => {
        setErrors(state => {
            state.attributeControlType = "";
            state.categoryAttributeId = "";
            return state
        });
    }
    const categoryAttributeValidator = new ValidatorManager();
    categoryAttributeValidator.setValidators({
        ["categoryAttributeId"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: loc.transModel<CategoryAttributeMapping>("categoryAttributeId", ['validators', 'not-empty']),
            isValid: true
        }],
        ["attributeControlType"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: loc.transModel<CategoryAttributeMapping>("attributeControlType", ['validators', 'not-empty']),
            isValid: true
        }]
    });
    const handleSubmit = () => {
        categoryAttributeValidator.isValid(model);
        setErrors({...errors, 
            attributeControlType: categoryAttributeValidator.getMessageByKey("attributeControlType"),
            categoryAttributeId: categoryAttributeValidator.getMessageByKey("categoryAttributeId")
        });
        if(categoryAttributeValidator.isAllValid())
        {
            const categoryView = new CategoryAttributeMappingView();
            categoryView.attributeControlType = model.attributeControlType;
            categoryView.categoryAttributeId = model.categoryAttributeId;
            const attribute = categoryAttribtue.source.filter(x => x.id === model.categoryAttributeId)[0];
            categoryView.name = attribute.name;
            categoryView.description = attribute.description;
            props.onSubmit(categoryView);
            formRef.current.closeForm();
            injectData(new CategoryAttributeMapping());
            clearErrors();
        }
    }
    const handleCancel = () => {
        injectData(new CategoryAttributeMapping());
        clearErrors();
    }
    const handleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        extractData("attributeControlType", event);
    };
    const isReadonly = () => props.disabled !== undefined ? props.disabled : false;
    return (
        <FormDialog
            title={loc.trans('title')}
            caption={loc.trans('caption')}
            variantCaption="contained"
            onCancel={handleCancel}
            showCancel={true}
            showLink={false}
            ref={formRef}
            disableOpenButton={props.disabled}
            actions={
                <Button variant="contained" color="primary" type="submit" onClick={handleSubmit} disabled={isReadonly()}>
                    {loc.trans(['submit', 'caption'])}
                </Button>
            }
        >
            <SimpleInfiniteSelect
                id="category-attribute"
                label={loc.transModel<CategoryAttributeMapping>("categoryAttributeId", 'label')}
                maxHeight="200px"
                fullWidth={true}
                required
                error={!!errors.categoryAttributeId}
                errorText={errors.categoryAttributeId}
                pageSize={10}
                isLoading={isLoading}
                fetchData={fetchCategoryAttribute}
                data={categoryAttribtue.source}
                totalCount={categoryAttribtue?.totalCount}
                onChangeValue={(value) => extractDataFromDerivedValue("categoryAttributeId", value)}
                disabled={isReadonly()}
            />
            <FormControl fullWidth required>
                <InputLabel 
                    id="attribute-control"
                    htmlFor="attribute-control"
                    disabled={isReadonly()}
                    error={!!errors.attributeControlType}>
                        {loc.transModel<CategoryAttributeMapping>("attributeControlType", 'label')}
                </InputLabel>
                <Select
                    id="select-attribute-control"
                    value={model?.attributeControlType}
                    onChange={handleChange}
                    error={!!errors.attributeControlType}
                    fullWidth
                    disabled={isReadonly()}
                >
                    {Object.keys(AttributeControlType).map(obj => {
                        const isValue = parseInt(obj, 10) >= 0;
                        if(isValue)
                        {
                            return (
                                <MenuItem value={obj}>{AttributeControlType[obj]}</MenuItem>
                            )}
                    })}
                </Select>
                <FormHelperText error={!!errors.attributeControlType}>{errors.attributeControlType}</FormHelperText>
            </FormControl>
        </FormDialog>
    )
}
export default CategoryLinkAttribute;
import { Button, FormControl, InputLabel, MenuItem, Select, TextField } from "@material-ui/core";
import { useRef, useState } from "react";
import { AttributeControlType } from "../../../../core/models/enums/AttributeControlType";
import useExtractData from "../../../../hooks/data/ExtracttDataHook";
import useAuhtorizationPagedList from "../../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import FormDialog from "../../../base/FormDialogComponent";
import SimpleInfiniteSelect from "../../../base/select/simple-infinite-select/SimpleInfiniteSelectComponent";
import { ValidatorManage, ValidatorType } from "../../../login/models/validators/Validator";
import CategoryAttributeMappingView from "../categories-list/models/CategoryAttributeMappingView";
import CategoryAttributeView from "../category-attributes/models/CategoryAttributeView";
import CategoryAttributeMapping from "./models/CategoryAttributeMapping";

interface CategoryLinkAttributeProps {
    onSubmit: (categoryAttributeMapping: CategoryAttributeMappingView) => void,
}

const CategoryLinkAttribute = (props: CategoryLinkAttributeProps) => {
    const formRef = useRef(null);
    const [fetchCategoryAttribute, isLoading, categoryAttribtue] = useAuhtorizationPagedList<CategoryAttributeView>('/v1/category/attribute/list');
    const [injectData, model, extractData, extractDataFromDerivedValue] = useExtractData<CategoryAttributeMapping>(new CategoryAttributeMapping());
    const [errors, setErrors] = useState({
        attributeControlType: "",
        categoryAttributeId: "",
    });
    const categoryAttributeValidator = new ValidatorManage();
    categoryAttributeValidator.setValidators({
        ["categoryAttributeId"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Atrybut jest wymagany.",
            isValid: true
        }],
        ["attributeControlType"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Kontrolka jest wymagana.",
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
            categoryView.id = model.categoryAttributeId;

            const attribute = categoryAttribtue.source.filter(x => x.id === model.categoryAttributeId)[0];
            categoryView.name = attribute.name;
            categoryView.description = attribute.description;

            props.onSubmit(categoryView);
            formRef.current.closeForm();
        }
    }
    const handleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
        extractData("attributeControlType", event);
    };
    return (
        <FormDialog
            title="Łączenie atrybutu z kategorią"
            caption="Połącz atrybut"
            variantCaption="contained"
            showCancel={true}
            showLink={false}
            ref={formRef}
            actions={
                <Button variant="contained" color="primary" type="submit" onClick={handleSubmit}>
                    Zapisz
                </Button>
            }
        >
            <SimpleInfiniteSelect
                id="category-attribute"
                label="Atrybut"
                maxHeight="200px"
                fullWidth={true}
                pageSize={10}
                isLoading={isLoading}
                fetchData={fetchCategoryAttribute}
                data={categoryAttribtue.source}
                totalCount={categoryAttribtue?.totalCount}
                onChangeValue={(value) => extractDataFromDerivedValue("categoryAttributeId", value)}
            />
            <FormControl fullWidth variant="outlined">
                <InputLabel id="attribute-control">Kontrolka</InputLabel>
                <Select
                    labelId="attribute-control"
                    id="select-attribute-control"
                    value={model?.attributeControlType}
                    onChange={handleChange}
                    fullWidth
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
            </FormControl>
        </FormDialog>
    )
}
export default CategoryLinkAttribute;
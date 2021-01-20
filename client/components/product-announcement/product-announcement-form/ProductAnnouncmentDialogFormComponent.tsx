import { Box, TextField, Tooltip, Typography } from "@material-ui/core";
import { Guid } from "guid-typescript";
import { forwardRef, useContext, useImperativeHandle, useRef, useState } from "react";
import NotificationContext from "../../../contexts/NotificationContext";
import File from "../../../core/models/base/File";
import { ExtendedControlChangeValueType } from "../../../core/models/enums/ExtendedControlChangeValueType";
import Picture from "../../../core/models/shared/Picture";
import { isEmpty } from "../../../core/models/utils/ObjectExtension";
import { isStringNullOrEmpty } from "../../../core/models/utils/StringExtension";
import useExtractData from "../../../hooks/data/ExtracttDataHook";
import useGetData from "../../../hooks/fetch/GetDataHook";
import useAuhtorizedPagedList from "../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import useSendSubmitWithNotification from "../../../hooks/fetch/SendSubmitHook";
import CategoryShortInfoView from "../../admin/categories/categories-list/models/CategoryShortInfoView";
import CategoryAttributeFullInfoView from "../../admin/categories/category-attributes/models/CategoryAttributeFullInfoView";
import Caption from "../../base/controls/CaptionComponent";
import ExtendedControl from "../../base/controls/ExtendedControlComponent";
import SubmitDialogForm from "../../base/form-dialog/SubmitDialogFormComponent";
import SimpleInfiniteSelect from "../../base/select/simple-infinite-select/SimpleInfiniteSelectComponent";
import ImagesDropzoneArea from "../../base/upload-files/ImagesDropzoneAreaComponent";
import { ValidatorManager, ValidatorType } from "../../../core/models/shared/Validator";
import CreateProductAnnouncement from "../models/CreateProductAnnouncement";
import ProductAnnouncementAttribute from "../models/ProductAnnouncementAttribute";

interface ProductAnnouncementDialogProps {

}

const ProductAnnouncementDialogForm = forwardRef((props, ref) => {
    const formDialog = useRef(null);
    const notification = useContext(NotificationContext);
    
    const [injectData, createModel, extractData, extractFromDerivedValue] = useExtractData<CreateProductAnnouncement>(new CreateProductAnnouncement());
    const productAttributes = useRef(new Array<ProductAnnouncementAttribute>());
    const [images, setImages] = useState<File[]>(new Array<File>());
    const [mainImage, setMainImage] = useState<File[]>(new Array<File>());
    const modelValidator = new ValidatorManager();
    modelValidator.setValidators({
        ["name"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Nazwa jest wymagana.",
            isValid: true
        }],
        ["description"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Opis jest wymagany.",
            isValid: true
        }],
        ["shortDescription"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Opis jest wymagany.",
            isValid: true
        }]
    });
    const [modelErrors, setModelErrors] = useState({
        name: "",
        description: "",
        shortDescription: ""
    });

    const [categoryId, setCategoryId] = useState(Guid.createEmpty());
    const [fetchCategories, _, isLoadingCategories, categories] = useAuhtorizedPagedList<CategoryShortInfoView>("/v1/category/list");
    const [categoryAttributes, isLoading, getData] = useGetData<Array<CategoryAttributeFullInfoView>>("/v1/category/attribute/list", false);
    const [send] = useSendSubmitWithNotification("/v1/productannouncement/create");
    const handleSubmit = async () => {
        if(productAttributes.current.length < categoryAttributes.length) {
            notification.showErrorMessage("Wszystkie atrybuty muszą być wypełnione.");
            return;
        }
        modelValidator.isValid(createModel);
        setModelErrors({...modelErrors, name: modelValidator.getMessageByKey("name"),
            description: modelValidator.getMessageByKey("description"),
            shortDescription: modelValidator.getMessageByKey("shortDescription")
        });
        if(modelValidator.isAllValid()) {
            if(mainImage.length === 1) {
                createModel.pictures.push(...mainImage.map(i => new Picture(Guid.create().toString(), i.type, i.name, i.name, i.dataAsBase64, true)));
            }
            createModel.attributes = productAttributes.current;
            createModel.pictures.push(...images.map(i => new Picture(Guid.create().toString(), i.type, i.name, i.name, i.dataAsBase64)));
            createModel.attributes.forEach(x => x.categoryAttributeValueId = isEmpty(x.categoryAttributeValueId) ? null : x.categoryAttributeValueId.toString());
            await send(createModel).finally(() => 
            {
                injectData(new CreateProductAnnouncement());
                formDialog.current.closeForm();
            });
        }
    }

    const handleExtendedControl = (value: any, model: CategoryAttributeFullInfoView, type: ExtendedControlChangeValueType) => {
        switch(type)
        {
            case ExtendedControlChangeValueType.Text: {
                (() => {
                    let productAttribute = productAttributes?.current?.find(x => x.categoryAttributeId === model.id);
                    if(!isEmpty(productAttribute))
                    {
                        productAttribute.value = value;
                        productAttribute.categoryAttributeValueId = null;
                        return;
                    }

                    if(isStringNullOrEmpty(value)) {
                        productAttributes.current = productAttributes.current.filter(x => x.categoryAttributeId !== model.id);
                        return;
                    }
                    productAttributes.current.push(new ProductAnnouncementAttribute(model.id, value, null));
                    return;
                })();
                break;
            }
            case ExtendedControlChangeValueType.Id: {
                (() => {
                    const parsedGuidId = Guid.parse(value);
                    let productAttribute = productAttributes?.current?.find(x => x.categoryAttributeId?.toString() == parsedGuidId.toString());
                    if(!isEmpty(productAttribute))
                    {
                        productAttribute.value = null;
                        productAttribute.categoryAttributeId = parsedGuidId;
                        return;
                    }

                    productAttributes.current.push(new ProductAnnouncementAttribute(model.id, null, parsedGuidId));
                    return;
                })();
                break;
            }
            case ExtendedControlChangeValueType.Object: {
                (() => {
                    const parsedGuid = Guid.parse(value.id);
                    const findedInMultOpt = productAttributes?.current?.find(x => x.categoryAttributeValueId?.toString() === parsedGuid.toString());
                    
                    if(isEmpty(findedInMultOpt)) {
                        productAttributes.current.push(new ProductAnnouncementAttribute(model.id, null, parsedGuid));
                        return;
                    }
                    
                    console.log(value.isSelected);
                    if(!value.isSelected) {
                        productAttributes.current = productAttributes.current.filter(x => x.categoryAttributeValueId?.toString() !== parsedGuid.toString());
                        return;
                    }
                })();
                break;
            }
        }
    }

    useImperativeHandle(ref, () => ({
        openForm() {

        }
    }));

    return (
        <>
            <SubmitDialogForm
                title="Dodawanie samochodu"
                caption="Dodaj samochód"
                handleSubmit={handleSubmit}
                showChangeScreen={false}
                fullScreen={true}
                disableOpenButton={false}
                ref={formDialog}
            >
                <form noValidate>
                    <Box sx={{display: "flex", width: "100%", flexDirection: "column", marginBottom: 5}}>
                        <Box sx={{display: "flex", width: "100%"}}>
                            <Box sx={{display: "flex", width: "50%", marginRight: 1}}>
                                <Tooltip 
                                    title="Nazwa będzie wyświetlana jako tytuł na liście." 
                                    key="name-tooltip"
                                    disableFocusListener={true}>
                                    <TextField
                                        InputLabelProps={{shrink: true}}
                                        error={!!modelErrors.name}
                                        helperText={modelErrors.name}
                                        variant="outlined"
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="name"
                                        label="Nazwa"
                                        name="name"
                                        value={createModel?.name}
                                        onChange={(name) => extractData("name", name)}
                                    />
                                </Tooltip>
                            </Box>
                            <Box sx={{display: "flex", width: "50%", marginLeft: 1}}>
                                <Tooltip
                                    title="Opis ten będzie się wyświetlał na liście samochodów."
                                    key="short-description-tooltip"
                                    disableFocusListener={true}>
                                    <TextField
                                        InputLabelProps={{shrink: true}}
                                        error={!!modelErrors.shortDescription}
                                        helperText={modelErrors.shortDescription}
                                        variant="outlined"
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="short-description"
                                        label="Krótki opis"
                                        name="short-description"
                                        value={createModel?.shortDescription}
                                        onChange={(shortDescription) => extractData("shortDescription", shortDescription)}
                                    />
                                </Tooltip>
                            </Box>
                        </Box>
                        <Tooltip 
                            title="Krótko opisz swój pojazd" 
                            key="description" 
                            disableFocusListener={true}>
                            <TextField
                                InputLabelProps={{shrink: true}}
                                error={!!modelErrors.description}
                                helperText={modelErrors.description}
                                variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                multiline
                                maxRows={5}
                                id="description"
                                label="Opis"
                                name="description"
                                value={createModel?.description}
                                onChange={(description) => extractData("description", description)}
                            />
                        </Tooltip>
                    </Box>
                    <SimpleInfiniteSelect
                        id="category"
                        label="Kategoria"
                        maxHeight="200px"
                        disabled={false}
                        fullWidth={true}
                        pageSize={10}
                        value={categoryId}
                        fetchData={fetchCategories}
                        data={categories?.source}
                        isLoading={isLoadingCategories}
                        onChangeValue={(selectedCategoryId) => 
                            {
                                setCategoryId(selectedCategoryId);
                                getData({
                                    categoryId: selectedCategoryId
                                });
                            }
                        }
                        totalCount={categories?.totalCount}
                    />
                    <Box sx={{display: "flex", width: '100%'}}>
                        <Box sx={{display: "flex", flexDirection: "column", width: '100%'}}>
                            {isEmpty(categoryAttributes) ? <></>:
                                <>
                                    <Box sx={{display: "flex", flexDirection: "row", width: '100%'}}>
                                        {categoryAttributes.slice(0, categoryAttributes.length / 2).map((m, index) => {
                                            return(
                                                <ExtendedControl 
                                                    id={m.id.toString()}
                                                    key={`${m.id.toString()}-${index}`}
                                                    label={m.name}
                                                    attributeControlType={m.attributeControlType}
                                                    multipleOptions={m.values?.sort((a, b) => a.displayOrder - b.displayOrder)}
                                                    onChangeValue={(value, type) => handleExtendedControl(value, m, type)}
                                                />
                                            )
                                        })}
                                    </Box>
                                    <Box sx={{display: "flex", flexDirection: "row", width: '100%'}}>
                                    {categoryAttributes.slice((categoryAttributes.length / 2), categoryAttributes.length).map((m, index) => {
                                            return(
                                                <ExtendedControl 
                                                    id={m.id.toString()}
                                                    key={`${m.id.toString()}-${index}`}
                                                    label={m.name}
                                                    attributeControlType={m.attributeControlType}
                                                    multipleOptions={m.values?.sort((a, b) => a.displayOrder - b.displayOrder)}
                                                    onChangeValue={(value, type) => handleExtendedControl(value, m, type)}
                                                />
                                            )
                                        })}
                                    </Box>
                                </>
                            }
                        </Box>
                    </Box>
                    <Box sx={{display: "flex", flexDirection: "row", width: "100%", marginTop: 10}}>
                        <Box sx={{display: "flex", flexDirection: "column", marginRight: 5, flexGrow: 1}}>
                            <Typography key="caption-main-image" variant="h6">
                                Główne zdjęcie: 
                            </Typography>
                            <ImagesDropzoneArea key="main-image-dropzone" setFiles={setMainImage} imageLimit={1}/>
                        </Box>
                        <Box sx={{display: "flex", flexDirection: "column",  flexGrow: 5}}>
                            <Typography key="caption-images" variant="h6">
                                Zdjęcia:
                            </Typography>
                            <ImagesDropzoneArea key="images-dropzone" setFiles={setImages}/>
                        </Box>
                    </Box>
                </form>
            </SubmitDialogForm>
        </>
    )
}) 

export default ProductAnnouncementDialogForm;
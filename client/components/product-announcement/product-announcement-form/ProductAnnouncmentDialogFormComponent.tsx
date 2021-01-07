import { Guid } from "guid-typescript";
import { forwardRef, useImperativeHandle, useRef, useState } from "react";
import useGetData from "../../../hooks/fetch/GetDataHook";
import useAuhtorizedPagedList from "../../../hooks/fetch/pagedAPI/AuthorizedPagedAPIHook";
import CategoryShortInfoView from "../../admin/categories/categories-list/models/CategoryShortInfoView";
import CategoryAttributeFullInfoView from "../../admin/categories/category-attributes/models/CategoryAttributeFullInfoView";
import ExtendedControl from "../../base/controls/ExtendedControlComponent";
import SubmitDialogForm from "../../base/form-dialog/SubmitDialogFormComponent";
import SimpleInfiniteSelect from "../../base/select/simple-infinite-select/SimpleInfiniteSelectComponent";

interface ProductAnnouncementDialogProps {

}

const ProductAnnouncementDialogForm = forwardRef((props, ref) => {
    const formDialog = useRef(null);

    const [categoryId, setCategoryId] = useState(Guid.createEmpty());
    const [fetchCategories, _, isLoadingCategories, categories] = useAuhtorizedPagedList<CategoryShortInfoView>("/v1/category/list");
    const [model, isLoading, getData] = useGetData<Array<CategoryAttributeFullInfoView>>("/v1/category/attribute/list", false);

    const handleSubmit = async () => {
        alert(categoryId);
        
        console.log(model);
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
                    {model.map(m => {
                        return(
                            <ExtendedControl 
                                id={m.id.toString()}
                                label={m.name}
                                attributeControlType={m.attributeControlType}
                                multipleOptions={m.values?.sort((a, b) => a.displayOrder - b.displayOrder)}
                                onChangeValue={(value) => alert(value)}
                            />
                        )
                    })}
                </form>
            </SubmitDialogForm>
        </>
    )
}) 

export default ProductAnnouncementDialogForm;
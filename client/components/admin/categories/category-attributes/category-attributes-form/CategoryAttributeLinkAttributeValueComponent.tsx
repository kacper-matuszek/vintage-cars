import { Guid } from "guid-typescript";
import { useRef, useState } from "react";
import useSendSubmitWithNotification from "../../../../../hooks/fetch/SendSubmitHook";
import useLocale from "../../../../../hooks/utils/LocaleHook";
import SubmitDialogForm from "../../../../base/form-dialog/SubmitDialogFormComponent";
import CategoryAttributeValueList from "../../category-attribute-values/category-attribute-values-list/CategoryAttributeValueListComponent";
import CategoryAttributeValueView from "../../category-attribute-values/models/CategoryAttributeValueView";
import CategoryAttributeLinkAttributeValueSendData from "../models/CategoryAttributeLinkAttributeValue";

interface ICategoryAttributeLinkAttributeValueProps {
    categoryAttributeName: string;
    categoryAttributeId: Guid;
    categoryId: Guid;
}
const CategoryAttributeLinkAttributeValue = (props: ICategoryAttributeLinkAttributeValueProps) => {
    const {categoryAttributeName, categoryAttributeId, categoryId} = props;
    const loc = useLocale('common', ['categories', 'category-attributes', 'link']);
    const formDialog = useRef(null);
    const [categoryAttributeValues, setCategoryAttributeValues] = useState<CategoryAttributeValueView[]>([]);
    const [send] = useSendSubmitWithNotification("/admin/v1/category/attribute-value/link");
    const prepareDataToSend = () => {
        const sendData = new CategoryAttributeLinkAttributeValueSendData();
        categoryAttributeValues.forEach(x => x.id = x.isNew ? null : x.id);
        sendData.categoryId = categoryId;
        sendData.categoryAttributeValues = categoryAttributeValues.map(x => ({...x, categoryAttributeId: categoryAttributeId}));
        return sendData;
    }
    const handleSubmit = async() => {
        await send(prepareDataToSend()).finally(() => {
            if(formDialog.current)
                formDialog.current.closeForm();
        });
    }
    return (
        <SubmitDialogForm
            title={loc.transQuery('title', { categoryAttributeName: categoryAttributeName})}
            handleSubmit={handleSubmit}
            caption={loc.trans('caption')}
            showLink={false}
            maxWidth="md"
            disableOpenButton={false}
            showChangeScreen={true}
            ref={formDialog}
        >
            <CategoryAttributeValueList
                categoryId={categoryId}
                categoryAttributeId={categoryAttributeId}
                onListChanged={(list) => setCategoryAttributeValues(list)}
            />
        </SubmitDialogForm>
    )
}
export default CategoryAttributeLinkAttributeValue;
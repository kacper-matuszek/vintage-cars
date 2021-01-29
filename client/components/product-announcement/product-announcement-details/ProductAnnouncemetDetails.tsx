import { Button, CardMedia, Divider, Grid, Typography } from "@material-ui/core";
import { Guid } from "guid-typescript";
import { forwardRef, useEffect, useImperativeHandle, useRef, useState } from "react";
import Picture from "../../../core/models/shared/Picture";
import { isEmpty } from "../../../core/models/utils/ObjectExtension";
import { isStringNullOrEmpty } from "../../../core/models/utils/StringExtension";
import useGetData from "../../../hooks/fetch/GetDataHook";
import ImageCarousel from "../../base/cards/image-carousel/ImageCarouselComponent";
import FormDialog from "../../base/FormDialogComponent";
import ProductAnnouncementDetail from '../models/ProductAnnouncementDetails'

const ProductAnnouncementDetails = forwardRef((props, ref) => {
    const formDialog = useRef(null);
    const actions = () => <Button variant="contained" onClick={() => formDialog.current.closeForm()}>Wróć</Button>

    const [data, _, fetchData] = useGetData<ProductAnnouncementDetail>('/v1/productannouncement', false, false);
    const [mainPicture, setMainPicute] = useState<Picture>(null);
    const [detailId, setDetailId] = useState<string>();
    const [mainPicSrc, setMainPicSrc] = useState<string>();
    const [images, setImages] = useState<Array<{title: string, imageData: string, imageMimeType: string}>>(new Array<{title: string, imageData: string, imageMimeType: string}>());
    const [attributes, setAttributes] = useState();

    useEffect(() => {
        const mainPic = data?.pictures?.filter(x => x.isMain)[0];
        setMainPicute(mainPic);
        const pictures = data?.pictures.map<{title: string, imageData: string, imageMimeType: string}>(x => {
            return {
                title: x.titleAttribute,
                imageData: x.dataAsBase64,
                imageMimeType: x.mimeType
            }
        });
        setImages(prevState => {
            if(!isEmpty(pictures) && pictures.length !== 0)
                prevState.push(...pictures);
            return prevState;
        });
    }, [data]);
    useEffect(() => {
        setMainPicSrc(`data:${mainPicture?.mimeType};base64,${mainPicture?.dataAsBase64}`);
    }, [mainPicture])
    useEffect(() => {
        (() => {
            if(isStringNullOrEmpty(detailId)) {
                return;
            }
            fetchData({
                productAnnouncementId: detailId
            });
        })();

    }, [detailId])
    useImperativeHandle(ref, () => ({
        openForm(id: Guid) {
            setDetailId(id.toString());
            formDialog.current.openForm();
        }
    }))
    return (
        <FormDialog
            showLink={false}
            disableOpenButton={true}
            fullScreen={true}
            showChangeScreen={false}
            showCancel={false}
            actions={actions()}
            ref={formDialog}
            title={`${data?.name} ${data?.shortDescription}`}
        >
            <Grid container style={{display: 'flex'}}>
                <Grid container style={{display: 'flex', flexFlow: 'row'}}>
                    <Grid container style={{display: 'flex', margin: 5}}>
                        <CardMedia
                            component="img"
                            src={mainPicSrc}
                        />
                        <Grid container style={{display: 'flex', flexShrink: 1, height: 230}}>
                            <ImageCarousel
                                animation="slide"
                                autoPlay={true}
                                amountImagesInSection={2}
                                images={data?.pictures.map<{title: string, imageData: string, imageMimeType: string}>(x => {
                                    return {
                                        title: x.titleAttribute,
                                        imageData: x.dataAsBase64,
                                        imageMimeType: x.mimeType
                                    }
                                }) || new Array<{title: string, imageData: string, imageMimeType: string}>()}
                            />
                        </Grid>
                    </Grid>
                    <Grid container style={{display: 'flex', justifyContent: 'center', margin: 5}}>
                        <Typography>
                            {data?.description}
                        </Typography>
                    </Grid>
                </Grid>
                <Typography variant="h6" style={{marginTop: 30}}>
                    Specyfikacja
                </Typography>
                <Divider style={{width: '100%'}}/>
                <Grid container style={{display: 'flex', width: '100%', flexDirection: 'column'}}>
                    <Grid style={{display: 'flex', width: '100%'}}>
                        <Grid style={{display: 'flex', width: '100%', justifyContent: 'flex-start'}}>
                            {isEmpty(data?.attributes) ? <></> : 
                                <><Grid style={{display: 'flex',  flexDirection: 'column', flexWrap: 'wrap', margin: 10}}>
                                    {data.attributes.slice(0, data.attributes.length / 2).map(x => {
                                            return (
                                                <Grid style={{display: 'flex', width: '100%', flexDirection: 'row'}}>
                                                    <Typography style={{fontWeight: 'bold', margin: 5}}>
                                                        {x.name}:
                                                    </Typography>
                                                    <Typography style={{margin: 5}}>
                                                        {x.value}
                                                    </Typography>
                                                </Grid>
                                        )})}
                                </Grid>
                                <Grid style={{display: 'flex', flexDirection: 'column', margin: 10, marginLeft: 50}}>
                                    {data.attributes.slice(data.attributes.length / 2, data.attributes.length).map(x => {
                                            return (
                                                <Grid style={{display: 'flex', width: '100%', flexDirection: 'row'}}>
                                                    <Typography style={{fontWeight: 'bold', margin: 5}}>
                                                        {x.name}:
                                                    </Typography>
                                                    <Typography style={{margin: 5}}>
                                                        {x.value}
                                                    </Typography>
                                                </Grid>
                                    )})}
                                </Grid></>
                            }
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </FormDialog>
    )
})
export default ProductAnnouncementDetails;
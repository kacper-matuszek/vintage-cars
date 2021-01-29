import { CardMedia, Grid } from "@material-ui/core"
import Carousel from "react-material-ui-carousel"
import { isEmpty } from "../../../../core/models/utils/ObjectExtension";
import useStyles from "./image-carousel-style";

interface ImageProps {
    title: string,
    imageData: string,
    imageMimeType: string,
}

interface ImageCarouselProps {
    autoPlay?: boolean,
    stopAutoPlayOnHover?: boolean,
    animation: "slide" | "fade",
    indicatorsVisible?: boolean,
    animationDuration?: number,
    navButtonsAlwaysVisible?: boolean,
    navButtonsAlwaysInvisible?: boolean
    images: Array<ImageProps>,
    amountImagesInSection?: number,
}

const ImageCarousel = (props: ImageCarouselProps) => {
    const classes = useStyles();
    const {autoPlay, stopAutoPlayOnHover, animation, indicatorsVisible, animationDuration, navButtonsAlwaysVisible, navButtonsAlwaysInvisible, images, amountImagesInSection} = props;
    const imagesInSection = isEmpty(amountImagesInSection) ? 3 : amountImagesInSection === 0 ? 1 : amountImagesInSection;
    const sections = Math.ceil(images.length / imagesInSection);
    const imageSectionIndexes = new Array(sections).fill(sections).map(_ => images.splice(0, imagesInSection))

    return (
        <Carousel
            autoPlay={isEmpty(autoPlay) ? false : autoPlay}
            stopAutoPlayOnHover={isEmpty(stopAutoPlayOnHover) ? true : stopAutoPlayOnHover}
            animation={animation}
            indicators={isEmpty(indicatorsVisible) ? false : indicatorsVisible}
            timeout={isEmpty(animationDuration) ? 500 : animationDuration}
            navButtonsAlwaysVisible={isEmpty(navButtonsAlwaysVisible) ? false : navButtonsAlwaysVisible}
            navButtonsAlwaysInvisible={isEmpty(navButtonsAlwaysInvisible) ? false: navButtonsAlwaysInvisible}
            fullHeightHover={true}
            className={classes.carousel}
        >
            {imageSectionIndexes.map((section, index) => {
                return (
                    <Grid container key={index} className={classes.containerCardMedia}>
                        {section.map((isi, index) =>{
                            return (
                                <CardMedia
                                    className={classes.cardMedia}
                                    style={{width: `${100/imagesInSection}%`}}
                                    title={isi.title}
                                    src={`data:${isi.imageMimeType};base64,${isi.imageData}`}
                                    component='img'
                                    key={index}
                                />
                            )
                        })}
                    </Grid>
                )
            })}
        </Carousel>
    )
}

export default ImageCarousel
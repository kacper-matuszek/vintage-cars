import { ReactNode } from "react";
import { Grid, CssBaseline } from "@material-ui/core";
import pictureStyle from "./picture-content-style";

const PictureContent = props => (
    <Grid container>
        <CssBaseline/>
        <Grid item xs={false} sm={4} md={5} className={pictureStyle().image}/>
        {props.children}
    </Grid>
);
export default PictureContent;
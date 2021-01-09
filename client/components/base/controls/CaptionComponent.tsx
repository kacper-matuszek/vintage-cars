import { Typography } from "@material-ui/core";
import useCaptionStyles from "./caption-component-style";

interface CaptionProps {
    text: string
}
const Caption = (props: CaptionProps) => 
<Typography variant="subtitle1" className={useCaptionStyles().root}>
    {props.text}
</Typography>

export default Caption;
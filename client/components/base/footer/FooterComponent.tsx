import { Box } from '@material-ui/core';
import {footerStyle} from './footer-style';

const Footer = (props) => {
    const classes = footerStyle()
    return (
        <Box className={classes.layoutFooter}>
            <Box className={classes.footerContent}>
                <div className={classes.div}>
                    {props.children}
                </div>
            </Box>
        </Box>
    )
}

export default Footer
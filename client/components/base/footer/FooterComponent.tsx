import { Box, Paper } from '@material-ui/core';
import {footerStyle} from './footer-style';

const Footer = (props) => {
    const classes = footerStyle()
    return (
        <Paper className={classes.layoutFooter} square>
            <Box className={classes.footerContent}>
                <Box className={classes.div}>
                    {props.children}
                </Box>
            </Box>
        </Paper>
    )
}

export default Footer
import { AppBar, Box, Divider, Paper, Tab, Tabs } from "@material-ui/core"
import { useStyles } from "../../../theme";
import { footerSectionStyle } from "./footer-section-style"

const FooterSection = (props) => {
    const classes = footerSectionStyle();
    const them = useStyles();
    return (
        <Paper className={classes.footerSection}>
            <AppBar position="static">
                <Tabs value={0}>
                    <Tab  label={props.label}  value={0}/>
                </Tabs>
            </AppBar>
            <Divider/>
            <Box className={classes.footerBox} component="span" sx={{display: "block"}}>
                {props.children}
            </Box>
        </Paper>
    )
}

export default FooterSection
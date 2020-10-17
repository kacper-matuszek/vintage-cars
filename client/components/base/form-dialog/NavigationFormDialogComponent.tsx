import { Box, Button, Dialog, DialogActions, DialogContent, DialogProps, Tab, Tabs } from "@material-ui/core"
import { useStyles } from "./navigation-form-dialog-style"
import TabPanel from "./tab-panel/TabPanelComponent"
import React from "react";
import { DialogTitle } from "./dialog-title/DialogTitleComponent";
import { IconWithContent } from "../../../core/models/base/IconWithContent";

export interface NavigationFormDialogProps {
    title: string,
    titleIsDynamic: boolean
    iconsWithContent: Array<IconWithContent>,
    open: boolean,
    onClose: () => void,
    showSave: boolean,
    onSave: () => void,
}
export const NavigationFormDialog = (props: NavigationFormDialogProps) => {
    const classes = useStyles();
    const [value, setValue] = React.useState(0);
    const [title, setTitle] = React.useState(!props.titleIsDynamic ? props.title : props.iconsWithContent[0].title);
    const {iconsWithContent} = props;
    /*HANDLERS*/
    const handleChange = (event, newValue) => {
        setValue(newValue);

        if(props.titleIsDynamic) 
        {
            setTitle(props.iconsWithContent[newValue].title);
        }
    }

    return (
        <div className={classes.root}>
            <Dialog open={props.open} fullWidth={true}>
                <DialogTitle id={"navigation-form-dialog"} onClose={props.onClose}>{title}</DialogTitle>
                <DialogContent dividers className={classes.dialogContent}>
                    <div className={classes.contentTab}>
                        <Tabs
                            value={value}
                            variant="scrollable"
                            orientation="vertical"
                            onChange={handleChange}
                            indicatorColor="primary"
                            textColor="secondary"
                            aria-label="icon label tabs"
                            className={classes.tabs}>
                            {props.iconsWithContent.map((icon, index) => (
                                <Tab icon={icon.icon} key={`${icon.title}-${index}`} className={classes.tab} fullWidth />
                            ))}
                        </Tabs> 
                        {iconsWithContent.map((icWithCont, index) => (
                            <TabPanel value={value} index={index} key={`Panel-${icWithCont.title}-${index}`}>
                                {icWithCont.content}
                            </TabPanel>
                        ))}
                    </div>
                </DialogContent>
                <DialogActions>
                    { props.showSave ? <Button onClick={props.onSave} color="primary" variant="contained">
                        Zapisz
                    </Button> : ''}
                </DialogActions>
            </Dialog>
        </div>
    )
}
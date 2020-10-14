import { Button, Dialog, DialogActions, DialogContent, Tab, Tabs } from "@material-ui/core"
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
    onSave: () => void
}
export const NavigationFormDialog = (props: NavigationFormDialogProps) => {
    const classes = useStyles();
    const [value, setValue] = React.useState(0);
    const [title, setTitle] = React.useState(props.title);
    
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
            <Dialog open={props.open}>
                <DialogTitle id={"navigation-form-dialog"} onClose={props.onClose}>{title}</DialogTitle>
                <DialogContent dividers>
                    <div className={classes.contentTab}>
                        <Tabs
                            value={value}
                            variant="scrollable"
                            orientation="vertical"
                            onChange={handleChange}
                            indicatorColor="secondary"
                            textColor="secondary"
                            aria-label="icon label tabs example"
                            className={classes.tabs}>
                            {props.iconsWithContent.map(icon => (
                                <Tab icon={icon.icon} />
                            ))}
                        </Tabs>
                        {props.iconsWithContent.map((icWithCont, index) => (
                            <TabPanel value={value} index={index}>
                                {icWithCont}
                            </TabPanel>
                        ))}
                    </div>
                </DialogContent>
                <DialogActions>
                    { props.showSave ? <Button onClick={props.onSave} color="primary">
                        Zapisz
                    </Button> : ''}
                </DialogActions>
            </Dialog>
        </div>
    )
}
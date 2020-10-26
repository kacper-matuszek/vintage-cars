import { Box, Button, createStyles, Dialog, DialogContent, DialogTitle, Drawer, List, ListItem, ListItemIcon, ListItemText, MenuItem, Tab, Tabs, Theme } from "@material-ui/core"
import MenuAppBar from "../../components/base/menu-bar/MenuAppBarComponent"
import { NameWithNode } from "../../core/models/base/NameWithNode"
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';
import SendIcon from '@material-ui/icons/Send'
import AppBase from "../../components/base/AppBaseComponent";
import Typography from '@material-ui/core/Typography';
import {useContext, useState} from "react";
import { useRouter } from "next/router";
import { makeStyles } from "@material-ui/styles";
import React from "react";
import { NavigationFormDialog } from "../../components/base/form-dialog/NavigationFormDialogComponent";
import { IconWithContent } from "../../core/models/base/IconWithContent";
import NavigationProfileDialog from "../../components/profile/NavigationProfileDialogComponent";
import NotificationContext from "../../contexts/NotificationContext";



const Home = (props) => {
    const router = useRouter();
    const [value, setValue] = React.useState(0);
    const [title, setTitle] = React.useState("Title");
    const [open, setOpen] = React.useState(false);
    const [showSave, setShowSave] = React.useState(true);
    const { showSuccessMessage } = useContext(NotificationContext);
    const accountChildren = () => {
        return (
            <MenuItem>
            <ListItemIcon>
                <SendIcon fontSize="small" />
            </ListItemIcon>
            <Typography variant="inherit">Profile</Typography>
            </MenuItem>
        )
    }
    const menuItem: [NameWithNode] = [
        { name: "Tytuł", children: <InboxIcon/> }
    ]
    const icon: Array<IconWithContent> = [
        {title: "Section 1", icon: <MailIcon/>, content: <div>Sekcja</div> },
        {title: "Section 2", icon: <MailIcon/>, content: <div>Sekcja 2</div> },
        {title: "Section 3", icon: <MailIcon/>, content: <div>Sekcja 3</div> },
        {title: "Section 4", icon: <MailIcon/>, content: <div>Sekcja 4</div> },
    ];
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);
    const handleSave = () => {}

    return (
        <div>
            <Button onClick={() => showSuccessMessage("hej")}>HEJ</Button>
           <NavigationProfileDialog
            open={open}
            onClose={handleClose}
            showSave={showSave}
            onSave={handleSave}
           />
        </div>
    )
}

export async function getStaticProps() {
    return {
        props: {
            title: "Home",
        }
    }
}

export default Home
import { Box, Button, createStyles, Dialog, DialogContent, CircularProgress, Backdrop, DialogTitle, Drawer, List, ListItem, ListItemIcon, ListItemText, MenuItem, Tab, Tabs, Theme } from "@material-ui/core"
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
import LoadingContext from "../../contexts/LoadingContext";
import CategoryAttributeList from "../../components/admin/categories/category-attributes/category-attributes-list/CategoryAttributeList";

const useStyles = makeStyles({
  parent: {
    position: "relative",
    width: 200,
    height: 200,
    backgroundColor: "red",
    zIndex: 0
  },
  backdrop: {
    position: "absolute"
  }
});

const Home = (props) => {
    const classes = useStyles();
    const router = useRouter();
    const [value, setValue] = React.useState(0);
    const [title, setTitle] = React.useState("Title");
    const [open, setOpen] = React.useState(false);
    const [showSave, setShowSave] = React.useState(true);
    const [loading, setLoading] = useState(false);
    const loadingContextValue = {showLoading: () => setLoading(true), hideLoading: () => setLoading(false)};
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
            {/* <LoadingContext.Provider value={loadingContextValue}>
                { loading ? <div className={classes.parent}>
                              <Backdrop className={classes.backdrop} open={true}>
                                <CircularProgress color="inherit" />
                              </Backdrop>
                            </div> : <></>}
                <LoadingContext.Consumer>
                    {({showLoading, hideLoading}) => (
            <><Button onClick={() => { 
                showLoading();
            }}>HEJ</Button>
           <NavigationProfileDialog
            open={open}
            onClose={handleClose}
            showSave={showSave}
            onSave={handleSave}
           /></>)}
           </LoadingContext.Consumer>
           </LoadingContext.Provider> */}
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
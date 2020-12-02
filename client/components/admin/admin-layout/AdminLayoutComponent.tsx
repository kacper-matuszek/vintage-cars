import { Box, Paper } from "@material-ui/core";
import React from "react"
import useOpenClose from "../../../hooks/utils/CloseHook";
import MenuAppBar from "../../base/menu-bar/MenuAppBarComponent"
import { layoutStyle } from "./admin-layout-style"
import PersonIcon from '@material-ui/icons/Person'
import { RouterWithElement } from "../../../core/models/base/NameWithNode";
import { generateLinkMenuItems, generateRouteMenuItems } from "../../shared/generatorUtils/GeneratorExtension";
import NavigationProfileDialog from "../../profile/NavigationProfileDialogComponent";
import { useRouter } from "next/router";
import CategoryIcon from '@material-ui/icons/Category';
import ViewModuleIcon from '@material-ui/icons/ViewModule';

const AdminLayout = (props) => {
    const classes = layoutStyle();
    const fontSize = "small";
    const [isNavigationProfileOpen, setNavigationProfileOpen, closeNavigationProfile] = useOpenClose();
    const router = useRouter();
    const accountMenu: Array<RouterWithElement> = [
        {name: "Profil", route: "", onClick: () => setNavigationProfileOpen(true), children: <PersonIcon fontSize={fontSize}/>}
    ]
    const sideBarItems: Array<RouterWithElement> = [
        {name: "Katalog", children: <CategoryIcon fontSize={fontSize}/>, collapseElements: [
            {name: "Atrybuty", route: "/admin/categories/attributes" ,children: <ViewModuleIcon fontSize={fontSize}/>},
            {name: "Kategorie", route: "/admin/categories", children: <CategoryIcon fontSize={fontSize}/>}
        ]}
    ]
    return(
        <React.Fragment>
            <MenuAppBar
                accountMenuChildren={generateRouteMenuItems(accountMenu)}
                listMenu={generateLinkMenuItems(sideBarItems)}
                isAuthorized={props.isAuthorized}
            />
            <Box className={classes.layoutContainer}>
                <Box className={classes.shadowBox}>
                    <Box className={classes.layoutContent}>
                        <Paper className={classes.paperBox} elevation={3}>
                            {props.children}
                            <NavigationProfileDialog
                                open={isNavigationProfileOpen}
                                onClose={closeNavigationProfile}
                            />
                        </Paper>
                    </Box>
                </Box>
            </Box>
        </React.Fragment>
    )
}

export default AdminLayout;
import React from 'react'
import MenuAppBar from "../menu-bar/MenuAppBarComponent"
import PersonIcon from '@material-ui/icons/Person'
import ExitToAppIcon from '@material-ui/icons/ExitToApp'
import { NameWithNode, RouterWithElement } from '../../../core/models/base/NameWithNode'
import DirectionsCarIcon from '@material-ui/icons/DirectionsCar';
import { generateMenuItems, generateLinkMenuItems } from '../../shared/generatorUtils/GeneratorExtension'
import Footer from '../footer/FooterComponent'
import { Box } from '@material-ui/core'
import { layoutStyle } from './main-layout-style'

const MainLayout = (props) => {
    const classes = layoutStyle();
    const fontSize = "small";
    const accountMenu: Array<NameWithNode> = [
        {name: "Profil", children: <PersonIcon fontSize={fontSize}/>},
        {name: "Wyloguj", children: <ExitToAppIcon fontSize={fontSize}/>}
    ]
    const sideBarItems: Array<RouterWithElement> = [
        {name: "Lista samochodów", route: "/login", children: <DirectionsCarIcon fontSize={fontSize}/>}
    ]
    
    return (
        <React.Fragment>
            <MenuAppBar
                accountMenuChildren={generateMenuItems(accountMenu)}
                listMenu={generateLinkMenuItems(sideBarItems)}
                isAuthorized={props.isAuthorized}
            ></MenuAppBar>
            <Box className={classes.layoutContainer}>
                <Box className={classes.layoutContent}>{props.children}</Box>
            </Box>
            <Footer></Footer>
        </React.Fragment>
    )
}

export default MainLayout
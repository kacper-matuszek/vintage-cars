import React from 'react'
import MenuAppBar from "../menu-bar/MenuAppBarComponent"
import PersonIcon from '@material-ui/icons/Person'
import ExitToAppIcon from '@material-ui/icons/ExitToApp'
import { NameWithNode, RouterWithElement } from '../../../core/models/base/NameWithNode'
import DirectionsCarIcon from '@material-ui/icons/DirectionsCar';
import { generateMenuItems, generateLinkMenuItems, generateRouteMenuItems } from '../../shared/generatorUtils/GeneratorExtension'
import Footer from '../footer/FooterComponent'
import { AppBar, Box, Fade, Paper, Typography } from '@material-ui/core'
import { layoutStyle } from './main-layout-style'
import FooterSection from '../footer/footer-section/FooterSectionComponent'

const MainLayout = (props) => {
    const classes = layoutStyle();
    const fontSize = "small";
    const accountMenu: Array<RouterWithElement> = [
        {name: "Profil", route: "/profile",children: <PersonIcon fontSize={fontSize}/>},
    ]
    const sideBarItems: Array<RouterWithElement> = [
        {name: "Lista samochodów", route: "/login", children: <DirectionsCarIcon fontSize={fontSize}/>}
    ]
    
    return (
        <React.Fragment>
            <MenuAppBar
                accountMenuChildren={generateRouteMenuItems(accountMenu)}
                listMenu={generateLinkMenuItems(sideBarItems)}
                isAuthorized={props.isAuthorized}
            ></MenuAppBar>
            <Box className={classes.layoutContainer}>
                <Box className={classes.shadowBox}>
                    <Box className={classes.layoutContent}>
                        <Paper className={classes.paperBox} elevation={3}>
                            {props.children}
                        </Paper>
                    </Box>
                </Box>
            </Box>
            <Footer>
                <FooterSection label="Media">
                    <Typography variant="body2" className={classes.footerTypography}>TEST</Typography>
                    <Typography variant="body2" className={classes.footerTypography}>TEST</Typography>
                </FooterSection>
                <FooterSection label="Kontakt">
                    <Typography variant="body2" className={classes.footerTypography}>tel. 821u312</Typography>
                    <Typography variant="body2" className={classes.footerTypography}>test adres</Typography>
                </FooterSection>
            </Footer>
        </React.Fragment>
    )
}

export default MainLayout
import React, { ReactNode } from "react";
import useStyles from "./menu-app-bar-style";
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import AccountCircle from '@material-ui/icons/AccountCircle';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import Menu from '@material-ui/core/Menu';
import Drawer from '@material-ui/core/Drawer';
import Divider from '@material-ui/core/Divider';
import List from '@material-ui/core/List';
import { useTheme } from '@material-ui/core/styles';
import clsx from 'clsx';
import { Box, Button, ListItemIcon, MenuItem } from "@material-ui/core";
import { ExtendedBox } from "../../shared/helperComponents/box-component";
import { ExitToApp } from "@material-ui/icons";
import Cookie from 'universal-cookie';
import CookieDictionary from "../../../core/models/settings/cookieSettings/CookieDictionary";
import { useRouter } from "next/router";

type Props = {
    isAuthorized?:boolean
    accountMenuChildren: ReactNode,
    listMenu: ReactNode
}
const MenuAppBar = ({isAuthorized, accountMenuChildren, listMenu}: Props) => {
    const classes = useStyles();
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const [leftOpen, setLeftOpen] = React.useState(false);
    const theme = useTheme();
    const router = useRouter();
    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
      };
    
    const handleClose = () => {
      setAnchorEl(null);
    };

    const handleDrawerOpen = () => {
        setLeftOpen(true);
    };
    
    const handleDrawerClose = () => {
        setLeftOpen(false);
    };

    const handleLogout = e => {
        e.preventDefault();
        new Cookie().remove(CookieDictionary.Token);
        router.push('/');
    }
    return (
      <React.Fragment>
      <AppBar position="fixed"
        className={clsx(classes.appBar, {
          [classes.appBarShift]: leftOpen
        })} id="app-bar">
        <Toolbar>
          <IconButton edge="start" className={clsx(classes.menuButton, leftOpen && classes.hide)} color="inherit" aria-label="menu"
            onClick={handleDrawerOpen}>
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" className={classes.title}>
            Your logo and company name
          </Typography>
          {isAuthorized ? (
            <div>
              <IconButton
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                onClick={handleMenu}
                color="inherit"
              >
                <AccountCircle />
              </IconButton>
              <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                open={open}
                onClose={handleClose}
              >
                {accountMenuChildren}
                <Divider/>
                <MenuItem key="logout" onClick={handleLogout}>
                    <ListItemIcon>
                        <ExitToApp />
                    </ListItemIcon>
                <Typography variant="inherit">Wyloguj</Typography>
                </MenuItem>
              </Menu>
            </div>
          ) : <Box className={classes.loginRegister}>
                <ExtendedBox>
                  <Button variant="outlined" href="./register">
                    Rejestracja
                  </Button>
                  <Button variant="contained" href="./login">
                    Logowanie
                  </Button>
                </ExtendedBox>
              </Box>}
        </Toolbar>
      </AppBar>
      <Drawer
        className={classes.drawer}
        variant="persistent"
        anchor="left"
        open={leftOpen}
        classes={{
          paper: classes.drawerPaper,
        }}
      >
        <div className={classes.drawerHeader}>
          <IconButton onClick={handleDrawerClose}>
            {theme.direction === 'ltr' ? <ChevronLeftIcon /> : <ChevronRightIcon />}
          </IconButton>
        </div>
        <Divider />
        <List>
          {listMenu}
        </List>
      </Drawer>
      </React.Fragment>
    )
}
export default MenuAppBar;
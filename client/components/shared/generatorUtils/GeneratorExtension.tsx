import React from 'react'
import { ListItem, ListItemIcon, ListItemText, MenuItem, Typography } from "@material-ui/core"
import { NameWithNode, RouterWithElement } from '../../../core/models/base/NameWithNode'
import { ListItemLink } from './ListItemLinkComponent';
import Link from "next/link";
import { generateUnique } from '../../../core/models/utils/Generator';
import isEmpty from '../../../core/models/utils/StringExtension';

export const generateMenuItems = (items: Array<NameWithNode>) => {
    return(
        <div>
         {items.map((nameWithIcon) => (
          <MenuItem key={generateUnique(nameWithIcon.name)}>
             <ListItemIcon>
                 {nameWithIcon.children}
             </ListItemIcon>
         <Typography variant="inherit">{nameWithIcon.name}</Typography>
         </MenuItem>
         ))}
        </div>
     )
 }

export const generateRouteMenuItems = (items: Array<RouterWithElement>) => {
    return(
        <div>
            {items.map((routerWithElement) => (
                !isEmpty(routerWithElement.route) ?
                <Link href={routerWithElement.route} key={generateUnique(routerWithElement.name)}>
                <MenuItem key={generateUnique(routerWithElement.name)}>
                    <ListItemIcon>
                        {routerWithElement.children}
                    </ListItemIcon>
                <Typography variant="inherit">{routerWithElement.name}</Typography>
                </MenuItem>
                </Link> : 
                <MenuItem key={generateUnique(routerWithElement.name)} onClick={routerWithElement.onClick}>
                <ListItemIcon>
                    {routerWithElement.children}
                </ListItemIcon>
                <Typography variant="inherit">{routerWithElement.name}</Typography>
                </MenuItem> 
            ))}
        </div>
    )
} 

 export const generateLinkMenuItems = (items: Array<RouterWithElement>) => {
     return (
         <React.Fragment>
             {items.map((router) => (
                 <ListItemLink to={router.route} primary={router.name} icon={router.children} key={generateUnique(router.name)} />
             ))}
         </React.Fragment>
     )
 }
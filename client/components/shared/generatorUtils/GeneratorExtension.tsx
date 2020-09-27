import React from 'react'
import { ListItemIcon, MenuItem, Typography } from "@material-ui/core"
import { NameWithNode, RouterWithElement } from '../../../core/models/base/NameWithNode'
import { ListItemLink } from './ListItemLinkComponent';

export const generateMenuItems = (items: Array<NameWithNode>) => {
    return(
     <React.Fragment>
         {items.map((nameWithIcon) => (
          <MenuItem>
             <ListItemIcon>
                 {nameWithIcon.children}
             </ListItemIcon>
         <Typography variant="inherit">{nameWithIcon.name}</Typography>
         </MenuItem>
         ))}
     </React.Fragment>
     )
 }

 export const generateLinkMenuItems = (items: Array<RouterWithElement>) => {
     return (
         <React.Fragment>
             {items.map((router) => (
                 <ListItemLink to={router.route} primary={router.name} icon={router.children} />
             ))}
         </React.Fragment>
     )
 }
import React, { useEffect, useState } from 'react';
import ListItem from '@material-ui/core/ListItem';
import Link from 'next/link'
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import { RouterWithElement } from '../../../core/models/base/NameWithNode';
import { Collapse, List } from '@material-ui/core';
import { generateLinkMenuItems } from './GeneratorExtension';
import { ExpandLess, ExpandMore } from '@material-ui/icons';
import isStringNullOrEmpty from '../../../core/models/utils/StringExtension';

interface ListItemLinkProps {
    icon?: React.ReactElement;
    primary: string;
    to: string;
    collapseElements: Array<RouterWithElement>;
}

export const ListItemLink = (props: ListItemLinkProps) => {
    const { icon, primary, to, collapseElements } = props;
    const [open, setOpen] = useState(false);
    const [isCurrent, setIsCurrent] = useState(false);
    const handleClick = () => {
      setOpen(!open);
    };

    const hasCollapseItems = () => 
        collapseElements !== undefined && collapseElements.length > 0;

    useEffect(() => {
      if(!isStringNullOrEmpty(to) && window.location.href.endsWith(to)) {
        setIsCurrent(true);
        return;
      }
      setIsCurrent(false);
    }, []);
    return (
        <>
          {!hasCollapseItems() ? 
            <Link href={to}>
              <ListItem button selected={isCurrent}>
                {icon ? <ListItemIcon>{icon}</ListItemIcon> : null}
                <ListItemText primary={primary} />
              </ListItem>
            </Link> : 
            <>
            <ListItem button onClick={handleClick}>
              {icon ? <ListItemIcon>{icon}</ListItemIcon> : null}
              <ListItemText primary={primary}/>
             {open ? <ExpandLess /> : <ExpandMore/>}
            </ListItem>
            <Collapse in={open} timeout="auto">
              <List component="div" disablePadding>
                {generateLinkMenuItems(collapseElements)}
              </List>
            </Collapse>
            </>
          }
        </>
    );
}
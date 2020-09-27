import React from 'react';
import ListItem from '@material-ui/core/ListItem';
import Link from 'next/link'
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';

interface ListItemLinkProps {
    icon?: React.ReactElement;
    primary: string;
    to: string;
}

export const ListItemLink = (props: ListItemLinkProps) => {
    const { icon, primary, to } = props;
  
    return (
        <li>
          <Link href={to}>
            <ListItem button>
              {icon ? <ListItemIcon>{icon}</ListItemIcon> : null}
              <ListItemText primary={primary} />
            </ListItem>
          </Link>
        </li>
    );
}
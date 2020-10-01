import { ListItemIcon, MenuItem } from "@material-ui/core"
import MenuAppBar from "../../components/base/menu-bar/MenuAppBarComponent"
import { NameWithNode } from "../../core/models/base/NameWithNode"
import InboxIcon from '@material-ui/icons/MoveToInbox';
import SendIcon from '@material-ui/icons/Send'
import AppBase from "../../components/base/AppBaseComponent";
import Typography from '@material-ui/core/Typography';
import {useState} from "react";
import { useRouter } from "next/router";
const Home = (props) => {
    const router = useRouter();
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
    const handleClickk = e => {
        e.preventDefault();
        router.push('/login');
    }
    return (
        <button onClick={handleClickk}>HEJ</button>
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
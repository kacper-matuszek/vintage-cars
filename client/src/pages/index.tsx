import { Divider, Grid } from "@material-ui/core";
import React from "react";
import ProductAnnouncementDialogForm from "../../components/product-announcement/product-announcement-form/ProductAnnouncmentDialogFormComponent";
import ProductAnnouncementList from "../../components/product-announcement/product-announcement-list/ProductAnnouncementListComponent";
import useIsRegistered from "../../hooks/authorization/IsRegisteredHook";

const Home = () => {
    const isRegistered = useIsRegistered();

    return (
        <>
            {isRegistered() ?
             <>
                <Grid container style={{display: 'flex', flexFlow: 'column', alignItems: 'flex-end', width: '100%'}}>
                    <ProductAnnouncementDialogForm/> 
                </Grid>
                <Divider style={{marginTop: 15, marginBottom: 5}}/>
             </> : <></>}
            <ProductAnnouncementList pageSize={10}/>
        </>
    )
}

export async function getStaticProps() {
    return {
        props: {
            title: "Strona główna",
        }
    }
}

export default Home
import React from "react";
import ProductAnnouncementList from "../../components/product-announcement/product-announcement-list/ProductAnnouncementListComponent";

const Home = () => {
    return (
        <ProductAnnouncementList pageSize={10}/>
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
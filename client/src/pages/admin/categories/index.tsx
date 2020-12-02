import CategoryList from "../../../../components/admin/categories/categories-list/CategoryListComponent";

const Categories = (props) => {
    return(
        <CategoryList/>
    )
}
export async function getStaticProps() {
    return {
        props: {
            title: "Kategorie",
        }
    }
}
export default Categories;
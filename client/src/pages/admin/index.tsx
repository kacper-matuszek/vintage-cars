const Admin = (props) => {
    return (
        <div>
            To jest adminka
        </div>
    )
}
export async function getStaticProps() {
    return {
        props: {
            title: "Administracja",
        }
    }
}
export default Admin
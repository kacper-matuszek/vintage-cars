import useTranslation from 'next-translate/useTranslation'

const Admin = (props) => {
    const { t, lang } = useTranslation('common');
    
    return (
        <div>
            {t('title')}
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
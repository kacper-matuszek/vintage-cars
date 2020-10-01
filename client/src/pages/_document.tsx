import Document, { Html, Main, NextScript } from 'next/document'
import { ServerStyleSheets } from '@material-ui/styles'
import Head from 'next/head';

class MyDocument extends Document {
    static async getInitialProps(ctx) {
        const sheet = new ServerStyleSheets();
        const originalRenderPage = ctx.renderPage;

        try{
            ctx.renderPage = () => originalRenderPage({
                enhanceApp: App => props => sheet.collect(<App {...props}/>)
            });

            const initialProps = await Document.getInitialProps(ctx);
            return { ...initialProps,
                styles: (
                    <>
                        {initialProps.styles}
                        {sheet.getStyleElement()}
                    </>
                )
            }
        } finally {
            ctx.renderPage(sheet)
        }

    }
    render() {
        return (
            <Html>
                <Head>
                </Head>
                <body style={{ margin: 0, padding: 0 }}>
                    <Main />
                    <NextScript />
                </body>
            </Html>
    )}
}

export default MyDocument;
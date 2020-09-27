import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";

export const footerStyle = makeStyles((theme: Theme) =>
createStyles({
       layoutFooter: {
       position: 'relative',
       display: 'flex',
       justifyContent: 'center',
       flexDirection: 'row',
       flexWrap: "wrap",
       width: '100%'
    }
    ,
    footerContent: {
       display: 'flex',
       justifyContent: 'center',
       flexDirection: 'column',
       width: '80%',
       minHeight: '8vh',
       margin: '0 auto',
    },
    div: {
           display: 'flex',
           flexFlow: 'row wrap',
           justifyContent: 'center'
    }
}))
  
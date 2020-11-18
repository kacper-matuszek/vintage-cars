import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";

export const footerStyle = makeStyles((theme: Theme) =>
createStyles({
       layoutFooter: {
       position: 'relative',
       display: 'flex',
       justifyContent: 'center',
       flexDirection: 'row',
       flexWrap: "wrap",
       width: '100%',
       height: '30vh',
    }
    ,
    footerContent: {
       display: 'flex',
       justifyContent: 'center',
       flexDirection: 'column',
       width: '90%',
       minHeight: '8vh',
       margin: '0',
       padding: '2vh',
    },
    div: {
           display: 'flex',
           flexFlow: 'row',
           flexWrap: 'wrap',
           justifyContent: 'space-evenly'
    }
}))
  
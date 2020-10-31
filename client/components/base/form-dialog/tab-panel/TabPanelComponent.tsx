import { Box } from "@material-ui/core";
import withLoading from "../../loading/LoadingComponent";
import { useStyles } from "./tab-panel-style";
import React from 'react'

const TabPanel = (props) => {
   const { children, value, index, showLoading, hideLoading, ...other } = props;
   const classes = useStyles();
   const {childrenProps} = children.props;
   const childWithProps = React.Children.map(props.children, child => {
     const props = {showLoading, hideLoading, ...childrenProps};
     if(React.isValidElement(child)) {
         return React.cloneElement(child, props);
     }
 });
   return (
     <div
       role="tabpanel"
       hidden={value !== index}
       id={`vertical-tabpanel-${index}`}
       aria-labelledby={`vertical-tab-${index}`}
       className={classes.root}
       {...other}
     >
       {value === index && (
         <Box p={3} className={classes.box}>
           {childWithProps}
         </Box>
       )}
     </div>
   );
 }
export default withLoading(TabPanel);
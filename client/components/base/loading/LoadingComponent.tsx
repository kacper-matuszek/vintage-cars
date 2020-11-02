import { Backdrop, Box, CircularProgress, Fade } from '@material-ui/core';
import React, { useState } from 'react';
import LoadingContext from '../../../contexts/LoadingContext';
import useStyles from './loading-style';
const LoadingComponent = (props) => {
    const classes = useStyles();
    const {childProps} = props;
    const [loading, setLoading] = useState(false);
    const loadingContextValue = {showLoading: () => setLoading(true), hideLoading: () => setLoading(false)};
    const childWithProps = React.Children.map(props.children, child => {
        const props = {...loadingContextValue, ...childProps};
        if(React.isValidElement(child)) {
            return React.cloneElement(child, props);
        }
    });
    return (
        <LoadingContext.Provider value={loadingContextValue}>
            <div className={classes.parent}>
           {loading ? 
                <Backdrop className={classes.backdrop} open={true}>
                  <CircularProgress className={classes.circular}/>
                </Backdrop> : <></>
                }
                {childWithProps}
            </div>
        </LoadingContext.Provider>
    );
}
const withLoading = (Component) => {
    return (props) => (
        <LoadingComponent childProps={props}>
            <Component/>
        </LoadingComponent>
    );
};
export default withLoading;
import { createContext } from "react";

const LoadingContext = createContext({
    showLoading: () => {},
    hideLoading: () => {}
})

export default LoadingContext;
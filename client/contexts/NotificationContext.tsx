import { createContext } from "react";

const NotificationContext = createContext({
    showSuccessMessage: (message: string) => {},
    showErrorMessage: (message: string) => {},
    showWarningMessage: (message: string) => {},
})

export default NotificationContext;
import { useEffect } from "react"

const useLoading = <T extends object>(isLoading: Array<boolean>, showLoading: () => void, hideLoading: () => void) => {
    useEffect(() => {
        if(isLoading.some(l => l))
            showLoading();
        else 
            hideLoading();
    }, [...isLoading])
}
export default useLoading;
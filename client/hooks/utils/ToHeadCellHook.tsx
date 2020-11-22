import { useEffect, useState } from "react";
import { HeadCell } from "../../components/base/table-list/table-head/HeadCell";
import React from "react";

const useToHeadCell = <T extends object>(jsxElement: JSX.Element[]): HeadCell<T>[] => {
    const [headCells, setHeadCells] = useState<HeadCell<T>[]>([]);

    useEffect(() => {
        React.Children.map(jsxElement, child => {
            const {name, headerName} = child.props
            setHeadCells(prevState => {
                const list = new Array<HeadCell<T>>();
                list.push(...prevState);
                list.push({id: name, label: headerName });
                return list; 
            });
        })
    }, []);

    return headCells;
}
export default useToHeadCell;
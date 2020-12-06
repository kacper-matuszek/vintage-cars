import { useEffect, useState } from "react";
import { CellContent } from "../../components/base/table-list/table-head/HeadCell";
import React from "react";

const useToCellContent = (jsxElement: JSX.Element[]): CellContent[] => {
    const [cellContent, setCellContent] = useState<CellContent[]>([]);

    useEffect(() => {
        React.Children.map(jsxElement, child => {
            const {name, content} = child.props;
            setCellContent(prevState => {
                const list = new Array<CellContent>();
                list.push(...prevState);
                list.push({id: name, content: content});
                return list;
            })
        })
    }, []);

    return cellContent;
}
export default useToCellContent;
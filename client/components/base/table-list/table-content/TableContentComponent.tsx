interface TableContentProps {
    name: string;
    headerName: string;
    content?: (model: any) => JSX.Element;
}

const TableContent = (props: TableContentProps) => {
    const {name, headerName, content} = props;

    return <></>
}

export default TableContent;
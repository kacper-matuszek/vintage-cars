export interface HeadCell<T> {
    id: keyof T,
    label: string
}
export interface SimpleHeadCell {
    label: string,
    visible: boolean,
    width?: string, //width in px,
    add: boolean
}
export interface CellContent {
    id: string,
    content: (data: any) => JSX.Element,
}
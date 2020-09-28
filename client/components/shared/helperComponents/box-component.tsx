import { Box } from "@material-ui/core"
import { BaseProps } from "../../../core/models/base/BaseProps";
import { columnStyle } from "./box-component-style"
interface Props extends BaseProps {
    column?: boolean
}
export const ExtendedBox = ({column = false, children}: Props) => {
    const classes = columnStyle();
    return (
        <Box className={column ? classes.column : classes.row}>{children}</Box>
    )
}
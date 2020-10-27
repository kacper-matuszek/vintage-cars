import { Box } from "@material-ui/core";
import { useStyles } from "./tab-panel-style";

interface TabPanelProps {
    children?: React.ReactElement;
    index: any;
    value: any;
  }
  
 export default function TabPanel(props: TabPanelProps) {
    const { children, value, index, ...other } = props;
    const classes = useStyles();
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
            {children}
          </Box>
        )}
      </div>
    );
  }
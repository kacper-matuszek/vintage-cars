import { makeStyles, createStyles } from '@material-ui/core/styles';

const pictureStyle = makeStyles((theme) => 
createStyles ({
    image: {
        backgroundImage: 'url(/static/PorscheWallpaper.jpg)',
        backgroundRepeat: 'no-repeat',
        backgroundColor:
          theme.palette.mode === 'light' ? theme.palette.grey[50] : theme.palette.grey[900],
        backgroundSize: 'cover',
        backgroundPosition: 'center',
    },
}));
export default pictureStyle;
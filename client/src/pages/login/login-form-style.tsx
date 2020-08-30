import { makeStyles } from '@material-ui/core/styles';

const loginStyle = makeStyles((theme) => ({
    image: {
        backgroundImage: 'url(/static/PorscheWallpaper.jpg)',
        backgroundRepeat: 'no-repeat',
        backgroundColor:
          theme.palette.type === 'light' ? theme.palette.grey[50] : theme.palette.grey[900],
        backgroundSize: 'cover',
        backgroundPosition: 'center',
    },
}))

export default loginStyle
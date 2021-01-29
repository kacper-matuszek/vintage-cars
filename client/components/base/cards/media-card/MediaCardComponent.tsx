import useStyles from "./media-card-style";
import React from 'react';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';

interface MediaCardProps {
    title: string,
    description: string,
    imageData: string,
    imageMimeType: string,
    onClick?: () => void
}

const MediaCard = (props: MediaCardProps) => {
    const classes = useStyles();
    const { title, description, imageData, imageMimeType, onClick } = props;
    const handleClick = (e) => {
      e.preventDefault();
      onClick();
    }
    return (
      <Card className={classes.root} elevation={5} onClick={handleClick}>
      <CardActionArea>
        <CardMedia
          className={classes.media}
          component='img'
          src={`data:${imageMimeType};base64,${imageData}`}
          title={title}
        />
        <CardContent className={classes.cardContent}>
          <Typography gutterBottom variant="h5" component="h2">
            {title}
          </Typography>
          <Typography variant="body2" color="textPrimary" component="p" style={{fontStyle: "italic"}}>
            {description}
          </Typography>
        </CardContent>
      </CardActionArea>
    </Card>
    )
}

export default MediaCard;
import useStyles from "./media-card-style";
import React from 'react';
import Card from '@material-ui/core/Card';
import CardActionArea from '@material-ui/core/CardActionArea';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import Typography from '@material-ui/core/Typography';

interface MediaCardProps {
    title: string,
    description: string;
    imageData: string
}

const MediaCard = (props: MediaCardProps) => {
    const classes = useStyles();
    const { title, description, imageData } = props;

    return (
      <Card className={classes.root}>
      <CardActionArea>
        <CardMedia
          className={classes.media}
          component='img'
          src={imageData}
          title={title}
        />
        <CardContent>
          <Typography gutterBottom variant="h5" component="h2">
            {title}
          </Typography>
          <Typography variant="body2" color="textSecondary" component="p">
            {description}
          </Typography>
        </CardContent>
      </CardActionArea>
    </Card>
    )
}

export default MediaCard;
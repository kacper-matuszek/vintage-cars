import { Container, Grid, Paper, TextField } from "@material-ui/core";
import { profileSectionStyle } from "./profile-section-style";

const ProfileSection = (props) => {
    const classes = profileSectionStyle();
    return (
        <Container>
            <form className={classes.form} noValidate method="POST">
                <TextField
                error={!!props.errors.firstName}
                variant="outlined"
                margin="normal"
                required
                fullWidth
                id="firstName"
                label="Imię"
                name="firstName"
                autoComplete="firstName" />
                <TextField
                error={!!props.errors.lastName}
                variant="outlined"
                margin="normal"
                required
                fullWidth
                id="lastName"
                label="Nazwisko"
                name="lastName"
                autoComplete="lastName" />
                <TextField
                error={!!props.errors.phoneNumber}
                variant="outlined"
                margin="normal"
                fullWidth
                required
                id="phoneNumber"
                label="Numer telefonu"
                name="phoneNumber"
                autoComplete="phoneNumber" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="company"
                label="Nazwa firmy"
                name="company"
                autoComplete="company" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="city"
                label="Miejscowość"
                name="city"
                autoComplete="city" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="address"
                label="Adres"
                name="address"
                autoComplete="address" />
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="postalCode"
                label="Kod pocztowy"
                name="postalCode"
                autoComplete="postalCode" />
            </form>
        </Container>
    )
}

export default ProfileSection;
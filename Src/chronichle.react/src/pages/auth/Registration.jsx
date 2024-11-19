import { useState } from "react";
import { Link } from "react-router-dom";
import {
    Box,
    IconButton,
    OutlinedInput,
    InputLabel,
    InputAdornment,
    FormControl,
    Typography,
    Button,
    TextField,
    Card,
    CardContent,
    Grid,
} from "@mui/material";
import { VisibilityOff, Visibility } from "@mui/icons-material";
import LoginIcon from "@mui/icons-material/Login";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import { useAuth } from "../../hooks/useAuth";

function Registration() {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);

    const { mutate } = useAuth(
        ["registration"],
        "/api/Identity/Register",
        "register"
    );

    const handleShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        mutate({ firstName, lastName, email, userName, password });
    };

    return (
        <Grid
            container
            justifyContent="center"
            alignItems="center"
        >
            <Card elevation={3} sx={{ width: 400, padding: 3, backgroundColor: "#f3eceb" }}>
                <CardContent>
                    <Typography
                        variant="h5"
                        component="h1"
                        textAlign="center"
                        fontWeight="bold"
                        gutterBottom
                    >
                        Create an Account
                    </Typography>
                    <Typography
                        variant="body2"
                        color="textSecondary"
                        textAlign="center"
                        fontWeight="bold"
                        gutterBottom
                    >
                        Register to get started
                    </Typography>
                    <Box component="form" mt={2} onSubmit={handleSubmit}>
                        <TextField
                            required
                            fullWidth
                            label="First Name"
                            value={firstName}
                            onChange={(e) => setFirstName(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                            color="black"
                        />
                        <TextField
                            required
                            fullWidth
                            label="Last Name"
                            value={lastName}
                            onChange={(e) => setLastName(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                            color="black"
                        />
                        <TextField
                            required
                            fullWidth
                            label="Email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                            color="black"
                        />
                        <TextField
                            required
                            fullWidth
                            label="User Name"
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)}
                            variant="outlined"
                            sx={{ mb: 2 }}
                            color="black"
                        />
                        <FormControl
                            fullWidth
                            variant="outlined"
                            sx={{ mb: 3 }}
                            color="black"
                        >
                            <InputLabel htmlFor="password">Password</InputLabel>
                            <OutlinedInput
                                id="password"
                                type={showPassword ? "text" : "password"}
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                endAdornment={
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleShowPassword}
                                            onMouseDown={handleMouseDownPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                }
                                label="Password"
                            />
                        </FormControl>
                        <Button
                            type="submit"
                            variant="outlined"
                            color="black"
                            fullWidth
                            sx={{ mb: 2, textTransform: "none", fontWeight: "bold" }}
                            
                        >
                            Register
                        </Button>
                        <Typography variant="body2" textAlign="center">
                            Already have an account?{" "}
                            <Link
                                to="/auth"
                                style={{ textDecoration: "none", color: "#1976d2" }}
                            >
                                Login
                            </Link>
                        </Typography>
                    </Box>
                </CardContent>
            </Card>
        </Grid>
    );
}

export default Registration;

import "./auth.css";
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
import { useAuth } from "../../hooks/useAuth";

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);

    const { mutate } = useAuth(["login"], "/api/Identity/Login", "login");

    const handleShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        mutate({ email, password });
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
                        Welcome Back
                    </Typography>
                    <Typography
                        variant="body2"
                        color="textSecondary"
                        textAlign="center"
                        fontWeight="bold"
                        gutterBottom
                    >
                        Login to your account
                    </Typography>
                    <Box component="form" mt={2} onSubmit={handleSubmit}>
                        <FormControl fullWidth sx={{ mb: 2 }}>
                            <TextField
                                required
                                label="Email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                variant="outlined"
                                color="black"
                            />
                        </FormControl>
                        <FormControl fullWidth variant="outlined" sx={{ mb: 3 }} color="black">
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
                            Login
                        </Button>
                        <Typography variant="body2" textAlign="center">
                            Don't have an account?{" "}
                            <Link
                                to="/auth/registration"
                                style={{ textDecoration: "none", color: "#1976d2" }}
                            >
                                Register
                            </Link>
                        </Typography>
                    </Box>
                </CardContent>
            </Card>
        </Grid>
    );
}

export default Login;

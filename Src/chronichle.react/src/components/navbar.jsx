import React, { useContext } from "react";
import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import AuthContext from "../context/AuthContext";

const Navbar = () => {
    const { user, logout } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate("/login");
    };

    return (
        <AppBar sx={{ bgcolor: "black" }}>
            <Toolbar>
                <Typography variant="h6" component={Link} to="/" sx={{ textDecoration: "none", color: "inherit" }}>
                    Chronicle
                </Typography>
                <Box>
                    {!user ? (
                        <>
                            <Button component={Link} to="/login" color="inherit">
                                Login
                            </Button>
                            <Button component={Link} to="/register" color="inherit">
                                Register
                            </Button>
                        </>
                    ) : (
                        <>
                            <Typography variant="body1" sx={{ marginRight: 2 }}>
                                Welcome, {user.email}
                            </Typography>
                            <Button color="inherit" onClick={handleLogout}>
                                Logout
                            </Button>
                        </>
                    )}
                </Box>
            </Toolbar>
        </AppBar>
    );
};

export default Navbar;

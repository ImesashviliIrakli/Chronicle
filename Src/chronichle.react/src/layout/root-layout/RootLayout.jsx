import "./root-layout.css";
import React, { useState } from "react";
import { NavLink, Outlet, useNavigate } from "react-router-dom";
import { AccountCircle } from "@mui/icons-material";
import LogoIcon from "../../assets/icons/logoIcon";
import { Fab, AppBar, Box, Toolbar, IconButton, Typography, Menu, Button, MenuItem, Container } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import useScrollTrigger from "@mui/material/useScrollTrigger";
import Fade from "@mui/material/Fade";

const settings = ["Account", "Logout"];

function RootLayout(props) {
    const [anchorElNav, setAnchorElNav] = useState(null);
    const [anchorElUser, setAnchorElUser] = useState(null);

    const auth = sessionStorage.getItem("login");
    const navigate = useNavigate();

    const handleOpenNavMenu = (event) => {
        setAnchorElNav(event.currentTarget);
    };
    const handleOpenUserMenu = (event) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = (e) => {
        setAnchorElUser(null);
        if (e.target.innerText === "Logout") {
            sessionStorage.clear();
            navigate("/");
        }
    };

    function ScrollTop(props) {
        const { children, window } = props;
        const trigger = useScrollTrigger({
            target: window ? window() : undefined,
            disableHysteresis: true,
            threshold: 100,
        });

        const handleClick = (event) => {
            const anchor = (event.target.ownerDocument || document).querySelector("#top");
            if (anchor) {
                anchor.scrollIntoView({ block: "center" });
            }
        };

        return (
            <Fade in={trigger}>
                <Box
                    onClick={handleClick}
                    role="presentation"
                    sx={{ position: "fixed", bottom: 16, right: 16 }}
                >
                    {children}
                </Box>
            </Fade>
        );
    }

    return (
        <div className="root-layout" id="top">
            <AppBar sx={{ bgcolor: "#f3eceb", color: "black" }}>
                <Container maxWidth="xl">
                    <Toolbar disableGutters>
                        <LogoIcon />
                        <Typography
                            variant="h6"
                            component="a"
                            onClick={() => navigate("/tasks")}
                            sx={{
                                mr: 2,
                                display: { xs: "none", md: "flex" },
                                fontFamily: "monospace",
                                fontWeight: 700,
                                letterSpacing: ".1rem",
                                color: "inherit",
                                textDecoration: "none",
                                cursor: "pointer",
                            }}
                        >
                            Chronicle
                        </Typography>
                        <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
                            <IconButton
                                size="large"
                                aria-label="menu"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleOpenNavMenu}
                                color="inherit"
                            >
                                <MenuIcon />
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorElNav}
                                anchorOrigin={{
                                    vertical: "bottom",
                                    horizontal: "left",
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: "top",
                                    horizontal: "left",
                                }}
                                open={Boolean(anchorElNav)}
                                onClose={handleCloseNavMenu}
                                sx={{ display: { xs: "block", md: "none" } }}
                            >
                                <MenuItem onClick={handleCloseNavMenu}>
                                    <NavLink
                                        to="/tasks"
                                        style={{
                                            textDecoration: "none",
                                            fontWeight: 500,
                                            fontSize: "1.2rem"
                                        }}
                                    >
                                        Tasks
                                    </NavLink>
                                </MenuItem>
                            </Menu>
                        </Box>
                        <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
                            <NavLink
                                to="/tasks"
                                style={{
                                    textDecoration: "none",
                                    color: "black",
                                    marginRight: "16px",
                                    fontWeight: 600,
                                    fontSize: "1.2rem",
                                }}
                            >
                                Tasks
                            </NavLink>
                        </Box>
                        {auth === "true" ? (
                            <Box>
                                <IconButton
                                    size="large"
                                    aria-label="user menu"
                                    aria-controls="menu-appbar"
                                    aria-haspopup="true"
                                    onClick={handleOpenUserMenu}
                                    color="inherit"
                                >
                                    <AccountCircle />
                                </IconButton>
                                <Menu
                                    id="menu-appbar"
                                    anchorEl={anchorElUser}
                                    anchorOrigin={{
                                        vertical: "top",
                                        horizontal: "right",
                                    }}
                                    keepMounted
                                    transformOrigin={{
                                        vertical: "top",
                                        horizontal: "right",
                                    }}
                                    open={Boolean(anchorElUser)}
                                    onClose={handleCloseUserMenu}
                                >
                                    {settings.map((setting) => (
                                        <MenuItem
                                            key={setting}
                                            onClick={(e) => handleCloseUserMenu(e)}
                                            sx={{ fontWeight: 500 }}
                                        >
                                            {setting}
                                        </MenuItem>
                                    ))}
                                </Menu>
                            </Box>
                        ) : (
                            <Button
                                color="inherit"
                                onClick={() => navigate("/auth")}
                                sx={{ fontWeight: 600, fontSize: "1rem" }}
                            >
                                Login
                            </Button>
                        )}
                    </Toolbar>
                </Container>
            </AppBar>

            <div className="outlet-container">
                <Outlet />
            </div>

            <ScrollTop {...props}>
                <Fab size="small" aria-label="scroll back to top">
                    <KeyboardArrowUpIcon />
                </Fab>
            </ScrollTop>
        </div>
    );
}

export default RootLayout;

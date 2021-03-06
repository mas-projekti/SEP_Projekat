import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import {
  Link,
  useNavigate
} from "react-router-dom";




export default function NavBar() {
  const history = useNavigate()

  const logOut = event =>{
    localStorage.removeItem('client-token')
    history('/login')
  }
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Payment Service Provider
          </Typography>
          {localStorage.getItem('client-token') == null ? (
            <>
          <Button component={Link} to="/login" color="inherit">Login</Button>
          <Button component={Link} to="/register" color="inherit">Register</Button></>
          ) :(
            <Button color="inherit" onClick={logOut}>Log out</Button>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
}
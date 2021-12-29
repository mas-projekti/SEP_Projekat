import { Navbar, Nav } from "react-bootstrap"
import logo from "../img/cart_logo_inv.jpg"
import profileLogo from "../img/profile_logo.png"
import lock from "../img/lock_inv.jpg"
import { NavLink, useHistory } from "react-router-dom"
import { useEffect, useState } from "react"
import PubSub from "pubsub-js"
import jwtDecode from "jwt-decode"
// import { MDBInput, MDBCol } from "mdbreact";


const MainBar = () => {
    var history = useHistory();
    const [ isLoggedIn, setIsLoggedIn ] = useState(false);
    const [ username, setUsername ] = useState(null);

    useEffect(() => {
        var pubSubToken = PubSub.subscribe(`LoginEvent`, updateUser);

        return () => {
            PubSub.unsubscribe(pubSubToken);
        }
    })

    let updateUser = () => {
        console.log('USAO');
        let token = localStorage.getItem(`jwt`);
        console.log(token);
        if (token === null) return;
        let decodedToken = jwtDecode(token);
        console.log(decodedToken);
        setUsername(decodedToken[`http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name`]);
        setIsLoggedIn(true);
        console.log(username);
    }

    let logout = () =>  {
        localStorage.removeItem(`jwt`);
        setUsername(``);
        setIsLoggedIn(false);
        history.push(`/login`);
    }


    return (
        <Navbar bg="dark" 
                variant="dark" 
                style={{maxHeight:'50px'}} 
                className="shadow-lg p-4">
                <Navbar.Brand >
                    <NavLink to="/">
                        <img
                            alt={"Logo"}
                            src={logo}
                            width="30"
                            height="30"
                            className="d-inline-block align-top mx-2"/>
                        WebShop MAS
                    </NavLink>
                </Navbar.Brand>
                <Navbar.Collapse id="responsive-navbar-nav ">
                </Navbar.Collapse>
                <Nav>
                    <NavLink to="/about">
                        About
                    </NavLink>
                </Nav>
                    {isLoggedIn ?
                        (<Nav>
                            <NavLink to="#profile" className="d-flex mt-2">
                                <img alt={"Profile Logo"}
                                src={profileLogo}
                                width="30"
                                height="30"
                                className="d-inline-block align-top mx-2"/>
                                <h5>{username}</h5>
                            </NavLink>
                            <button className="btn btn-secondary mx-2" onClick={logout}>
                                Logout
                            </button>
                        </Nav>)
                        :
                        (<Nav>
                            <NavLink to="/login" className="mx-2">
                                <img alt={"Lock"}
                                src={lock}
                                width="25"
                                height="25"
                                className="d-inline-block align-top mx-1" style={{marginRight:"5px"}}/>
                                Login
                            </NavLink>
                            <NavLink to="/register" className="mx-2">
                                Register
                            </NavLink>
                        </Nav>)
                    }
        </Navbar>
    )
}



export default MainBar

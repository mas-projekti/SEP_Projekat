import { Navbar, Nav } from "react-bootstrap"
import logo from "../img/cart_logo_inv.jpg"
import profileLogo from "../img/profile_logo.png"
import lock from "../img/lock_inv.jpg"
import { NavLink } from "react-router-dom"
// import { MDBInput, MDBCol } from "mdbreact";


const MainBar = () => {
    const isLoggedIn= false;
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
                            <NavLink to="#profile">
                                <img alt={"Profile Logo"}
                                src={profileLogo}
                                width="30"
                                height="30"
                                className="d-inline-block align-top mx-2"/>
                                Profile
                            </NavLink>
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

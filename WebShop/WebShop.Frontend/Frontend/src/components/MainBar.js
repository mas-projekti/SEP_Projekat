import { Navbar, Nav } from "react-bootstrap"
import logo from "../img/cart_logo_inv.jpg"
import profileLogo from "../img/profile_logo.png"
import lock from "../img/lock_inv.jpg"
// import { MDBInput, MDBCol } from "mdbreact";


const MainBar = () => {
    const isLoggedIn= false;
    return (
        <Navbar bg="dark" 
                variant="dark" 
                style={{maxHeight:'50px'}} 
                className="shadow-lg p-4">
                <Navbar.Brand href="/" >
                    <img
                    alt={"Logo"}
                    src={logo}
                    width="30"
                    height="30"
                    className="d-inline-block align-top mx-2"/>
                WebShop MAS
                </Navbar.Brand>
                <Navbar.Collapse id="responsive-navbar-nav ">
                    
                </Navbar.Collapse>
                <Nav>
                    <Nav.Link href="/about">
                        About
                    </Nav.Link>
                </Nav>
                    {isLoggedIn ?
                        (<Nav>
                            <Nav.Link href="#profile">
                                <img alt={"Profile Logo"}
                                src={profileLogo}
                                width="30"
                                height="30"
                                className="d-inline-block align-top mx-2"/>
                                Profile
                            </Nav.Link>
                        </Nav>)
                        :
                        (<Nav>
                            <Nav.Link href="/login">
                                <img alt={"Lock"}
                                src={lock}
                                width="30"
                                height="30"
                                className="d-inline-block align-top mx-2"/>
                                Login
                            </Nav.Link>
                            <Nav.Link href="/register">
                                Register
                            </Nav.Link>
                        </Nav>)
                    }
        </Navbar>
    )
}

export default MainBar

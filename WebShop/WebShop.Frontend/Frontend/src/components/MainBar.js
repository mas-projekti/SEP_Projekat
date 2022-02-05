import { Navbar, Nav } from "react-bootstrap"
import logo from "../img/cart_logo_inv.jpg"
import profileLogo from "../img/profile_logo.png"
import lock from "../img/lock_inv.jpg"
import { NavLink, useHistory } from "react-router-dom"
import { useEffect, useState } from "react"
import PubSub from "pubsub-js"
import jwtDecode from "jwt-decode"
// import { MDBInput, MDBCol } from "mdbreact";


const MainBar = (props) => {
    var history = useHistory();
    let startDecodedToken = localStorage.getItem(`jwt`) !== null ? jwtDecode(localStorage.getItem(`jwt`)) : null;
    const [ isLoggedIn, setIsLoggedIn ] = useState(startDecodedToken !== null ? true : false);
    const [ username, setUsername ] = useState(startDecodedToken !== null ? startDecodedToken[`http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name`] : `` );
    const [ id, setId ] = useState(startDecodedToken !== null ? startDecodedToken[`http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber`] : `` );
    const [ imageURL, setimageURL ] = useState(startDecodedToken !== null ? startDecodedToken[`http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor`] : ``);
    const { notAuthenticated } = props;
    // const [ link, setLink ] = useState(``);

    useEffect(() => {
        var pubSubToken = PubSub.subscribe(`LoginEvent`, updateUser);

        return () => {
            PubSub.unsubscribe(pubSubToken);
        }
    })

    let updateUser = () => {
        let token = localStorage.getItem(`jwt`);
        if (token === null) return;
        let decodedToken = jwtDecode(token);
        setUsername(decodedToken[`http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name`]);
        setimageURL(decodedToken[`http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor`]);
        setId(decodedToken[`http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber`]);
        setIsLoggedIn(true);
    }

    let logout = () =>  {
        localStorage.removeItem(`jwt`);
        setUsername(``);
        setIsLoggedIn(false);
        notAuthenticated();
        history.push(`/login`);
    }


    return (
        <Navbar bg="dark" 
                variant="dark" 
                style={{maxHeight:'50px'}} 
                className="shadow-lg p-4">
                <Navbar.Brand className="d-flex">
                    <NavLink to="/">
                        <img
                            alt={"Logo"}
                            src={logo}
                            width="30"
                            height="30"
                            className="d-inline-block align-top mx-2"/>
                        WebShop MAS
                    </NavLink>
                    <div className="mx-5">
                        |
                    </div>
                    <NavLink to="/about">
                        About
                    </NavLink>
                    <div className="mx-5">
                        |
                    </div>
                    <NavLink to="/courses">
                        Courses
                    </NavLink>
                </Navbar.Brand>
                <Navbar.Collapse id="responsive-navbar-nav ">
                </Navbar.Collapse>
                <Nav>
                <NavLink to="/cart">
                    <img
                        alt={"Logo"}
                        src={logo}
                        width="30"
                        height="30"
                        className="d-inline-block mx-2"/>
                    Cart{' '}
                    {props.countCartItems ? (
                        <span className="btn btn-secondary rounded">{props.countCartItems}</span>
                    ) : (
                        ''
                    )}
                </NavLink>
                <div className="mx-4">|</div>
                </Nav>
                    {isLoggedIn ?
                        (<Nav>
                            <NavLink to={"/user/" + id} className="d-flex mt-2 mx-3">
                                {imageURL === `` ?
                                    <img alt={"Profile Logo"}
                                    src={profileLogo}
                                    width="30"
                                    height="30"
                                    className="d-inline-block align-top mx-2"/>
                                : 
                                    <img alt={"Profile Logo"}
                                    src={imageURL}
                                    width="30"
                                    height="30"
                                    className="d-inline-block align-top mx-2"/>    
                                }
                                
                                <h5>{username}</h5>
                            </NavLink>
                            <NavLink to="/createNewItem">
                                <button className="btn btn-secondary mx-2">
                                    Add New Item
                                </button>
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

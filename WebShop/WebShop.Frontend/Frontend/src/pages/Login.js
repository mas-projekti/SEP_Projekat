import { Link, useHistory } from "react-router-dom"
import { useState, useEffect } from "react";
import axios from "axios";
import PubSub from "pubsub-js";
// import { Route, Router } from "workbox-routing";
// import MainBar from "../components/MainBar"

const Login = () => {
    const history = useHistory()
    const [ errorMessage, setErrorMessage ] = useState("");
    const [ credentials, setCredentials ] = useState({ username: "", password: ""});

    useEffect(() => {

    })

    const loginAttempt = async () => {
        if (credentials.username === "" || credentials.password === "") setErrorMessage("You must enter the fields first to attempt login");

        await axios.post(process.env.REACT_APP_WEB_SHOP_USERS_BACKEND_API + `/login`, credentials)
        .then((resp) => {
            setErrorMessage("");
            localStorage.setItem(`jwt`, resp.data);
            PubSub.publish(`LoginEvent`);
            console.log(`Published an event to: LoginEvent`);
            history.push(`/`);
        })
        .catch(() => {
            setErrorMessage("That user doesn't exist");
        });
    };

    return (
        <div className="page">
            {/* <MainBar/> */}
            <div className="container">
                <div className="row" style={{margin:"3%"}}/>
                <div className="row">
                    <div className="col-4"/>
                    <div className="col-4">
                        <h1>Login</h1>
                        <div className="row my-5">
                            <div className="col">Username:</div>
                            <div className="col">
                                <input type="text" value={credentials.username} onChange={(ev) => setCredentials({...credentials, username: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Password:</div>
                            <div className="col">
                                <input type="password" value={credentials.password} onChange={(ev) => setCredentials({...credentials, password: ev.target.value})} />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">
                                <button type="button" className="btn btn-outline-light" onClick={loginAttempt.bind(this)}>Login</button>
                            </div>
                            { errorMessage !== "" 
                                ? 
                                <div className="bg-danger my-2 rounded">
                                    <h4>{errorMessage}</h4>
                                </div> 
                                : 
                                <div></div>
                            }
                            
                            <div className="w-100 my-0 hr">    
                                <hr/>
                            </div>
                            <div className="col">
                                <p>You don't have an account?</p>
                                <button type="button" className="btn btn-outline-light">
                                    <Link to="/register">Register</Link>
                                </button>
                            </div>
                        </div>
                    </div>
                    <div className="col-4" />
                </div>
            </div>
        </div>
    )
}

export default Login

import { Link, useHistory } from "react-router-dom"
import { useState } from "react";
import axios from "axios";
// import MainBar from "../components/MainBar"

const Register = () => {
    const history = useHistory();
    const [ errorMessage, setErrorMessage ] = useState("");
    const [ credentials, setCredentials ] = useState({ username: "", password: "", confirmPassword: "", email: "", name: "", lastname: "", imageURL: ""});

    const registerAttempt = async () => {
        if (credentials.username === "" || credentials.password === "" || credentials.confirmPassword === "" || credentials.email === "" || credentials.name === "" || credentials.lastname === "" || credentials.imageURL === "") {
            setErrorMessage("You must enter the fields first to attempt registration");
            return;
        }

        console.log(credentials);

        if (credentials.password !== credentials.confirmPassword)  {
            setErrorMessage("Passwords do not match");
            return;
        }

        await axios.post(process.env.REACT_APP_WEB_SHOP_USERS_BACKEND_API + `/register`, credentials)
        .then((resp) => {
            history.push(`/login`);
        })
        .catch(() => {
            setErrorMessage("Something went wrong!");
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
                        <h1>Register</h1>
                        <div className="row my-5">
                            <div className="col">Username*:</div>
                            <div className="col">
                                <input type="text" value={credentials.username} onChange={(ev) => setCredentials({...credentials, username: ev.target.value})} />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Password*:</div>
                            <div className="col">
                                <input type="password" value={credentials.password} onChange={(ev) => setCredentials({...credentials, password: ev.target.value})} />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Confirm Password*:</div>
                            <div className="col">
                                <input type="password" value={credentials.confirmPassword} onChange={(ev) => setCredentials({...credentials, confirmPassword: ev.target.value})} />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Email*:</div>
                            <div className="col">
                                <input type="text" value={credentials.email} onChange={(ev) => setCredentials({...credentials, email: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Name*:</div>
                            <div className="col">
                                <input type="text" value={credentials.name} onChange={(ev) => setCredentials({...credentials, name: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Lastname*:</div>
                            <div className="col">
                                <input type="text" value={credentials.lastname} onChange={(ev) => setCredentials({...credentials, lastname: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Profile Picture URL*:</div>
                            <div className="col">
                                <input type="text" value={credentials.imageURL} onChange={(ev) => setCredentials({...credentials, imageURL: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            
                            <div className="col">
                                <button type="button" class="btn btn-outline-light" onClick={registerAttempt.bind(this)}>Register</button>
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
                                <p>You have an account?</p>
                                <button type="button" class="btn btn-outline-light">
                                    <Link to="/login">Login</Link>
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

export default Register

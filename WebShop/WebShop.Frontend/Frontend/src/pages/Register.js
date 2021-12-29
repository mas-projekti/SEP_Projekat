import { Link } from "react-router-dom"
// import MainBar from "../components/MainBar"

const Register = () => {
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
                                <input type="text" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Password*:</div>
                            <div className="col">
                                <input type="password" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Confirm Password*:</div>
                            <div className="col">
                                <input type="password" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Email*:</div>
                            <div className="col">
                                <input type="text" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Name*:</div>
                            <div className="col">
                                <input type="text" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Lastname*:</div>
                            <div className="col">
                                <input type="text" />
                            </div>
                            <div className="w-100 my-2"/>
                            
                            <div className="col">
                                <button type="button" class="btn btn-outline-light">Register</button>
                            </div>
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

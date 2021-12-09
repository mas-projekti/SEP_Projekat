import { Link } from "react-router-dom"
// import MainBar from "../components/MainBar"

const Login = () => {
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
                                <input s type="text" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Password:</div>
                            <div className="col">
                                <input s type="text" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">
                                <button type="button" class="btn btn-outline-light">Login</button>
                            </div>
                            <div className="w-100 my-0 hr">    
                                <hr/>
                            </div>
                            <div className="col">
                                <p>You don't have an account?</p>
                                <button type="button" class="btn btn-outline-light">
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
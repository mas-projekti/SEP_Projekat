import React from 'react';
import './login.css';
import  {useState} from 'react';

export default function Login() {
  const [clientID, setClientID] = useState("");
  const [secret, setSecret] = useState("");

  const handleSubmit = event => {
    event.preventDefault();
    alert('You have submitted the form.')
  }
  return(
    <div className="container login-container">

            <div className="row">
                <div className="col-md-12 login-form-1">
                    <h3>Login for clients</h3>
                    <form onSubmit={handleSubmit}>
                        <div className="form-group">
                            <input type="text" className="form-control" placeholder="Your Client ID *"  onChange={e => setClientID(e.target.value)} />
                        </div>
                        <div className="form-group">
                            <input type="password" className="form-control" placeholder="Your Client Secret *" onChange={e => setSecret(e.target.value)} />
                        </div>
                        <div className="form-group">
                            <input type="submit" className="btnSubmit" value="Login" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
  )
}
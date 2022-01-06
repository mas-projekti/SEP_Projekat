// @ts-nocheck
import React from 'react';
import './login.css';
import  {useState} from 'react';

import {
    useNavigate
  } from "react-router-dom";
import { useForm } from "react-hook-form";
import CircularProgress from '@mui/material/CircularProgress';
import {apiIdentityProvider} from '../../services/api/identity-service';

export default function Login() {
    const navigate = useNavigate();

    const {
        register,
        handleSubmit,
        formState: { errors }
      } = useForm();
  const [isLoading, setIsLoading] = useState(false);

  const onSubmit = data => {
    apiIdentityProvider.getPSPToken(data.clientID, data.clientSecret, [] )
    .then(function(resp){
      localStorage.setItem('client-token', resp.access_token);
      setIsLoading(false)
      navigate(`/client-settings/${data.clientID}`)
    }).catch((error) => {
        console.log(error.response)
        // Error
        if (error.response) {
            alert("Wrong credentials!")
            setIsLoading(false)
        } else {
            alert("Check your credentials again!");
            setIsLoading(false)
        }
    });
    setIsLoading(true);
  }

  return(
    <div className="container login-container">

            <div className="row">
                <div className="col-md-12 login-form-1">
                   
                    { isLoading === false ? ( 
                    <>
                    <h3>Login for clients</h3>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <div className="form-group">
                            <input {...register("clientID", { required: true })} type="text" className="form-control" placeholder="Your Client ID *" />
                            {errors.clientID && <p>This field is required</p>}
                        </div>
                        <div className="form-group">
                            <input {...register("clientSecret", { required: true })} type="password" className="form-control" placeholder="Your Client Secret *" />
                            {errors.clientSecret && <p>This field is required</p>}
                        </div>
                        <div className="form-group">
                            <input type="submit" className="btnSubmit" value="Login" />
                        </div>
                    </form>
                    </>
                    ) :(
                        <center>
                            <CircularProgress
                            size={400}
                            thickness={4}/>
                        </center>
                    )}
                    
                </div>
            </div>
        </div>
  )
}
// @ts-nocheck
import React from 'react';
import './registration.css';
import  {useState} from 'react';
import {apiClientsProvider} from '../../services/api/client-service';
import CircularProgress from '@mui/material/CircularProgress';
import Button from '@mui/material/Button';
import {
  Link
} from "react-router-dom";
import { useForm } from "react-hook-form";

export default function Registration() {
    const {
        register,
        handleSubmit,
        formState: { errors }
      } = useForm({
        defaultValues: {
          paypalActive: true,
        }
      });
  const [created, setCreated] = useState(false);
  const [clientID, setClientID] = useState("");
  const [clientSecret, setClientSecret] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  

  const onSubmit = data => {
    const createNewPspClientDto = 
    {
        clientName:data.clientName,
        transactionOutcomeCallback: data.transactionResultCallback,
        settingsUpdatedCallback: data.settingsCallback,
        paypalActive: data.paypalActive,
        bitcoinActive: data.bitcoinActive,
        bankActive: data.bankActive,
      
    } 
    apiClientsProvider.createNewClient(createNewPspClientDto)
    .then(function(data) {
        setClientID(data.clientID)
        setClientSecret(data.clientSecret)
        setCreated(true)
        setIsLoading(false)
    }).catch((error) => {
        // Error
        if (error.response) {
            setIsLoading(false)
            alert("Server could not be contacted, please try again!");
        } else if (error.request) {
            alert("Server could not be contacted, please try again!");
            setIsLoading(false)
        }
        console.log(error.config);
    });
    setIsLoading(true)
    
  }
  return(

    <div className="container login-container">
        {created === false ?
        (
                    <div className="row">
                        <div className="col-md-12 login-form-1">
                            {isLoading === true ? (
                                <center>
                                    <CircularProgress
                                    size={400}
                                    thickness={4}/>
                                </center>

                            ):(
                             <> 
                            <h3>Register as a new client</h3>
                            <form onSubmit={handleSubmit(onSubmit)}>
                                <div className="form-group">
                                    <input {...register("clientName", { required: true, maxLength: 50 })}  type="text" className="form-control" placeholder="Your application name *"/>
                                    {errors.clientName && <p>This field is required</p>}
                                </div>
                                <div className="form-group">
                                    <input {...register("transactionResultCallback", { required: true })} type="text" className="form-control" placeholder="Your application transaction result callback URL *" />
                                    {errors.transactionResultCallback && <p>This field is required</p>}
                                    <p>Transaction result callback URL should be HTTPS. Your application needs to provide HTTP PUT REST endpoint which will take transaction ID as body parameter.</p>
                                    <p>Example: PUT https://www.myapp.com/api/transactions/confirm/</p>
                                </div>
                                <div className="form-group">
                                    <input {...register("settingsCallback", { required: true })} type="text" className="form-control" placeholder="Your application settings changed callback URL *"  />
                                    {errors.settingsCallback && <p>This field is required</p>}
                                    <p>settings callback URL should be HTTPS. Your application needs to provide HTTP PUT REST endpoint which will take a list Dictionary od string-bool as body parameters. </p>
                                    <p>Dictionary key is string which represents service name (ex. paypal, bank..), and value is boolean which represents if service is activated for web shop or not.</p>
                                    <p>Example: PUT  https://www.myapp.com/api/options/</p>
                                </div>

                                <div className="form-group">
                                        <input readOnly={true} {...register("paypalActive", {})} type="checkbox" checked={true} /> Use paypal
                                </div>

                                <div className="form-group">
                                        <input {...register("bankActive", {})} type="checkbox" /> Use banking services
                                </div>

                                <div className="form-group">
                                        <input {...register("bitcoinActive", {})} type="checkbox" /> Use bitcoin
                                </div>
                                <div className="form-group">
                                    <input type="submit" className="btnSubmit" value="Register" />
                                </div>
                            </form>
                            </>  
                            )}
                        </div>
                    </div>
        ) : (
            <div className="row">
                <div className="col-md-12 login-form-1">
                    <h1>Account created</h1>
                    <h4> Your Client ID is {clientID}</h4>
                    <h4> Your Client secret is {clientSecret}</h4>

                    <h2>PLEASE STORE THEM CAREFULLY AS YOU WILL NOT BE ABLE TO RESET THEM!</h2>
                    <Button component={Link} to="/login" variant="contained">Proceed to Log in</Button>
                </div>
            </div>
        )}
        </div>
  )
}
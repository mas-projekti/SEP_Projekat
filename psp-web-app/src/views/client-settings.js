// @ts-nocheck
import React from 'react';
import  {useState, useEffect} from 'react';
import { useParams , useNavigate} from 'react-router'
import { useForm} from "react-hook-form";
import CircularProgress from '@mui/material/CircularProgress';
import {apiClientsProvider} from '../services/api/client-service';

export default function ClientSettings() {
    const {
        register,
        handleSubmit,
        setValue,
        formState: { errors }
      } = useForm();
  const [isLoading, setIsLoading] = useState(false);
  const [id, setID] = useState(-1);
  const routeParams = useParams();
  const navigate = useNavigate();

  const onSubmit = data => {
    const client = {
        clientID:data.clientID,
        transactionOutcomeCallback: data.transactionResultCallback,
        settingsUpdatedCallback: data.settingsCallback,
        paypalActive: data.paypalActive,
        bitcoinActive: data.bitcoinActive,
        bankActive: data.bankActive,
      
    } 
    apiClientsProvider.updateClient(id, client)
    .then(function(data) {
        alert("Updated successfully!")
        setIsLoading(false)
    }).catch((error) => {
        // Error
        if (error.response) {
            alert(error.response)
            
        } else if (error.request) {
            alert("Server could not be contacted, please try again!");
            setIsLoading(false)
        }
    });
    setIsLoading(true);
  }

  useEffect(() => {
      apiClientsProvider.getClientByClientId(routeParams.clientID)
        .then(function(resp){
          console.log(resp)

            setValue('clientID', resp.clientID, { shouldValidate: true })
            setValue('transactionResultCallback', resp.transactionOutcomeCallback, { shouldValidate: true })
            setValue('settingsCallback', resp.settingsUpdatedCallback, { shouldValidate: true })
            setValue('paypalActive', resp.paypalActive, { shouldValidate: true })
            setValue('bankActive', resp.bankActive, { shouldValidate: true })
            setValue('bitcoinActive', resp.bitcoinActive, { shouldValidate: true })
            setID(resp.id)
            setIsLoading(false)
          
       })
       .catch((error) =>{
            navigate("/login", { replace: true });
       });
       setIsLoading(true)

  }, [navigate, routeParams]);

  return(
    <div className="container login-container">

            <div className="row">
                <div className="col-md-12 login-form-1">
                   
                    { isLoading === false ? ( 
                    <>
                    <h3>Client settings</h3>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <h2>Client ID</h2>
                        <div className="form-group">
                            <input readOnly={true} {...register("clientID", {  })}  type="text" className="form-control" placeholder="Your application name *"/>
                        </div>
                        <div className="form-group">
                            <h2>Transaction result callback URL</h2>
                            <input {...register("transactionResultCallback", { required: true })} type="text" className="form-control" placeholder="Your application transaction result callback URL *" />
                            {errors.transactionResultCallback && <p>This field is required</p>}
                        </div>
                        <div className="form-group">
                            <h2>Settings changed callback URL</h2>
                            <input {...register("settingsCallback", { required: true })} type="text" className="form-control" placeholder="Your application settings changed callback URL *"  />
                            {errors.settingsCallback && <p>This field is required</p>}
                        </div>

                        <div className="form-group">
                                <input readOnly={true} {...register("paypalActive", {})} type="checkbox" /> Use paypal
                        </div>

                        <div className="form-group">
                                <input {...register("bankActive", {})} type="checkbox" /> Use banking services
                        </div>

                        <div className="form-group">
                                <input {...register("bitcoinActive", {})} type="checkbox" /> Use bitcoin
                        </div>
                        <div className="form-group">
                            <input type="submit" className="btnSubmit" value="Save changes" />
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
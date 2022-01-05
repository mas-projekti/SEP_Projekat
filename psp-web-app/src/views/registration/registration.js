import React from 'react';
import './registration.css';
import  {useState} from 'react';
import {apiClientsProvider} from '../../services/api/client-service';
import CircularProgress from '@mui/material/CircularProgress';

export default function Registration() {
  const [clientName, setClientName] = useState("");
  const [transactionResultCallback, setTRC] = useState("");
  const [settingsCallback, setSettingsCallback] = useState("");
  const [paypalActive, setPaypalActive] = useState(false);
  const [bitcoinActive, setBitcoinActive] = useState(false);
  const [bankActive, setBankActive] = useState(false);
  const [created, setCreated] = useState(false);
  const [clientID, setClientID] = useState("");
  const [clientSecret, setClientSecret] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  

  const handleSubmit = event => {
    const createNewPspClientDto = 
    {
        clientName:clientName,
        transactionOutcomeCallback: transactionResultCallback,
        settingsUpdatedCallback: settingsCallback,
        paypalActive: paypalActive,
        bitcoinActive: bitcoinActive,
        bankActive: bankActive,
      
    } 
    apiClientsProvider.createNewClient(createNewPspClientDto)
    .then(function(data) {
        setClientID(data.clientID)
        setClientSecret(data.clientSecret)
        setCreated(true)
        setIsLoading(false)
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
                            <form>
                                <div className="form-group">
                                    <input type="text" className="form-control" placeholder="Your application name *"  onChange={e => setClientName(e.target.value)} />
                                </div>
                                <div className="form-group">
                                    <input type="text" className="form-control" placeholder="Your application transaction result callback URL *" onChange={e => setTRC(e.target.value)} />
                                </div>
                                <div className="form-group">
                                    <input type="text" className="form-control" placeholder="Your application settings changed callback URL *" onChange={e => setSettingsCallback(e.target.value)} />
                                </div>

                                <div className="form-group">
                                        <input type="checkbox" checked={paypalActive} onChange={e => setPaypalActive(e.target.checked)}/> Use paypal
                                </div>

                                <div className="form-group">
                                        <input type="checkbox" checked={bankActive} onChange={e => setBankActive(e.target.checked)}/> Use banking services
                                </div>

                                <div className="form-group">
                                        <input type="checkbox" checked={bitcoinActive} onChange={e => setBitcoinActive(e.target.checked)}/> Use bitcoin
                                </div>
                                <div className="form-group">
                                    <input type="button" className="btnSubmit" value="Register" onClick={handleSubmit} />
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

                    <h2>PLEASE STORE THEM CAREFULLY AS YOU WILL NOT BE ABLE TO RESET THEM IF LOST!</h2>
                </div>
            </div>
        )}
        </div>
  )
}
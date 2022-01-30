import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";
import {apiIdentityProvider} from '../services/api/identity-service';
import {apiPaypalProvider} from '../services/api/paypal-service';
import {apiTransactionsProvider} from '../services/api/transactions-service';
import OrderBreakdown from '../components/order-breakdown'
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import * as React from 'react'
import  {useEffect, useState} from 'react';
import { useParams , useNavigate} from 'react-router'
import CircularProgress from '@mui/material/CircularProgress';
import { apiClientsProvider } from './../services/api/client-service';
import Button from '@mui/material/Button';
import CreditCardIcon from '@mui/icons-material/CreditCard';
import QrCode2Icon from '@mui/icons-material/QrCode2';

//MAKE THIS NOT HARDCODED LATER
var psp_client_id = 'klijentneki';
var psp_client_secret = 'tajnovitatajna';


function Checkout() {
  const [isLoaded, setIsLoaded] = useState(false)
  const [bitcoinActive, setBitcoinActive] = useState(false)
  const [bankActive, setBankActive] = useState(false)
  const routeParams = useParams();
  const [orderItems, setOrderItems] = useState([])
  const [paypalOptions, setOptions] = useState({
    'client-id': "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
    currency: "USD",
    intent: "capture",
    'merchant-id': [],
  });
  const navigate = useNavigate()
  const [qrCode, setQrCode] = useState("");

  useEffect(() => {
    
    apiIdentityProvider.getPSPToken(psp_client_id, psp_client_secret, ['paypal-api'] )
    .then(function(resp){
      localStorage.setItem('psp-token', resp.access_token)
      apiTransactionsProvider.getTransactionById(routeParams.transactionId)
        .then(function(resp){
          console.log(resp);
          if(resp === undefined || resp.isAxiosError)
          {
            navigate("/", { replace: true });
      
          }else
          {
            apiClientsProvider.getClientById(resp.clientId)
                              .then(function(data){
                                  setBitcoinActive(data.bitcoinActive)
                                  setBankActive(data.bankActive)
                                  setIsLoaded(true)
                              });

            setOptions({
              'client-id': "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
              currency: resp.currency,
              intent: "capture",
              'merchant-id': resp.merchantIds,
            });
            setOrderItems(resp.items);
            let QRData = 'INFO';
            let size = 100;
            setQrCode(`http://api.qrserver.com/v1/create-qr-code/?data=${QRData}&size=${size}x${size}`);
          }
       });
    });

  }, [routeParams, navigate]);
  
  return (
    <>
    {
     isLoaded === true ? (
      <div className="container">
      {/* <Grid container justifyContent="center" alignItems="center" rowSpacing={1} spacing={1}>

      </Grid> */}
      <div className="row my-3">
        <div className="col-1"></div>
        <div className="col-10">
          <Grid style={{textAlign: "center"}} item xs={12}>
              <OrderBreakdown items={orderItems}/>
          </Grid>
        </div>
        <div className="col-1"></div>
      </div>
        <div className="row">
          <div className="col-1"></div>
          <div className="col-10 d-flex">
            <Grid item xs={4} className="me-2">  
              <center className="mx-2 my-2">
                <PayPalScriptProvider options={paypalOptions}>
                        <PayPalButtons
                        style={{ layout: "horizontal" }}
                        onApprove={(data, actions) => onApproveCallback(data, actions, orderItems)}
                        createOrder={(data, actions) => onCreateOrder(data, actions, orderItems, routeParams.transactionId)}
                        />
                </PayPalScriptProvider>
              </center>      
            </Grid>
            <Grid item xs={4} className="me-2" >
              <center className="mx-2 my-2">
                { bankActive ? (      
                <Button onClick={(data) => onBankTransactionCreate(routeParams.transactionId)} variant="contained">
                  <CreditCardIcon fontSize="large" color="white" className="me-2"/>
                  Pay with credit card
                </Button>
                )
                :
                (<></>)
                } 
              </center>
              <hr className="mx-3"></hr>
              <center>
                <Button onClick={(data) => onBankTransactionCreate(routeParams.transactionId)} variant="contained" disabled>
                  <QrCode2Icon fontSize="large" color="white" className="me-2"></QrCode2Icon>
                  Pay with QR Code
                </Button>
                
                <h3></h3>
                <div className="col">
                  <img src={qrCode} alt="" />
                </div>
                <div className="col my-3">
                  <a href={qrCode} download="QRCode">
                    <Button variant="contained">Download</Button>
                  </a>
                </div>
              </center>
            </Grid>
            <Grid item xs={4}>
              <center className="mx-2 my-2">
                { bankActive ? (      
                <Button onClick={(data) => onBankTransactionCreate(routeParams.transactionId)} variant="contained">
                  <img src="https://cryptologos.cc/logos/bitcoin-btc-logo.svg?v=018" width={30} height={30} alt="" className="me-2"></img>
                  PODESITI OVO DUGME ZA BITKOIN, NE RADI POSAO KOJI TREBA
                </Button>
                )
                :
                (<></>)
                }  
              </center>
            </Grid>
          </div>
          <div className="col-1"></div>
        </div>
        <div className="row">
          
        </div> 

    </div>
     ) : (
       <div className="container my-3">
        <center>
          <CircularProgress
          size={400}
          thickness={4}/>
        </center>
       </div>
        )
    }
    
   </>
   
  );
}
export default Checkout;

function onBankTransactionCreate(transactionId){
  apiTransactionsProvider.payWithBank(transactionId)
  .then(function(data){
    window.open(data.paymentURL,"_self");
  });
}

function onApproveCallback(data, actions, orderItems){
  console.log(data)
  return apiPaypalProvider.capturePaypalOrder(data.orderID)
  .then(function(orderData) {
      let errorDetail = Array.isArray(orderData.details) && orderData.details[0];

          if (errorDetail && errorDetail.issue === 'INSTRUMENT_DECLINED') {
              return actions.restart(); // Recoverable state, per:
              // https://developer.paypal.com/docs/checkout/integration-features/funding-failure/
          }

          if (errorDetail) {
              var msg = 'Sorry, your transaction could not be processed.';
              if (errorDetail.description) msg += '\n\n' + errorDetail.description;
              if (orderData.debug_id) msg += ' (' + orderData.debug_id + ')';
              return alert(msg); // Show a failure message (try to avoid alerts in production environments)
          }

          // Successful capture! For demo purposes:
          console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));
          var transaction = orderData.purchase_units[0].payments.captures[0];
          alert('Transaction '+ transaction.status + ': ' + transaction.id + '\n\nSee console for all available details');
  });
                  
}

function onCreateOrder(data, actions, orderItems, transactionId){
  const createOrderDto = 
  {
    transactionId:transactionId,
    cancelUrl:`http://localhost:3000/checkout/${transactionId}`,
    returnUrl:`http://localhost:3001/`,
    items:orderItems
  } //Ovde isto staviti konfigurabilne podatke
  return apiPaypalProvider.createOrder(createOrderDto)
      .then(function(data) {
          console.log(data)
          return data.id;
      });

}
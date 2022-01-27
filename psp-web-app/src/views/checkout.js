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

  useEffect(() => {
    
    apiIdentityProvider.getPSPToken(psp_client_id, psp_client_secret, ['paypal-api'] )
    .then(function(resp){
      localStorage.setItem('psp-token', resp.access_token)
      apiTransactionsProvider.getTransactionById(routeParams.transactionId)
        .then(function(resp){
          console.log(resp)
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
          }
       });
    });

  }, [routeParams, navigate]);
  
  return (
    <>
    {
     isLoaded === true ? (
      <Box>
      <Grid container justifyContent="center" alignItems="center" rowSpacing={1} spacing={1}>
        <Grid style={{textAlign: "center"}} item xs={10}>
          <OrderBreakdown items={orderItems}/>
        </Grid>
        <Grid item xs={4}>        
            <PayPalScriptProvider options={paypalOptions}>
                    <PayPalButtons
                    style={{ layout: "horizontal" }}
                    onApprove={(data, actions) => onApproveCallback(data, actions, orderItems)}
                    createOrder={(data, actions) => onCreateOrder(data, actions, orderItems, routeParams.transactionId)}
                    />
            </PayPalScriptProvider>
        </Grid>
        <Grid item xs={4}>
          { bankActive ? (      
          <Button onClick={(data) => onBankTransactionCreate(routeParams.transactionId)} variant="contained">Pay with credit card</Button>
          )
           :
           (<></>)
           }  
        </Grid>  
      </Grid>
    </Box>
     ) : (
       <center>
          <CircularProgress
          size={400}
          thickness={4}/>
        </center>
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
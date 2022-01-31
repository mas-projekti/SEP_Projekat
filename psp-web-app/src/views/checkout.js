import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";
import {apiIdentityProvider} from '../services/api/identity-service';
import {apiPaypalProvider} from '../services/api/paypal-service';
import {apiTransactionsProvider} from '../services/api/transactions-service';
import OrderBreakdown from '../components/order-breakdown'
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

  const [transaction, setTransaction] = useState(null)
  const [planId, setPlanId] = useState(null)
  const [isLoaded, setIsLoaded] = useState(false);
  const [bitcoinActive, setBitcoinActive] = useState(true);
  const [bankActive, setBankActive] = useState(false);
  const routeParams = useParams();
  const [isQRBtnVisible, setIsQRBtnVisible] = useState("hidden");
  const [QRURL, setQRURL] = useState("");
  const [orderItems, setOrderItems] = useState([]);
  const [paypalOptions, setOptions] = useState({
    'client-id': "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
    currency: "USD",
    intent:'capture',
    'merchant-id': [],
  });
  const [paypalSubscribeOptions, setSubscribeOptions] = useState({
    'client-id': "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
    currency: "USD",
    intent:'subscribe',
    vault:true
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
            setTransaction(resp)

            
            let qrCodeData = {
              merchantId: resp.bankTransactionData.merchantID,
              merchantPassword: resp.bankTransactionData.merchantPassword,
              amount: resp.bankTransactionData.amount,
              merchantOrderId: resp.bankTransactionData.merchantOrderID,
              merchantTimestamp: resp.bankTransactionData.merchantTimestamp,
              bankUrl: resp.bankTransactionData.bankUrl,
              successUrl: `http://localhost:3000/transaction-passed/${routeParams.transactionId}`,
              failedrUrl: `http://localhost:3000/transaction-failed`,
              errorUrl: `http://localhost:3000/transaction-error`
            };

            apiClientsProvider.getClientById(resp.clientId)
                              .then(function(data){
                                  setBitcoinActive(data.bitcoinActive);
                                  setBankActive(data.bankActive);
                                  setIsLoaded(true);
                              });
            if(resp.subscriptionTransaction == null)
            {
              setPayOptions({
                'client-id': "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
                currency: resp.currency,
                intent: "capture",
                'merchant-id': resp.merchantIds,
              });
              

            }else
            {
              setPlanId(resp.subscriptionTransaction.subscriptionPlanId)
              setSubscribeOptions({
                'client-id': "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
                currency: resp.currency,
                intent: "subscription",
                vault:true
              });

            }
            
            setOrderItems(resp.items);
            let qrEncodedObject = encodeURIComponent(JSON.stringify(qrCodeData, null, 4));
            let qrString = `http://localhost:3000/qrCode/` + qrEncodedObject;
            setQRURL(qrString);
            let size = 300;
            setQrCode(`http://api.qrserver.com/v1/create-qr-code/?data=${JSON.stringify(qrCodeData)}&size=${size}x${size}`);
          }
       });
    });

  }, [routeParams, navigate]);


  function toggleBtn() {
    return isQRBtnVisible === "hidden" ? setIsQRBtnVisible("visible") : setIsQRBtnVisible("hidden");
  };

  function openInNewTab() {
    window.open(QRURL, '_blank').focus();
  }
  
  return (
    <>
    {
     isLoaded === true ? (
      <Box>
      <Grid container justifyContent="center" alignItems="center" rowSpacing={1} spacing={1}>
        <Grid style={{textAlign: "center"}} item xs={10}>
          <OrderBreakdown items={orderItems}/>
        </Grid>
        <Grid item xs={4} className="mt-4"> 
        { planId === null ? (   
            <>
            <PayPalScriptProvider options={paypalPayOptions}>
                    <PayPalButtons
                    style={{ layout: "horizontal" }}
                    onApprove={(data, actions) => onApproveCallback(data, actions, orderItems)}
                    createOrder={(data, actions) => onCreateOrder(data, actions, orderItems, routeParams.transactionId)}
                    />
            </PayPalScriptProvider>
            </>
          ):(    

            <PayPalScriptProvider options={paypalSubscribeOptions}>
                    <PayPalButtons
                    style={{ layout: "horizontal", label:"subscribe" }}
                    createSubscription={(data, actions) => onCreateSubscription(data, actions, planId)}
                    onApprove={(data, actions) => onSubscriptionApproveCallback(data, actions, routeParams.transactionId, navigate)}
                    />
            </PayPalScriptProvider>
          )}
            
        </Grid>
        <Grid item xs={4}>
          { bankActive && transaction.bankTransactionData!=null ? (      
          <Button onClick={(data) => onBankTransactionCreate(routeParams.transactionId)} variant="contained">
            <CreditCardIcon fontSize="large" color="white" className="me-2"/>
            Pay with credit card
          </Button>
          )
           :
           (<></>)
           }
           <hr className="mx-3"></hr>
              <center>
                <Button variant="contained" onClick={toggleBtn}>
                  <QrCode2Icon fontSize="large" color="white" className="me-2"></QrCode2Icon>
                  Pay with QR Code
                </Button>
                <div className="col mt-3" style={{visibility:isQRBtnVisible}}>
                  <img src={qrCode} alt="" />
                </div>
                <div className="col my-3" style={{visibility:isQRBtnVisible}}>
                  <div>
                    <Button variant="contained" onClick={openInNewTab}>Open QR Code</Button>
                  </div>
                </div>
              </center>
        </Grid>
        <Grid item xs={4}>
              <center className="mx-2 my-2">
                { bitcoinActive ? (      
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
      </Grid>
    </Box>

      
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


function onCreateSubscription(data, actions, subPlanId)
{
  return actions.subscription.create({
    /* Creates the subscription */
    plan_id: subPlanId
  });
}

function onSubscriptionApproveCallback(data, actions, transactionId, navigate)
{
  navigate(`/transaction-passed/${transactionId}`)
}


function onBankTransactionCreate(transactionId){
  apiTransactionsProvider.payWithBank(transactionId)
  .then(function(data){
    console.log(data);
    //window.open(data.paymentURL,"_self");
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


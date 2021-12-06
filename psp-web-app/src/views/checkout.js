import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";
import {apiIdentityProvider} from '../services/api/identity-service';
import {apiPaypalProvider} from '../services/api/paypal-service';
import OrderBreakdown from '../components/order-breakdown'
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import React, {useEffect} from 'react';

//MAKE THIS NOT HARDCODED LATER
var psp_client_id = 'klijentneki';
var psp_client_secret = 'tajnovitatajna';

const initialOptions = {
  'client-id': "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
  currency: "USD",
  intent: "capture",
  'merchant-id': ["KXJ2PH4QBBC9N","J9FGMF2MVAENC"],
};

const orderItems = [
    {
    name:"Racunar",
    description:"Lep neki komp",
    quantity: 1,
    currency: "USD",
    value: 1000,
    merchantID:'KXJ2PH4QBBC9N'
    },
    {
    name:"Mis",
    description:"Misic mali",
    quantity: 1,
    currency: "USD",
    value: 10,
    merchantID:'KXJ2PH4QBBC9N'
    },
    {
    name:"Stolica",
    description:"fotelja neka mnogo dobrA",
    quantity: 1,
    currency: "USD",
    value: 100,
    merchantID:'J9FGMF2MVAENC'
    },


];

function onApproveCallback(data, actions){
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

function onCreateOrder(data, actions){
    const createOrderDto = 
    {items:orderItems} //Ovde isto staviti konfigurabilne podatke
    return apiPaypalProvider.createOrder(createOrderDto)
        .then(function(data) {
            console.log(data)
            return data.id;
        });

}



function Checkout() {

  useEffect(() => {
    apiIdentityProvider.getPSPToken(psp_client_id, psp_client_secret, ['paypal-api'] )
    .then(function(resp){
      localStorage.setItem('psp-token', resp.access_token)
      console.log(localStorage.getItem('psp-token'))
    });
  });
  
  return (

    <Box sx={{ flexGrow: 1 }} spacing={0}>
      <Grid container justifyContent="center" alignItems="center" rowSpacing={1} spacing={1}>
        <Grid style={{textAlign: "center"}} item xs={10}>
          <OrderBreakdown items={orderItems}/>
        </Grid>
        <Grid item xs={6}>        
            <PayPalScriptProvider options={initialOptions}>
                    <PayPalButtons
                    style={{ layout: "horizontal" }}
                    onApprove={(data, actions) => onApproveCallback(data, actions)}
                    createOrder={(data, actions) => onCreateOrder(data, actions)}
                    />
            </PayPalScriptProvider>
        </Grid>

    </Grid>
    </Box>
  );
}

export default Checkout;
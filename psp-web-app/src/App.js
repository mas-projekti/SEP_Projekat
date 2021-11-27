import './App.css';
import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";
import axios from 'axios';

const initialOptions = {
  "client-id": "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
  currency: "USD",
  intent: "capture",
  "merchant-id": "KXJ2PH4QBBC9N",
};


function App() {
  return (
    <PayPalScriptProvider options={initialOptions}>
            <PayPalButtons
             style={{ layout: "horizontal" }}
             onApprove={(data, actions) =>{
              const headers = { 
                //TODO: Ovde dodati umetanje tokena. Za sada je zakucan neki moj koji je vrv istekao u momentu kad ovo citate.
                  'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkVBQzhFQkU0ODMxM0YwNDVCOTIyMTBBQUEyOUQwMkIxIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MzgwNTEzNzAsImV4cCI6MTYzODA1NDk3MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzODkiLCJjbGllbnRfaWQiOiJrbGlqZW50bmVraSIsImp0aSI6IjlEQjI2REY1MEE1OTcxMTZFRURCNEYwM0QwQzdGQzIxIiwiaWF0IjoxNjM4MDUxMzcwLCJzY29wZSI6WyJwYXlwYWwtYXBpIl19.cYkHOx0eP3SLjbdb41AFf--m3CgCM-CSoV6uqi_9KR2OU_y0FF8umQq6Vc65jw-GrpuYzGiPvWZIZqkxCVGwg7LnAe9qqKhW5ENroj4kFBAtFNYViaHylZHpUX593s9qaYu4p7Nf-egPxOQz_s8g3TXThkAuQhNwdHYk-j6cabAvrJlyg2RygvzS3rVpFGLbV40YBW3EpWKCF2w0cE4BtyIYOtGtcfdspWcDJU4S4jJyWLqd_JBQZhdwA87tEdVlcvRxkKWkpbwFsqynFfp7vMXaKfkfBbriqiYz8CRMEExdm2AchrAvppnErxv7R1hgSoEzg2wD2UJilcla9mFclQ',
                  'My-Custom-Header': 'foobar'
              };
              console.log(data)
              return axios.post(`https://localhost:44313/paypal/orders/${data.orderID}/capture`,{} , { headers })
              .then(function(res) {
                let orderData = res.data
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
          }
             createOrder={(data, actions) => {
              const createOrderDto = {merchantId:"KXJ2PH4QBBC9N", items:[]} //Ovde isto staviti konfigurabilne podatke
              const headers = { 
                //TODO: Ovde dodati umetanje tokena. Za sada je zakucan neki moj koji je vrv istekao u momentu kad ovo citate.
                  'Authorization': 'Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IkVBQzhFQkU0ODMxM0YwNDVCOTIyMTBBQUEyOUQwMkIxIiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2MzgwNTEzNzAsImV4cCI6MTYzODA1NDk3MCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzODkiLCJjbGllbnRfaWQiOiJrbGlqZW50bmVraSIsImp0aSI6IjlEQjI2REY1MEE1OTcxMTZFRURCNEYwM0QwQzdGQzIxIiwiaWF0IjoxNjM4MDUxMzcwLCJzY29wZSI6WyJwYXlwYWwtYXBpIl19.cYkHOx0eP3SLjbdb41AFf--m3CgCM-CSoV6uqi_9KR2OU_y0FF8umQq6Vc65jw-GrpuYzGiPvWZIZqkxCVGwg7LnAe9qqKhW5ENroj4kFBAtFNYViaHylZHpUX593s9qaYu4p7Nf-egPxOQz_s8g3TXThkAuQhNwdHYk-j6cabAvrJlyg2RygvzS3rVpFGLbV40YBW3EpWKCF2w0cE4BtyIYOtGtcfdspWcDJU4S4jJyWLqd_JBQZhdwA87tEdVlcvRxkKWkpbwFsqynFfp7vMXaKfkfBbriqiYz8CRMEExdm2AchrAvppnErxv7R1hgSoEzg2wD2UJilcla9mFclQ',
              };
              return axios.post('https://localhost:44313/paypal/orders', createOrderDto, { headers })
                  .then(function(res) {
                    console.log(res)
                    return res.data.id;
                });
              
            }}
             />
    </PayPalScriptProvider>
  );
}

export default App;

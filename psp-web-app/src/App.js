import './App.css';
import { PayPalScriptProvider, PayPalButtons } from "@paypal/react-paypal-js";

const initialOptions = {
  "client-id": "ATa_snSHZWQqqwq_ahDhynNClktGWCdwLr_bTbNCNxE-h8j4gZ3ByOYwrtu-PC2l3aFO8Wf_Pyaj71Xl",
  currency: "USD",
  intent: "capture",
  "merchant-id": "KXJ2PH4QBBC9N",
  locale: "en_RS"
};

function App() {
  return (
    <PayPalScriptProvider options={initialOptions}>
            <PayPalButtons />
    </PayPalScriptProvider>
  );
}

export default App;

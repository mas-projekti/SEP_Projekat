import React from 'react';
import './index.css';
import {
  BrowserRouter as Router,
  Route,
  Routes
} from 'react-router-dom';
import NavBar from './components/navbar';
import Checkout from './views/checkout';
import FrontPage from './views/frontpage';
import Login from './views/login/login'
import Registration from './views/registration/registration'
import ClientSettings from './views/client-settings'
import TransactionPassed from './views/transaction-outcomes/transaction-passed';
import TransactionFailed from './views/transaction-outcomes/transaction-failed';
import TransactionError from 'views/transaction-outcomes/transaction-error';

function App() {


  return (
    <Router>
    <div>
      <NavBar></NavBar>
      <Routes>
        <Route path="/client-settings/:clientID" element={<ClientSettings/>}/>
        <Route path="/login" element={<Login/>}/>
        <Route path="/register" element={<Registration/>}/>
        <Route path="/checkout/:transactionId" element={<Checkout/>}/>
        <Route path="/transaction-passed/:transactionId" element={<TransactionPassed/>}/>
        <Route path="/transaction-failed" element={<TransactionFailed/>}/>
        <Route path="/transaction-error" element={<TransactionError/>}/>
        <Route  path="*" element={<FrontPage/>}/>
      </Routes>
    </div>
  </Router>
  );
}

export default App;

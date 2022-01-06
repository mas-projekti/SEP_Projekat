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
        <Route  path="*" element={<FrontPage/>}/>
      </Routes>
    </div>
  </Router>
  );
}

export default App;

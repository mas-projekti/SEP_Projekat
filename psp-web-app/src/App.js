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

function App() {


  return (
    <Router>
    <div>
      <NavBar></NavBar>
      <Routes>
        <Route path="/checkout/:transactionId" element={<Checkout/>}/>
        <Route  path="*" element={<FrontPage/>}/>
      </Routes>
    </div>
  </Router>
  );
}

export default App;

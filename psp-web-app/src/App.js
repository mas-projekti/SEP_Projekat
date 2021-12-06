import React from 'react';
import './index.css';
import {
  BrowserRouter as Router,
  Route,
  Routes
} from 'react-router-dom';
import NavBar from './components/navbar';
import Checkout from './views/checkout'

function App() {


  return (
    <Router>
    <div>
      <NavBar></NavBar>
      <Routes>
        <Route exact path="/" element={<Checkout/>}/>
      </Routes>
    </div>
  </Router>
  );
}

export default App;

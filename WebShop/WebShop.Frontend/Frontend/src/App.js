import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Home from './pages/Home';
import About from './pages/About';
import ItemPage from './pages/ItemPage';
import Login from './pages/Login';
import Register from './pages/Register';
import MainBar from './components/MainBar';

function App() {
  return (
    <div className="App">
      <Router>
        <MainBar/>
        <Routes>
          <Route path='/' exact element={<Home/>} />
          <Route path='about' element={<About/>}/>
          <Route path='item/:itemId' element={<ItemPage/>}/>
          <Route path='login' element={<Login/>}/>
          <Route path='register' element={<Register/>}/>
        </Routes>
      </Router>
    </div>
  );
}

export default App;

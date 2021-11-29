import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Home from './pages/Home';
import About from './pages/About';
import ItemPage from './pages/ItemPage';

function App() {
  return (
    <div className="App">
      <Router>
        <Routes>
          <Route path='/' exact element={<Home/>} />
          <Route path='/about' element={<About/>}/>
          <Route path='/item' element={<ItemPage/>}/>
        </Routes>
      </Router>
    </div>
  );
}

export default App;

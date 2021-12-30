import './App.css';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import Home from './pages/Home';
import About from './pages/About';
import ItemPage from './pages/ItemPage';
import Login from './pages/Login';
import Register from './pages/Register';
import MainBar from './components/MainBar';
import Profile from './pages/Profile';
import { CSSTransition, TransitionGroup } from 'react-transition-group';


function AnimatedRoutes() {
  // const location = useLocation();

  return (
    <Route render={({location}) => (
      <TransitionGroup>
        <CSSTransition
          key={location.pathname}
          timeout={450}
          classNames="fade">
          <Switch location={location}>
            <Route path='/' exact>
              <Home/>
            </Route>
            <Route path='/about'>
              <About/>
            </Route>
            <Route path='/item/:itemId' render={(props) => <ItemPage {...props} />} >
              {/* <ItemPage/> */}
            </Route>
            <Route path='/login'>
              <Login/>
            </Route>
            <Route path='/register'>
              <Register/>
            </Route>
            <Route path='/user/:userId' render={(props) => <Profile {...props} />}>
              {/* <Profile/> */}
            </Route>
          </Switch>
      </CSSTransition>
    </TransitionGroup>
    )}/>
    
  )
}

function App() {
  return (
    <div className="App">
      <Router>
        <MainBar />
        <AnimatedRoutes />
      </Router>
    </div>
  );
}

export default App;

import './App.css';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import Home from './pages/Home';
import About from './pages/About';
import ItemPage from './pages/ItemPage';
import Login from './pages/Login';
import Register from './pages/Register';
import MainBar from './components/MainBar';
import Profile from './pages/Profile';
import Cart from './pages/Cart';
import CreateItemPage from './pages/CreateItemPage';
import { CSSTransition, TransitionGroup } from 'react-transition-group';
import { useState } from 'react';
import Courses from './pages/Courses';



// function AnimatedRoutes() {
  
//   const { cartItems, onAdd, onRemove } = props;
//   // const location = useLocation();

//   return (
    
    
//   )
// }

function App() {
  const [cartItems, setCartItems] = useState([]);
  const [isAuth, setIsAuth] = useState(localStorage.getItem(`jwt`) !== null);

  const onAdd = (product, quantity) => {
    const exist = cartItems.find((x) => x.id === product.id);
    if (exist) {
      let q = Number(exist.qty) + Number(quantity);
      q = q > Number(product.amount) ? Number(product.amount) : q
      setCartItems(
        cartItems.map((x) =>
          x.id === product.id ? { ...exist, qty: q } : x
        )
      );
    } else {
      setCartItems([...cartItems, { ...product, qty: quantity}]);
    }
  };

  const onRemove = (product) => {
    const exist = cartItems.find((x) => x.id === product.id);
    if (exist.qty === 1) {
      setCartItems(cartItems.filter((x) => x.id !== product.id));
    } else {
      setCartItems(
        cartItems.map((x) =>
          x.id === product.id ? { ...exist, qty: exist.qty - 1 } : x
        )
      );
    }
  };

  const onRemoveEntire = (product) => {
    setCartItems(cartItems.filter((x) => x.id !== product.id));
  };

  const emptyCart = () => {
    setCartItems([]);
  };


  const authenticated = () => {
    setIsAuth(true);
    console.log(isAuth);
  }

  const notAuthenticated = () => {
    setIsAuth(false);
    console.log(isAuth);
  }



  const authGuardFunction = () => {
    return isAuth;
  }

  const loginGuardFunction = () => {
    return isAuth;
  }

  return (
    <div className="App">
      <Router>
        <MainBar countCartItems={cartItems.length} notAuthenticated={notAuthenticated} />
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
                <Route path='/item/:itemId' render={(props) => <ItemPage {...props} onAdd={onAdd}/>} >
                  {/* <ItemPage/> */}
                </Route>
                <Route path='/login'>
                  <Login authenticated={authenticated} loginGuardFunction={loginGuardFunction} />
                </Route>
                <Route path='/register'>
                  <Register/>
                </Route>
                <Route path='/courses'>
                  <Courses/>
                </Route>
                <Route path='/user/:userId' render={(props) => <Profile {...props} authGuardFunction={authGuardFunction}/>}>
                  {/* <Profile/> */}
                </Route>
                <Route path='/cart'>
                  <Cart cartItems={cartItems} onAdd={onAdd} onRemove={onRemove} onRemoveEntire={onRemoveEntire} emptyCart={emptyCart}/>
                </Route>
                <Route path='/createNewItem' >
                  <CreateItemPage authGuardFunction={authGuardFunction}/>
                </Route>
              </Switch>
            </CSSTransition>
          </TransitionGroup>
        )}/>
      </Router>
    </div>
  );
}

export default App;

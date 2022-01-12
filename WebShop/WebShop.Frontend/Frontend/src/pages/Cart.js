import axios from 'axios';
import React, { useState } from 'react';
import jwtDecode from "jwt-decode"
import { useHistory } from 'react-router-dom';

export default function Cart(props) {
  const { cartItems, onAdd, onRemove, onRemoveEntire, emptyCart } = props;
  const itemsPrice = cartItems.reduce((a, c) => a + c.qty * c.price, 0);
  const totalPrice = itemsPrice;

  const BASE_URL = "https://localhost:44313";
  const PSP_FRONT = "http://localhost:3000/checkout/";
  const config = {
     headers: { Authorization: `Bearer ${localStorage.getItem("psp-token")}` },
  };

  const text = `You must login first`;
  const [ isTextVisible, setIsTextVisible ] = useState(false);
  const history = useHistory();

  function checkout() {
      
    let token = localStorage.getItem(`jwt`);
    if (token === null) {
        setIsTextVisible(true);
        return;
    }
    setIsTextVisible(false);
    let decodedToken = jwtDecode(token);

    const customerId = decodedToken[`http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber`];
    const loggedUserMerchantId = decodedToken[`http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata`];

    let newOrder = {
        customerId: customerId,
        transactionId: ``,
        products: []
    }

    // Lista za PSP
    let listOfOrders = []
    cartItems.map((item) => (
        listOfOrders.push({
            // id: item.id,
            name: item.model,
            description: item.description,
            quantity: item.qty,
            currency: "USD",
            value: item.price,
            merchantId: item.user.merchantId   // VELIKO PITANJE: merchantId: "KXJ2PH4QBBC9N" (Guid)  ili merchantId: item.userId
        })
    ));

    

    

    // Otkomentarisati posle radi PSP API-ja

    axios.post(`${BASE_URL}/payment-service/transactions`, listOfOrders, config)
    .then((pspResp) => {
        //Lista za WebShop
        cartItems.map((item) => (
            newOrder.products.push({
                productId: item.id,
                amount: item.qty,
                price: item.price
            })
        ));

        newOrder.transactionId = pspResp.data.id;

        axios.post(process.env.REACT_APP_WEB_SHOP_ORDERS_BACKEND_API, newOrder)
        .then((webShopResp) => {
            emptyCart();
            const putanjica = PSP_FRONT + pspResp.data.id;
            window.open(putanjica);
            history.push(`user/${customerId}`);
        })
    });
  }


  return (
      <div className='row'>
          <div className='col-2'>

          </div>
          <div className='col'>
            <h2>Cart Items</h2>
            <div>
            {cartItems.length === 0 && <div>Cart is empty</div>}
            {cartItems.map((item) => (
                <div key={item.id} className="row my-2s">
                <div className="col py-2">{item.manufacturer}</div>
                <div className="col py-2">{item.model}</div>
                <div className="col d-flex">
                    <button className="btn btn-secondary rounded-circle" style={{width:`40px`, height:`40px`}} onClick={() => onRemove(item)} >
                    -
                    </button>{' '}
                    <button className="btn btn-secondary rounded-circle" style={{width:`40px`, height:`40px`}} onClick={() => onAdd(item, 1)} >
                    +
                    </button>
                </div>

                <div className="col text-right py-2">
                    Max: {item.amount}
                </div>

                <div className="col text-right py-2">
                    {item.qty} x ${item.price.toFixed(2)}
                </div>

                <div className="col text-right py-2">
                    = ${item.qty * item.price.toFixed(2)}
                </div>
                <div className="col-2 text-right py-2">
                    <button className="btn btn-secondary r"  onClick={() => onRemoveEntire(item)} >
                        Remove Item
                    </button>
                </div>
                
                </div>
            ))}

            {cartItems.length !== 0 && (
                <>
                    <hr></hr>
                    <div className="row">
                        <div className="col-2">Items Price</div>
                        <div className="col-1 text-right">${itemsPrice.toFixed(2)}</div>
                    </div>

                    <div className="row">
                        <div className="col-2">
                        <strong>Total Price</strong>
                        </div>
                        <div className="col-1 text-right">
                        <strong>${totalPrice.toFixed(2)}</strong>
                        </div>
                    </div>
                    <hr/>
                    <div className="row">
                        <div className='col-4'>

                        </div>
                        <div className='col-4'>
                            <div className='bg-danger my-3 rounded'>
                                {isTextVisible ? text : null}
                            </div>
                            
                            
                            <button className="btn btn-secondary" onClick={() => checkout()}>
                                Checkout
                            </button>
                        </div>
                        <div className='col-4'>

                        </div>
                        
                    </div>
                </>
            )}
            </div>
          </div>
          <div className='col-2'>

          </div>
      </div>
    
  );
}


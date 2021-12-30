import React from 'react';

export default function Cart(props) {
  const { cartItems, onAdd, onRemove, onRemoveEntire } = props;
  const itemsPrice = cartItems.reduce((a, c) => a + c.qty * c.price, 0);
  const totalPrice = itemsPrice;
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
                            <button className="btn btn-secondary" onClick={() => alert('Implement Checkout!')}>
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


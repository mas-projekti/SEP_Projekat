import axios from 'axios';
import React, { Component } from 'react';

export class Profile extends Component {
    constructor(props){
        super(props);
        this.state = {
            user: {},
            orderList: [],
            orderItemList: [],
            orderNumber: 0,
            totalPrice: 0,
        }
    }

    

    componentDidMount() {
        if (!this.props.authGuardFunction()) this.props.history.push(`/login`);
        axios.get(process.env.REACT_APP_WEB_SHOP_USERS_BACKEND_API + `/` + this.props.match.params.userId)
        .then((resp) => {
            this.setState({user: resp.data});
            axios.get(process.env.REACT_APP_WEB_SHOP_ORDERS_BACKEND_API + `/user/` + this.props.match.params.userId + `/orders`)
            .then((resp) => {
                this.setState({ orderList: resp.data})
            })
        })
        .catch((err) => {

        })
    }


    seeFullOrder(orderNumber1, listOfItems) {
        let value = 0;
        listOfItems.forEach((o, i) => (
            value +=  Number(o.amount) * Number(o.price.toFixed(2))
        ));
        this.setState({ orderItemList: listOfItems, orderNumber: orderNumber1, totalPrice: value });
    }

    render() {
        return (
            <div>
                <div className='row my-2'>
                    <div className='col-2'>
                    </div>
                    <div className='col'>
                        <div className="login-form bg-dark my-3 p-4">
                            <h4 className="text-center">User Info</h4>
                            <div className="row">
                                <div className="col text-center">
                                    <h4>Username:</h4>
                                </div>
                                <div className="col text-center">
                                    <h4>{this.state.user.username}</h4>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col text-center">
                                    <h4>Name:</h4>
                                </div>
                                <div className="col text-center">
                                    <h4>{this.state.user.name}</h4>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col text-center">
                                    <h4>Lastname:</h4>
                                </div>
                                <div className="col text-center">
                                    <h4>{this.state.user.lastname}</h4>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col text-center">
                                    <h4>email:</h4>
                                </div>
                                <div className="col text-center">
                                    <h4>{this.state.user.email}</h4>
                                </div>
                            </div>
                            {/* <div className="row">
                                <div className="col text-center">
                                    <h4>Role:</h4>
                                </div>
                                <div className="col text-center">
                                    <h4>{this.state.user.userType}</h4>
                                </div>
                            </div> */}
                        </div>
                    </div>
                    <div className='col-2'>
                    </div>
                </div>

                <div className='row'>
                    <div className='col-1'>
                        
                    </div>
                    <div className='col'>
                        <div>
                            <table  className="table table-hover mt-3 bg-light ">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">OrderStatus</th>
                                        <th scope="col">TimeStamp</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    { this.state.orderList.map((o, i) => (
                                    <tr key={i} onClick={() => this.seeFullOrder(i+1, o.orderItems)} style={{cursor:`pointer`}}>
                                        <th scope="row">{i+1}</th>
                                        <td>{o.orderStatus}</td>
                                        <td>{new Intl.DateTimeFormat('sr-Latn-CS', {year: 'numeric', month: '2-digit',day: '2-digit', hour: '2-digit', minute: '2-digit', second: '2-digit'}).format(new Date(o.timestamp)) }</td>
                                    </tr>
                                    ))
                                    }
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div className='col-1'>
                        
                    </div>
                    <div className='col'>
                        <h4>You are seeing order number: <b>{this.state.orderNumber}</b></h4>
                        <h4>Total Cost: <b>${this.state.totalPrice}</b></h4>
                        <table  className="table table-hover mt-3 bg-light ">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Manufacturer</th>
                                    <th scope="col">Model</th>
                                    <th scope="col">Amount</th>
                                    <th scope="col">Price</th>
                                    <th scope='col'>Total Price</th>
                                </tr>
                            </thead>
                            <tbody>
                                { this.state.orderItemList.map((o, i) => (
                                <tr key={i}>
                                    <th scope="row">{i+1}</th>
                                    <td>{o.product.manufacturer}</td>
                                    <td>{o.product.model}</td>
                                    <td>{o.amount}</td>
                                    <td>{o.price.toFixed(2)}</td>
                                    <td>{o.amount * o.price.toFixed(2)}</td>
                                </tr>
                                ))
                
                                }
                            </tbody>
                        </table>
                    </div>
                    <div className='col-1'>
                        
                    </div>
                </div>
            </div>
            
        )
    }
}

export default Profile;
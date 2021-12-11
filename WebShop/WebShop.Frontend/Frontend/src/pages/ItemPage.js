import axios from 'axios';
import React, { Component } from 'react'

class ItemPage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            product: {},
            user: {},
            quantity: 1
        }
    }

    componentDidMount() {
        axios.get(process.env.REACT_APP_WEB_SHOP_PRDUCTS_BACKEND_API + `/` + this.props.match.params.itemId)
            .then((res) => { 
                this.setState({
                    product : res.data,
                }); 
                
                // Uncomment when Users API are finished

                // axios.get(process.env.REACT_APP_WEB_SHOP_USERS_BACKEND_API + `/` + this.state.product.userId)
                // .then((res2) => { 
                // this.setState({
                //     user : res2.data,
                // }); 
                // });
            });
    }

    buy() {
        console.log(this.state);
        // Redirect to Paypal
    }


    render() {
        return (
            <div className="page-div page" style={{backgroundColor:'black'}}>
            <div className="container my-3">
                <div className="row">
                    <div className="col"/>
                    <div className="col-10">
                        <div className="row" style={{border:'1px solid white', borderRadius:'10px',  overflow: 'hidden', textOverflow: 'ellipsis'}}>
                            <div className="col-4">
                                <img 
                                    src={this.state.product.imageURL} 
                                    alt="itemImage" 
                                    style={{height:'90%', width:'90%', margin:'5%', borderRadius:'10px'}} />
                            </div>
                            <div className="col-4">
                                <div className="row m-1">
                                    <div >
                                        <h3>{this.state.product.model}</h3>
                                        <p>Manufacturer:  {this.state.product.manufacturer}</p>
                                        <p>Cost per item: ${this.state.product.price}</p>
                                        <p>Amount available: {this.state.product.amount}</p>
                                    </div>
                                    <div >
                                        <p style={{border:'1px solid white', borderRadius:'10px', padding:'1px'}}>Description: {this.state.product.description}</p>
                                    </div>
                                </div>
                            </div>
                            <div className="col-4" style={{borderLeft: '1px dashed gray'}}>
                                <h3>Profile Info</h3>
                                <img 
                                    src={this.state.user.imageURL} 
                                    alt="itemImage" 
                                    style={{height:'50%', width:'50%', margin:'5%', borderRadius:'10px'}} />
                                <p>Name: {this.state.user.name}</p>
                                <p>Email: {this.state.user.email}</p>
                                <p>Phone number: {this.state.user.phoneNumber}</p>
                            </div>
                        </div>
                        <div className="row">
                            <nav className="my-3">
                                <p>
                                    Wanted Amount: <input className="mx-2" type="number" min={1} max={this.state.product.amount} defaultValue={this.state.quantity} onChange={e => this.setState({quantity: e.target.value})} style={{width:'100px'}}/>
                                    Total Cost : <input className="mx-2" type="string" value={`$${this.state.quantity*this.state.product.price}`} readOnly style={{width:'100px'}}/>
                                </p>
                                <button type="button" className="btn btn-outline-light" onClick={this.buy.bind(this)}>Buy via paypal</button>
                            </nav>
                        </div>
                        {/* <div className="row">
                            <h3>Comment section</h3>
                        </div> */}
                    </div>
                    <div className="col"/>
                </div>
            </div>
        </div>
        )

        
    }
}

export default ItemPage

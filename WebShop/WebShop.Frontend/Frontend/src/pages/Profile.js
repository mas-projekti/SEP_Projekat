import axios from 'axios';
import React, { Component } from 'react'

export class Profile extends Component {
    constructor(){
        super();
        this.state = {
            user: {},
            orderList: []
        }
    }

    

    componentDidMount() {
        axios.get(process.env.REACT_APP_WEB_SHOP_USERS_BACKEND_API + `/` + this.props.match.params.userId)
        .then((resp) => {
            this.setState({user: resp.data});
            axios.get(process.env.REACT_APP_WEB_SHOP_ORDERS_BACKEND_API + `/user/` + this.props.match.params.userId + `/orders`)
            .then((resp) => {
                this.setState({ orderList: resp.data})
                console.log(this.state);
            })
        })
        .catch((err) => {

        })
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
                            <div className="row">
                                <div className="col text-center">
                                    <h4>Role:</h4>
                                </div>
                                <div className="col text-center">
                                    <h4>{this.state.user.userType}</h4>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className='col-2'>
                    </div>
                </div>

                <div className='row'>
                    <div className='col-1 bg-primary'>
                        AAAA
                    </div>
                    <div className='col  bg-secondary'>
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
                            <tr>
                            <th scope="row">{i+1}</th>
                            <td>{o.name}</td>
                            <td>{o.isMobile ? 'Yes' : 'No'}</td>
                            <td>{o.x}</td>
                            <td>{o.y}</td>
                            <td>
                                <button class="btn btn-outline-dark d-flex edit-btn" routerLink="/camera/{{camera.name}}" >
                                    <i class="fas fa-video me-2 mt-1 rounded"></i>
                                    <p>Info</p>
                                </button>
                            </td>
                        </tr>
                        ))
        
                        }
                    </tbody>
                </table>
                        </div>
                    </div>
                    <div className='col-1  bg-warning'>
                        AAAA
                    </div>
                    <div className='col  bg-danger'>
                        AAAA
                    </div>
                    <div className='col-1  bg-dark'>
                        AAAA
                    </div>
                </div>
            </div>
            
        )
    }
}

export default Profile;
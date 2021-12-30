import axios from 'axios';
import React, { Component } from 'react'

export class Profile extends Component {
    constructor(){
        super();
        this.state = {
            user: {}
        }
    }

    

    componentDidMount() {
        axios.get(process.env.REACT_APP_WEB_SHOP_USERS_BACKEND_API + `/` + this.props.match.params.userId)
        .then((resp) => {
            this.setState({user: resp.data});
            console.log(this.state);
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
                        <div class="login-form bg-dark my-3 p-4">
                            <h4 class="text-center">User Info</h4>
                            <div class="row">
                                <div class="col text-center">
                                    <h4>Username:</h4>
                                </div>
                                <div class="col text-center">
                                    <h4>{this.state.user.username}</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <h4>Name:</h4>
                                </div>
                                <div class="col text-center">
                                    <h4>{this.state.user.name}</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <h4>Lastname:</h4>
                                </div>
                                <div class="col text-center">
                                    <h4>{this.state.user.lastname}</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <h4>email:</h4>
                                </div>
                                <div class="col text-center">
                                    <h4>{this.state.user.email}</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col text-center">
                                    <h4>Role:</h4>
                                </div>
                                <div class="col text-center">
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
                            <th scope="col">Name</th>
                            <th scope="col">Mobile</th>
                            <th scope="col">X coordinate</th>
                            <th scope="col">Y coordinate</th>
                            <th scope="col">Last heard</th>
                            <th scope="col"></th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* { transactionList.map((t, i) => (
                            <tr>
                            <th scope="row">{i+1}</th>
                            <td>{t.name}</td>
                            <td>{t.isMobile ? 'Yes' : 'No'}</td>
                            <td>{t.x}</td>
                            <td>{t.y}</td>
                            <td>{this.datepipe.transform(t.timeStamp, "dd.MM.yyyy. HH:mm:ss")}</td>
                            <td>
                                <button class="btn btn-outline-dark d-flex edit-btn" routerLink="/camera/{{camera.name}}" >
                                    <i class="fas fa-video me-2 mt-1 rounded"></i>
                                    <p>Info</p>
                                </button>
                            </td>
                        </tr>
                        ))
        
                        } */}
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
import React from 'react'
import { useEffect } from 'react';
import { useHistory } from 'react-router-dom';

const CreateItemPage = (props) => {
    const history = useHistory();

    useEffect(() => {
        if (!props.authGuardFunction()) history.push(`/login`);
        return () => {
            
        }
    })

    return (
        <div className="page">
            {/* <MainBar/> */}
            <div className="container">
                <div className="row" style={{margin:"3%"}}/>
                <div className="row">
                    <div className="col-4"/>
                    <div className="col-4">
                        <h1>Create Neww Item</h1>
                        <div className="row my-5">
                            <div className="col">Username:</div>
                            <div className="col">
                                <input type="text"/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Password:</div>
                            <div className="col">
                                <input type="password" />
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">
                                <button type="button" className="btn btn-outline-light">Login</button>
                            </div>
                            <div className="w-100 my-0 hr">    
                                <hr/>
                            </div>
                            <div className="col">
                                <p>You don't have an account?</p>
                                <button type="button" className="btn btn-outline-light">
                                </button>
                            </div>
                        </div>
                    </div>
                    <div className="col-4" />
                </div>
            </div>
        </div>
    )
}

export default CreateItemPage

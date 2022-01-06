import axios from 'axios';
import jwtDecode from 'jwt-decode';
import React, { useState } from 'react'
import { useEffect } from 'react';
import { useHistory } from 'react-router-dom';

const CreateItemPage = (props) => {
    const history = useHistory();
    const [ item, setItem ] = useState({ manufacturer: "", model: "", description: "", imageURL: "", price: "", amount: "", categoryType: "", warranty: "", userId: ""});
    const [ errorMessage, setErrorMessage ] = useState("");

    useEffect(() => {
        if (!props.authGuardFunction()) history.push(`/login`);
        return () => {
            
        }
    })

    const createNewItem = () => {
        if (item.manufacturer === "" || item.model === "" || item.description === "" || item.imageURL === "" || item.price === "" || item.amount === "" || item.categoryType === "" || item.warranty === "") {
            setErrorMessage("You didn't fill the entire form");
            return;
        }

        let token = localStorage.getItem(`jwt`);
        if (token === null) return;
        let decodedToken = jwtDecode(token);
        setItem({...item , userId: decodedToken[`http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber`]});

        axios.post(process.env.REACT_APP_WEB_SHOP_PRODUCTS_BACKEND_API, item)
        .then(() => {
            alert("You have successfully created new item");
            history.push("/");
        })
    } 
    

    return (
        <div className="page">
            {/* <MainBar/> */}
            <div className="container">
                <div className="row" style={{margin:"3%"}}/>
                <div className="row">
                    <div className="col-2"/>
                    <div className="col-8">
                        <h1>Create New Item</h1>
                        <div className="row my-5">
                            <div className="col">Manufacturer*:</div>
                            <div className="col">
                                <input type="text" value={item.manufacturer} onChange={(ev) => setItem({...item, manufacturer: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Model*:</div>
                            <div className="col">
                                <input type="text" value={item.model} onChange={(ev) => setItem({...item, model: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Description*:</div>
                            <div className="col">
                                <input type="text" value={item.description} onChange={(ev) => setItem({...item, description: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Category Type*:</div>
                            <div className="col">
                                <select name="CategoryType" value={item.categoryType} onChange={(ev) => setItem({...item, categoryType: ev.target.value})} >
                                    <option value="CPU">CPU</option>
                                    <option value="GPU">GPU</option>
                                    <option value="HardDisk">Hard Disk</option>
                                    <option value="Motherboard">Motherboard</option>
                                    <option value="Laptop">Laptop</option>
                                    <option value="PowerSupply">Power Supply</option>
                                    <option value="RAM">RAM</option>
                                    <option value="Headset">Headset</option>
                                    <option value="Mouse">Mouse</option>
                                    <option value="Keyboard">Keyboard</option>
                                    <option value="Cooler">Cooler</option>
                                    <option value="DesktopCase">Desktop Case</option>
                                    <option value="Unknown">Unknown</option>
                                </select>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Image URL*:</div>
                            <div className="col">
                                <input type="text" value={item.imageURL} onChange={(ev) => setItem({...item, imageURL: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Price per Item*:</div>
                            <div className="col">
                                <input type="text" value={item.price} onChange={(ev) => setItem({...item, price: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Amount*:</div>
                            <div className="col">
                                <input type="text" value={item.amount} onChange={(ev) => setItem({...item, amount: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">Warranty (years)*:</div>
                            <div className="col">
                                <input type="number" value={item.warranty} onChange={(ev) => setItem({...item, warranty: ev.target.value})}/>
                            </div>
                            <div className="w-100 my-2"/>
                            <div className="col">
                                <button type="button" class="btn btn-outline-light" onClick={createNewItem}>Create</button>
                            </div>
                            { errorMessage !== "" 
                                ? 
                                <div className="bg-danger my-2 rounded">
                                    <h4>{errorMessage}</h4>
                                </div> 
                                : 
                                <div></div>
                            }
                        </div>
                    </div>
                    <div className="col-2" />
                </div>
            </div>
        </div>
    )
}

export default CreateItemPage

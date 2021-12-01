import React, { useState } from 'react'
import { useParams } from 'react-router'
// import MainBar from '../components/MainBar';

const ItemPage = () => {

    const [quantity, setQuantity] = useState(1); // '' is the initial state value

    const item = {
        id: 3981239812,
        name: 'Sava Sumanovic - Šid',
        placeHolderImg: "https://mediasfera.rs/wp-content/uploads/2019/01/sava-2-1200x894.jpg",
        cost: 15,
        amount: 80,
        description: "This is the best item you can find on the market, eventualy you can find it on buvljak on Klisa, Novi Sad :).",
    }
    
    // Item id from route
    // const params = useParams()

    // Fetch Item by id
    // const fetchItem = async (id) => {
    //     const res = await fetch(`http://localhost:5000/tasks/${id}`)
    //     const data = await res.json()
    //     return data
    // }

    // Fetch User by userId in Item 
    // const fetchUser = async (userId) => {
    //     const res = await fetch(`http://localhost:5000/tasks/${userId}`)
    //     const data = await res.json()
    //     return data
    // }

    const user = {
        imgSrc: 'https://upload.wikimedia.org/wikipedia/en/thumb/c/c7/Michael_Jordan_crying.jpg/220px-Michael_Jordan_crying.jpg',
        name: 'Dzordan Dzordanic',
        email: 'michaeljordan@email.com',
        phoneNumber: '+381623456789'
    }

    return (

        <div className="page-div page" style={{backgroundColor:'black'}}>
            {/* <MainBar/> */}
            {/* <div className="row center my-2" style={{width:'100%'}}>
                <div className="col-3" />
                <Nav className="col" style={{alignItems:'center'}}>
                    <Nav.Link href="#query=?">New Items</Nav.Link>
                    <Nav.Link href="#query=?">Best Rated Items</Nav.Link>
                    <input className="mx-3" type="text" placeholder="Enter query" />
                    <button type="button" className="btn btn-outline-light" >Search</button>
                </Nav>
                <div className="col-3" />
            </div> */}
            <div className="container my-3">
                <div className="row">
                    <div className="col"/>
                    <div className="col-10">
                        <div className="row" style={{border:'1px solid white', borderRadius:'10px',  overflow: 'hidden', textOverflow: 'ellipsis'}}>
                            <div className="col-4">
                                <img 
                                    src={item.placeHolderImg} 
                                    alt="itemImage" 
                                    style={{height:'90%', width:'90%', margin:'5%', borderRadius:'10px'}} />
                            </div>
                            <div className="col-4">
                                <div className="row m-1">
                                    <div >
                                        <h3>{item.name}</h3>
                                        <p>Cost per item: <h5>${item.cost}</h5></p>
                                        <p>Amount available: <h5>{item.amount}</h5></p>
                                    </div>
                                    <div >
                                        <p style={{border:'1px solid white', borderRadius:'10px', padding:'1px'}}>Description:<p>{item.description}</p></p>
                                    </div>
                                </div>
                            </div>
                            <div className="col-4" style={{borderLeft: '1px dashed gray'}}>
                                <h3>Profile Info</h3>
                                <img 
                                    src={user.imgSrc} 
                                    alt="itemImage" 
                                    style={{height:'50%', width:'50%', margin:'5%', borderRadius:'10px'}} />
                                <p>Name: {user.name}</p>
                                <p>Email: {user.email}</p>
                                <p>Phone number: {user.phoneNumber}</p>
                            </div>
                        </div>
                        <div className="row">
                            <nav className="my-2">
                                <p>
                                    Wanted Amount: <input className="mx-2" type="number" min={1} max={item.amount} value={quantity} onChange={e => setQuantity(e.target.value)} style={{width:'100px'}}/>
                                    Total Cost : <input className="mx-2" type="string" value={`$${quantity*item.cost}`} readonly style={{width:'100px'}}/>
                                </p>
                                <button type="button" className="btn btn-outline-light">BUY</button>
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

export default ItemPage

import React from 'react'
import { useParams } from 'react-router'
// import MainBar from '../components/MainBar';

const ItemPage = () => {

    const params = useParams();

    return (
        // <div>
        //     {/* <MainBar/> */}
        //     <div>
        //         Hello
        //     </div>
        //     <h1>Item ID: {params.itemId}</h1>
        // </div>

        <div className="page-div" style={{backgroundColor:'black'}}>
            {/* <MainBar/> */}
            <h1>ITEM NAME - Item ID : {params.itemId}</h1>
            {/* <div className="row center" style={{width:'100%'}}>
                <div className="col-3" />
                <Nav className="col" style={{alignItems:'center'}}>
                    <Nav.Link href="#query=?">New Items</Nav.Link>
                    <Nav.Link href="#query=?">Best Rated Items</Nav.Link>
                    <input className="mx-3" type="text" placeholder="Enter query" />
                    <button type="button" className="btn btn-outline-light" >Search</button>
                </Nav>
                <div className="col-3" />
            </div> */}
            <div className="container">
                <div className="row">
                    <div className="col"/>
                    <div className="col-10">
                        <div className="row" style={{height:'300px'}}>
                            <div className="col-4" style={{backgroundColor:'yellow'}}>
                                <h3>Item picture</h3>
                            </div>
                            <div className="col-6" style={{backgroundColor:'green'}}>
                                <h3>Item description</h3>
                            </div>
                            <div className="col-2" style={{backgroundColor:'purple'}}>
                                <h3>Profile Info</h3>
                            </div>
                        </div>
                        <div className="row" style={{backgroundColor:'brown'}}>
                            <h3>Button?</h3>
                        </div>
                        <div className="row" style={{backgroundColor:'red'}}>
                            <h3>Comment section</h3>
                        </div>
                    </div>
                    <div className="col"/>
                </div>
            </div>
        </div>
    )
}

export default ItemPage

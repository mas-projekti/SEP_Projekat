import React from 'react'
import { useParams } from 'react-router'
import MainBar from '../components/MainBar';

const ItemPage = () => {

    const params = useParams();

    return (
        <div>
            <MainBar/>
            <div>
                Hello
            </div>
            <h1>Item ID: {params.itemId}</h1>
        </div>
    )
}

export default ItemPage

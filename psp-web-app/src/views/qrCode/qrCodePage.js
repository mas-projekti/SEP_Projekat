import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router';

function QRCodePage() {
    const routeParams = useParams();
    const [obj, setObject] = useState(``);

    useEffect(() => {
        setObject(JSON.stringify(JSON.parse(decodeURIComponent(routeParams.obj)), null, 2));
    }, [routeParams]);
    

    return (<div>
                <div className='container mt-5'>
                    <div className='col'></div>
                    <div className='col-10'>
                        <textarea className='form-control px-3' style={{width: `70vw`, height: `30vh`, resize: `none`}} value={obj} readOnly ></textarea>
                    </div>
                    <div className='col'></div>
                </div>                
            </div>);
};

export default QRCodePage;
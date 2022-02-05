import * as React from 'react';
import CancelIcon from '@mui/icons-material/Cancel';

function TransactionError() {

    return (
      <div>
        <div className='row'/>
          <div className='row' style={{marginTop:'20vh'}}>
          <div className='col-3'/>
          <div className='col rounded bg-danger'>
              <center>
              <div>
                  <h1 className='text-white'>Transaction Error</h1>
                  <i className="fas fa-exclamation-triangle"></i>
                  <CancelIcon className='my-3' style={{ color: 'white', fontSize: 200  }} />
              </div>
              </center>
          </div>
          <div className='col-3'/>
        </div>
      </div>
    );

}
export default TransactionError;
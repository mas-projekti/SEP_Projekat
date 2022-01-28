import ReportGmailerrorredIcon from '@mui/icons-material/ReportGmailerrorred';
import * as React from 'react';

function TransactionFailed() {

    return (
      <div>
        <div className='row'/>
        <div className='row' style={{marginTop:'20vh'}}>
          <div className='col-3'/>
          <div className='col rounded bg-warning'>
            <center>
              <div>
                <h1 className='text-white'>Transaction Succeeded, <br/> but didn't notify WebShop!</h1>
                <i className="fas fa-exclamation-triangle"></i>
                <ReportGmailerrorredIcon className='my-3' style={{ color: 'white', fontSize: 200  }} />
              </div>
            </center>
          </div>
          <div className='col-3'/>
        </div>

      </div>
    );

}
export default TransactionFailed;
import * as React from 'react'
import  {useState, useEffect} from 'react';
import { useParams } from 'react-router'
import { apiClientsProvider } from './../../services/api/client-service';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import CircularProgress from '@mui/material/CircularProgress';
import TransactionFailed from './transaction-failed';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';

function TransactionPassed() {
    const routeParams = useParams();
    const [isloading, setIsLoading] = useState(true);
    const [isProcessed, setIsProcessed] = useState(false);
    const [isError, setIsError] = useState(false);

    useEffect(() => {
        if(!isProcessed)
        {
            apiClientsProvider.notifyClientTransactionFinished(routeParams.transactionId)
            .then(function(data)
            {
                setIsProcessed(true);
                setIsLoading(false);
                

            }).catch(function(error)
            {
                setIsProcessed(true);
                setIsLoading(false);
                setIsError(true);
                
            });
        }

    });

    return (
        <>
        {
         isloading === false ? (
          <Box>
          <Grid container justifyContent="center" alignItems="center" rowSpacing={1} spacing={1}>
            <Grid item xs={12}>   
            { isError ? (
                <TransactionFailed/>    

            ):(
                <div>
                    <div className='row'/>
                    <div className='row' style={{marginTop:'20vh'}}>
                        <div className='col-3'/>
                        <div className='col rounded bg-success'>
                            <center>
                            <div>
                                <h1 className='text-white'>Transaction Succeeded</h1>
                                <i className="fas fa-exclamation-triangle"></i>
                                <CheckCircleOutlineIcon className='my-3' style={{ color: 'white', fontSize: 200  }} />
                            </div>
                            </center>
                        </div>
                        <div className='col-3'/>
                    </div>
                </div>
            )} 
            </Grid>
          </Grid>
        </Box>
         ) : (
           <center>
              <CircularProgress
              size={400}
              thickness={4}/>
            </center>
            )
        }
        
       </>

    );

}
export default TransactionPassed;
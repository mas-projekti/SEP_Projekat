import * as React from 'react'
import  {useState, useEffect} from 'react';
import { useParams , useNavigate} from 'react-router'
import { apiClientsProvider } from './../../services/api/client-service';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import CircularProgress from '@mui/material/CircularProgress';

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
                <center><h1>Transaction Failed!</h1></center>     

            ):(
                <center><h1>Transaction successfull!</h1></center>  

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
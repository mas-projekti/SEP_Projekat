import * as React from 'react'
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';


function TransactionError() {

    return (
        <>

          <Box>
          <Grid container justifyContent="center" alignItems="center" rowSpacing={1} spacing={1}>
            <Grid item xs={12}>   
                <center><h1>Transaction could not be finished bacuse of internal error!</h1></center>     
            </Grid>
          </Grid>
        </Box>
        </>

    );

}
export default TransactionError;
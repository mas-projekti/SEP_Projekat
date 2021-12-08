import * as React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

function ccyFormat(num) {
    return `${num.toFixed(2)}`;
  }
  
  
  function total(items) {
    let sum = 0;
    items.forEach(item =>{
      sum += item.quantity * item.value
    });

    return sum
  }


function OrderBreakdown(props) {
    return (
        <TableContainer component={Paper}>
        <Table sx={{ minWidth: 700 }} aria-label="spanning table">
          <TableHead>
          <TableRow>
              <TableCell align="center" colSpan={5}>
                <b>
               ORDER BREAKDOWN
                </b>
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell align="center" colSpan={4}>
                <b>
                Details
                </b>
              </TableCell>
              <TableCell align="right"><b>Price</b></TableCell>
            </TableRow>
            <TableRow>
              <TableCell><b>Name</b></TableCell> 
              <TableCell><b>Desc</b></TableCell>
              <TableCell align="right"><b>Qty.</b></TableCell>
              <TableCell align="right"><b>Unit</b></TableCell>
              <TableCell align="right"><b>Sum</b></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {props.items.map((item) => (
              <TableRow key={item.name}>
                 < TableCell>{item.name}</TableCell>
                <TableCell>{item.description}</TableCell>
                <TableCell align="right">{item.quantity}</TableCell>
                <TableCell align="right">{item.value} {item.currency}</TableCell>
                <TableCell align="right">{ccyFormat(item.value * item.quantity)} {item.currency}</TableCell>
              </TableRow>
            ))}
            { props.items[0]? (
            <TableRow>
              <TableCell rowSpan={4} />
              <TableCell colSpan={3}><b>TOTAL</b></TableCell>
              <TableCell align="right">{ccyFormat(total(props.items))} {props.items[0].currency}</TableCell>
            </TableRow>) : (
              <div></div>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    
    );
}

export default OrderBreakdown;
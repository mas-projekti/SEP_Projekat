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
    return items.map(({ value }) => value).reduce((sum, i) => sum + i, 0);
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
                <TableCell align="right">{item.value / item.quantity} {item.currency}</TableCell>
                <TableCell align="right">{ccyFormat(item.value)} {item.currency}</TableCell>
              </TableRow>
            ))}
  
            <TableRow>
              <TableCell rowSpan={4} />
              <TableCell colSpan={3}><b>TOTAL</b></TableCell>
              <TableCell align="right">{ccyFormat(total(props.items))} {props.items[0].currency}</TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </TableContainer>
    
    );
}

export default OrderBreakdown;
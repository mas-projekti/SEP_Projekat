import PropTypes from "prop-types";
import { Button } from "react-bootstrap";
import "./../styles/Item.css";
import axios from 'axios';

const Course = (props) => {
  return (
      <div className="col cardHover" style={{ cursor: "pointer" }}>
        <div className="card">
          <div className="card-body" style={cardStyle}>
            <h3 className="card-title">
              {props.course.name}
            </h3>
          </div>
          <div className="card-body" style={cardStyle}>
            <h5>Price: ${props.course.price}</h5>
            <h5>Subscription period: {props.course.subscriptionPeriod}</h5>
            <p
              className="card-text"
              style={{
                textOverflow: "ellipsis",
                overflow: "hidden",
                height: "30px",
              }}
            >
              Description: {props.course.description}
            </p>
          </div>
          <Button onClick={(data) => subscribeToCourse(props.course)}>Subscribe</Button>
        </div>
      </div>
  );
};

function subscribeToCourse(course)
{
    console.log(course)
    const BASE_URL = "https://localhost:44313";
    const PSP_FRONT = "http://localhost:3000/checkout/";
    const config = {
     headers: { Authorization: `Bearer ${localStorage.getItem("psp-token")}` },
    };
    
    // Lista za PSP
    let listOfOrders = []
    listOfOrders.push({
        name: course.name,
        description: course.description,
        quantity: 1,
        currency: "USD",
        value: course.price,
        merchantId: null
    });

    let min = Math.ceil(1);
    let max = Math.floor(100000);
    let num = Math.floor(Math.random() * (max - min) + min); 
    let bankTransData = {
        merchantID: course.user.bankMerchantID,
        merchantPassword: course.user.merchantPassword,
        amount: course.price,
        merchantOrderId: num,
        merchantTimestamp:  new Date(),
        bankURL: course.user.bankURL
    }

    let transaction = {
        items:listOfOrders,
        bankTransactionData:bankTransData,
        subscriptionTransaction:{
            id:0,
            subscriptionPlanId:course.planId,
        }
    }


    axios.post(`${BASE_URL}/payment-service/transactions`, transaction, config)
    .then((pspResp) => {
        const putanjica = PSP_FRONT + pspResp.data.id;
        window.open(putanjica, "_self");

    });


}

Course.defaultProps = {
  id: "",
  price: "",
  name: "",
  description: "",
  subscriptionPeriod: "",
  planId: "",
};

// After connecting to server set to .isRequired
Course.propTypes = {
  id: PropTypes.string,
  price: PropTypes.string,
  name: PropTypes.string,
  model: PropTypes.string,
  subscriptionPeriod: PropTypes.string,
  desription: PropTypes.string,

  onClick: PropTypes.func,
};

const cardStyle = {
  color: "black",
  cursor: "pointer",
  transition: "all 0.3s ease 0s",
};

export default Course;

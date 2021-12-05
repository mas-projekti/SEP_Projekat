import PropTypes from "prop-types";
import "./../styles/Item.css";
import { Link } from "react-router-dom";

const Item = (props) => {
  return (
    <Link to={"/item/" + props.product.id}>
      <div className="col cardHover" style={{ cursor: "pointer" }}>
        <div className="card">
          <div className="card-body" style={cardStyle}>
            <h3 className="card-title">
              {props.product.manufacturer + " " + props.product.model}
            </h3>
          </div>
          <img
            src={props.product.imageURL}
            className="card-img-top"
            alt=""
            style={{ height: "200px" }}
          />
          <div className="card-body" style={cardStyle}>
            <h5>Price: ${props.product.price}</h5>
            <h5>Amount available: {props.product.amount}</h5>
            <p
              className="card-text"
              style={{
                textOverflow: "ellipsis",
                overflow: "hidden",
                height: "30px",
              }}
            >
              Description: {props.product.description}
            </p>
          </div>
        </div>
      </div>
    </Link>
  );
};

Item.defaultProps = {
  id: "",
  price: "",
  manufacturer: "",
  model: "",
  categoryType: "",
  warranty: "",
  desription: "",
  amount: "",
  imageUrl: "",
};

// After connecting to server set to .isRequired
Item.propTypes = {
  id: PropTypes.string,
  price: PropTypes.string,
  manufacturer: PropTypes.string,
  model: PropTypes.string,
  categoryType: PropTypes.string,
  warranty: PropTypes.string,
  desription: PropTypes.string,
  amount: PropTypes.string,
  imageUrl: PropTypes.string,

  onClick: PropTypes.func,
};

const cardStyle = {
  color: "black",
  cursor: "pointer",
  transition: "all 0.3s ease 0s",
};

export default Item;

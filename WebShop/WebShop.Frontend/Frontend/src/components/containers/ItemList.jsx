import React, { Component } from "react";
import Item from "../Item";
import axios from "axios";

class ItemList extends Component {
  state = {
    products: [],
  };

  componentDidMount() {
    axios
      .get(process.env.REACT_APP_WEB_SHOP_PRDUCTS_BACKEND_API + `/all`)
      .then((res) => {
        const products = res.data;
        this.setState({ products });
      });
  }

  render() {
    return this.state.products.map((product) => (
      <Item key={product.id} product={product} />
    ));
  }
}

export default ItemList;

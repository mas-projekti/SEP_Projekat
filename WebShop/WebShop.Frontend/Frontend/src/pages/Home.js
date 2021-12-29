// import MainBar from "../components/MainBar"
import { Nav } from "react-bootstrap";
import ItemList from "../components/containers/ItemList";
import { useEffect } from "react";
import { apiIdentityProvider } from "../services/identityService";
import axios from "axios";

const BASE_URL = "https://localhost:44313";
const PSP_FRONT = "http://localhost:3000/checkout/";

const Home = () => {
  useEffect(() => {
    apiIdentityProvider
      .getPSPToken("klijentneki", "tajnovitatajna", ["paypal-api"])
      .then(function (resp) {
        localStorage.setItem("psp-token", resp.data.access_token);
      });
  }, []);

  function buyProducts() {
    const listOrders = [
      {
        id: 0,
        name: "Kompjuter",
        description: "racunarcic malii",
        quantity: 1,
        currency: "USD",
        value: 500,
        merchantId: "KXJ2PH4QBBC9N",
      },
      {
        id: 0,
        name: "Tastatura",
        description: "sve kafane zatvorio lola",
        quantity: 5,
        currency: "USD",
        value: 20,
        merchantId: "KXJ2PH4QBBC9N",
      },
    ];

    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("psp-token")}` },
    };
    axios
      .post(`${BASE_URL}/payment-service/transactions`, listOrders, config)
      .then(function (data) {
        console.log(data.data);
        const putanjica = PSP_FRONT + data.data.id;
        console.log(putanjica);
        window.open(putanjica);
      });
  }

  return (
    <div className="page-div page" style={{ backgroundColor: "black" }}>
      {/* <MainBar/> */}
      <h1>WebShop MAS</h1>
      <div className="row center" style={{ width: "100%" }}>
        <div className="col-3" />
        <Nav className="col" style={{ alignItems: "center" }}>
          <Nav.Link href="#query=?">New Items</Nav.Link>
          <Nav.Link href="#query=?">Best Rated Items</Nav.Link>
          <input className="mx-3" type="text" placeholder="Enter query" />
          <button type="button" className="btn btn-outline-light">
            Search
          </button>
        </Nav>
        <div className="col-3" />
      </div>
      <div className="container">
        <div className="row">
          <div className="col" />
          <div className="col-10">
            <div className="row row-cols-1 row-cols-md-3 g-3 my-0">
              <ItemList />
              <button
                type="button"
                className="btn btn-outline-light"
                onClick={buyProducts}
              >
                Buy products example
              </button>
            </div>
          </div>
          <div className="col" />
        </div>
      </div>
    </div>
  );
};

export default Home;

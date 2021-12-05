// import MainBar from "../components/MainBar"
import { Nav } from "react-bootstrap";
import ItemList from "../components/containers/ItemList";

const Home = () => {
  return (
    <div className="page-div page" style={{ backgroundColor: "black" }}>
      s{/* <MainBar/> */}
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
            </div>
          </div>
          <div className="col" />
        </div>
      </div>
    </div>
  );
};

export default Home;

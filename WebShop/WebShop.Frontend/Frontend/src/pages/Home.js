import Item from "../components/Item"
import MainBar from "../components/MainBar"
import { Nav } from "react-bootstrap"

const Home = () => {

    /* 1)   const fetchTasks = async () => {
            const res = await fetch('http://localhost:5000/tasks')
            const data = await res.json()
            return data
            }
            
       2)   {items.map((item, index) => (
                <Item key={index} item={item} />
            ))}
    */

    const items = [
        {
            title: "Proba1",
            imgSrc: "https://s.cdnmpro.com/921139424/p/m/9/led-sijalica-e27%E2%80%9315w~479.jpg",
            description: "Light1",
            cost: 20,
            ammount: 15
        },
        {
            title: "Proba2",
            imgSrc: "https://uniortools.com/res/katimages/817%23601840_1024.jpg",
            description: "Light2",
            cost: 50,
            ammount: 40
        }, 
        {
            title: "Proba3",
            imgSrc: "https://www.zilan.com.tr/Upload/Dosyalar/resim-jpg/zln1945-zln1952-zln1969-zln197-818bc0c9-a40e-4f18-8e0a-ba643478612d.jpg",
            description: "Light3",
            cost: 30,
            ammount: 55
        }, 
        {
            title: "Proba4",
            imgSrc: "https://m.media-amazon.com/images/I/612gOvBCvJL._AC_SL1200_.jpg",
            description: "Light3",
            cost: 100,
            ammount: 20
        }
    ] 

    

    return (
        <div className="page-div" style={{backgroundColor:'black'}}>
            <MainBar/>
            <h1>WebShop MAS</h1>
            <div className="row center" style={{width:'100%'}}>
                <div className="col-3" />
                <Nav className="col" style={{alignItems:'center'}}>
                    <Nav.Link href="#query=?">New Items</Nav.Link>
                    <Nav.Link href="#query=?">Best Rated Items</Nav.Link>
                    <input className="mx-3" type="text" placeholder="Enter query" />
                    <button type="button" className="btn btn-outline-light" >Search</button>
                </Nav>
                <div className="col-3" />
            </div>
            <div className="container">
                <div className="row">
                    <div className="col"/>
                    <div className="col-10">
                        <div className="row row-cols-1 row-cols-md-3 g-3 my-0">
                            {items.map((item, index) => (
                                <Item key={index}  
                                title={item.title} 
                                imgSrc={item.imgSrc}
                                description={item.description}
                                cost={item.cost}
                                ammount={item.ammount}/>
                            ))}
                        </div>
                    </div>
                    <div className="col" />
                </div>
            </div>
            
        </div>
    )
}

export default Home

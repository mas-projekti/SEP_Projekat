import Item from "../components/Item"
import MainBar from "../components/MainBar"

const Home = () => {
    return (
        <div className="page-div" style={{backgroundColor:'black'}}>
            <MainBar/>
            <h1>Homepage</h1>
            <div className="container">
                <div className="row">
                    <div className="col"/>
                    <div className="col-10">
                        <div className="row row-cols-1 row-cols-md-3 g-4 my-5">
                            <Item   title="Proba1" 
                                    imgSrc="https://s.cdnmpro.com/921139424/p/m/9/led-sijalica-e27%E2%80%9315w~479.jpg"
                                    description="Light"
                                    cost={20}
                                    ammount={15}/>

                        </div>
                    </div>
                    <div className="col" />
                </div>
            </div>
            
        </div>
    )
}




export default Home

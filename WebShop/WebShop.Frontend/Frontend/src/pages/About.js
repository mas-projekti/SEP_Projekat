import React from 'react'
import nm from "../img/nm.jpeg"
import pg from "../img/pg.jpeg"
import mk from "../img/mk.jpeg"
// import MainBar from '../components/MainBar'


export const About = () => {
    return (
        <div className="page">
            {/* <MainBar/> */}
            <div className="container">
                <div className="row">
                    <div className="col"/>
                    <div className="col-10">
                        <h1>About</h1>
                        <p>
                            This is modest Web Shop frontend application created in React technology.
                            Purpose of this application is to give the user basic insight of items that
                            are on market. User can purchase items from this items via PSP (Payment Service Protocol)
                            which is also implemented by the same team. Team consists of 3 master students of computer sciences.
                        </p>
                        <h3>Designed by</h3>
                        <div className="row">
                            <div className="col-4">
                                <img src={pg} alt="PG" width="200px" height="200px" className="rounded-circle"/>
                                <h4>
                                    Predrag Glavaš<br/>E2 38/2021
                                </h4>
                            </div>
                            <div className="col-4">
                                <img src={nm} alt="NM" width="200px" height="200px" className="rounded-circle"/>
                                <h4>
                                    Nikola Mijonić<br/>E2 45/2021
                                </h4>
                            </div>
                            <div className="col-4">
                                <img src={mk} alt="MK" width="200px" height="200px" className="rounded-circle" />
                                <h4>
                                    Milan Knežević<br/>E2 59/2021
                                </h4>
                            </div>
                        </div>
                    </div>
                    <div className="col" />
                </div>
            </div>
        </div>
    )
}

export default About

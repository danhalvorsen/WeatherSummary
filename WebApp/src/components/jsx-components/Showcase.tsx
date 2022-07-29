function Navbar() {

    return (
        <div className=" p-5 text-center text-sm-start">
            <div className="container">
                <div className="d-sm-flex align-items-center justify-content-between">
                    <div className="row align-items-center d-flex">
                        <h1>which one of these forecasts <span className="text-warning"> seem truth?</span></h1>
                        
                        <p className="lead"> You're going to choose one of the most relevant weather in compare with others.
                            then, it will been shown to you some more details of weather situation.
                        </p>
                        <div><button className="btn btn-primary btn-lg">More about weather compare</button></div>
                    </div>
                    
                    {/* <img src={pic} alt="pic" /> */}
                    <img className="img-fluid w-50 d-none d-sm-block" src={require("../../resources/images/weather-image.webp")} alt="weather graphity" />

                </div>
            </div>
        </div>
    )

}

export default Navbar;

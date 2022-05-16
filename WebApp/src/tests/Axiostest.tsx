import React from "react";
import axios from "axios";

function Axiostest (){


    type resultFormat = {
        source: Array<number>
    }

    axios.get<resultFormat>(`http://localhost:3000/source`)
            .then(result => {
            console.log('dddddddddddddddddd');
            console.log(result.data);
            })



            
return(
    <>
    
    </>
)
}




export default Axiostest;

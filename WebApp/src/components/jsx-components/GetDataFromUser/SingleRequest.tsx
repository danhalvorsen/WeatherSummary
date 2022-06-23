import { useCallback, useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { IresultJson as IResultJson } from "../../../Interfaces";

interface Iprops {
    singledate: Date,
    cityName: string
}

export async function SingleRequest({singledate, cityName}:Iprops){

    const [result , setResult] = useState('a');


    //const setComponentState = useCallback(async()=>{ 

    const node = "http://localhost:3000/data";
    const data = await connection(node)
    console.log(data);

//       if (data !== undefined && data !== null && data.length > 0) {
//       setResult(data);
//    } 

       
    console.log('this is for single request');
    console.log(singledate);
    console.log(cityName);


    return(
    <>
    </>
)
         }

         async function connection(node: string){
             const resulted = await axios.get(node);
            return resulted.data;
         }
        


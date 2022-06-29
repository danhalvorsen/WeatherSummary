import { FC, useCallback, useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { IresultJson as IResultJson } from "../../../Interfaces";

interface Iprops {

    date: Date,
    cityName: string
    }

export const SingleRequest: FC<Iprops> = ({date , cityName})=>{

    console.log('I am SingleRequest')
    console.log(cityName);
    console.log(date);
return(
    <>
   <div> Single Request Table would be here</div>
   
   <div>City is: {cityName}</div>

   <div>Date is: {date.toISOString()}</div>
    </>
)

}






//   //  export async function SingleRequest({date,cityName}):Iprops{

//     const [result , setResult] = useState('a');


//     //const setComponentState = useCallback(async()=>{ 

//     const node = "http://localhost:3000/data";
//     const data = await connection(node)
//     console.log(data);

// //       if (data !== undefined && data !== null && data.length > 0) {
// //       setResult(data);
// //    } 

       
//     console.log('this is for single request');
//     console.log(singledate);
//     console.log(cityName);


//     return(
//     <>
//     </>
// )
//          }

//          async function connection(node: string){
//              const resulted = await axios.get(node);
//             return resulted.data;
//          }
        


import React , { FC, useCallback, useEffect, useState } from "react";
import axios, { AxiosResponse } from "axios";
import { IresultJson as IResultJson } from "../../../Interfaces";
//import Table from "./Table";
import { IresultJson } from "../../../Interfaces"


export interface Iprops {
    date: Date ,
    cityName: string,
   
    }

export const  SingleRequest: FC<Iprops> =  ({date , cityName})=>{

    const [result , setResult] = useState<IresultJson[]>();

    const source = "http://localhost:3000/data/?weatherType=sunny"
    useEffect(()=>{

        const getRequest = async ()=>{
      
        const request: IresultJson[]  = await axios.get(source);

        if (request !== undefined && request !== null ) {
                   setResult(request);
                } 

               };

        getRequest()

    },[]);
   
    console.log(result)
   const items = result?.map((item)=>{
        
    })



    return(
    <>
   
   <div>City is: {cityName}</div>

   <div>Date is: {date.toISOString()}</div>
  

   {/* <Table data={newResult}/> */}
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

       



//     return(
//     <>
//     </>
// )
//          }

//          async function connection(node: string){
//              const resulted = await axios.get(node);
//             return resulted.data;
//          }
        


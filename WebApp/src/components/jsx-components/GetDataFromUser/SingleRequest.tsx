import React, { FC, useCallback, useEffect, useState } from 'react'
import axios, { AxiosResponse } from 'axios'
import { IresultJson as IResultJson } from '../../../Interfaces'
import Table from './Table'
import { IresultJson } from '../../../Interfaces'

export interface Iprops {
    date: Date
    cityName: string
}

export const SingleRequest: FC<Iprops> = ({ date, cityName }) => {
    const [result, setResult] = useState<IresultJson[] | undefined>()
    const [component, setComponent] = useState<JSX.Element>()

    //const source = "http://localhost:3000/data"
    //const source = "http://localhost:3000/data/?weatherType=sunny"
    //const source = `https://localhost:5000/api/weatherforecast/date?DateQuery.Date=${date.toISOString()}&CityQuery.City=${cityName}`;
    const source =
        'https://localhost:5000/api/weatherforecast/between?BetweenDateQuery.From=2022-05-01&BetweenDateQuery.To=2022-06-21&CityQuery.City=stavanger'
    useEffect(() => {
        const getRequest = (async () => {
            const res = await axios.get(source)
            if (res !== undefined && res !== null) {
                if (res.status == 200) {
                    setResult(res.data)
                    //  setComponent(<Table data={result}/>)
                }
            }
        })()
    }, [])

    return (
        <>
            <div className="container bg-light rounded-3">
                <br />
                <br />
                <div>
                    the city we are searching for is: <b>{cityName}</b> and the
                    date is: <b>{date.toISOString()}</b>{' '}
                </div>
                {/* <div>City is: {cityName}</div>
      <div>Date is: {date.toISOString()}</div> */}

                {/* {component} */}
                <Table data={result} city={cityName} />
            </div>
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

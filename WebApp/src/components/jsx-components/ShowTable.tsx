import React from 'react';
import { IresultJson as IResultJson } from "../../Interfaces";


type props= {
    data: IResultJson[]
}

export const ShowTable = ({data}: props)=>{

    console.log(data[0]);



return (
    <>
    <br/>
    <div>City Name: <strong>{data[0].city}</strong> </div><br/><br/><br/>
<table className="table table-striped">
  <thead>
    <tr>
      <th scope="col">Forecast Provider</th>
      <th scope="col">Date and Time</th>
      <th scope="col">Weather Type</th>
      <th scope="col">Temperature</th>
      <th scope="col">Wind speed</th>
      <th scope="col">Wind direction</th>
      <th scope="col">Wind speed guest</th>
      <th scope="col">Pressure</th>
      <th scope="col">Humidity</th>
      <th scope="col">Prob of Rain</th>
      <th scope="col">Amount rain</th>
      <th scope="col">Cloud area fraction</th>
      <th scope="col">Fog area fraction</th>
      <th scope="col">Prob of thunder</th>
    </tr>
  </thead>
  <tbody>
    <tr>
     
      <th scope="col">{data[0].source.dataProvider}</th>
      <th scope="col">{data[0].date}</th>
      <th scope="col">{data[0].weatherType}</th>
      <th scope="col">{data[0].temperature}</th>
      <th scope="col">{data[0].windspeed}</th>
      <th scope="col">{data[0].windDirection}</th>
      <th scope="col">{data[0].windspeedGust}</th>
      <th scope="col">{data[0].pressure}</th>
      <th scope="col">{data[0].humidity}</th>
      <th scope="col">{data[0].probOfRain}</th>
      <th scope="col">{data[0].amountRain}</th>
      <th scope="col">{data[0].cloudAreaFraction}</th>
      <th scope="col">{data[0].fogAreaFraction}</th>
      <th scope="col">{data[0].probOfThunder}</th>
    </tr>
   
  </tbody>
    </table>
    </>
)
}
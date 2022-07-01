import { AxiosResponse } from "axios";
import { FC, Key, ReactChild, ReactFragment, ReactPortal, useState } from "react";
import { IresultJson } from "../../../Interfaces";

interface Iprops  {
  data: IresultJson[] | undefined
  };

const Table:FC<Iprops> = ({ data }) => {
//const [city , setCity] = useState('');
//console.log(data![0].city)
    const  contactOfData = data?.map((row , index)=> {
     // if (data[0].city) {setCity(  data[0]?.city);}
      
      return (
        <tr key={index}>
          <td>{row.source.dataProvider}</td>
          <td scope="col">{row.date}</td>
          <td scope="col">{row.weatherType}</td>
          <td scope="col">{row.temperature}</td>
          <td scope="col">{row.windspeed}</td>
          <td scope="col">{row.windDirection}</td>
          <td scope="col">{row.windspeedGust}</td>
          <td scope="col">{row.pressure}</td>
          <td scope="col">{row.humidity}</td>
          <td scope="col">{row.probOfRain}</td>
          <td scope="col">{row.amountRain}</td>
          <td scope="col">{row.cloudAreaFraction}</td>
          <td scope="col">{row.fogAreaFraction}</td>
          <td scope="col">{row.probOfThunder}</td>
        </tr>
      );
    });
  
    const headOfData = (
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
    );
  
    return (
      <div className=" p-5 text-center text-sm-start">
        <div className="container">
          <div className="d-sm-flex align-items-center justify-content-between">
            <div className="row align-items-center d-flex">
              <>
                <br />
                <div className="fs-4 col text-center">
                  <p>City Name:</p>
                  {/* <strong> {city}</strong> */}
                </div>
                <br />
                <br />
                <br />
  
                <table className="table table-striped">
                  <thead className="table-dark">{headOfData}</thead>
                  <tbody>{contactOfData}</tbody>
                </table>
              </>
            </div>
          </div>
        </div>
      </div>
    );
   }




export default Table;

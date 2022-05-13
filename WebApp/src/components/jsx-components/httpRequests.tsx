//import axios from "axios";
import axios, { AxiosResponse } from "axios";
// import { ResultFormat } from "./WeatherData";


export function MakeHttpRequest(): Array<number> 
{
    let result = axios.get<Array<number>>(`http://localhost:3000/source`);
    console.log(JSON.stringify(result, null, 4));
    return [12,12,12];
    
}

export function MakeFakeHttpRequest(): Array<number> {
    const obj = [1, 2, 3];
    return obj;
  }

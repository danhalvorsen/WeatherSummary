//import axios from "axios";
import axios, { AxiosResponse } from "axios";
import { resourceLimits } from "worker_threads";
// import { ResultFormat } from "./WeatherData";
import { IresultJson } from "../../Interfaces";

export async function MakeHttpRequest(): Promise<IresultJson> {
  let resultvalue: IresultJson | any;

  // try {
  //   const result = axios.get<IresultJson>(`http://localhost:3000/data`); 
  //   resultvalue = result 
  // } catch (error) {
  //   console.log(error);
  // }
  
  axios
    .get(`http://localhost:3000/data`)
    .then((result) => {
      console.log(result.data);
      resultvalue = result.data;
    })
    .catch((err) => console.log(err));

  
  return resultvalue;

  // let result = new Array<number>();
  // axios.get<Array<number>>(`http://localhost:3000/source`)
  // .then(res => result = res.data)
  // .catch();
  // console.log(JSON.stringify(result, null, 4));
  // console.log(result)
  // return result;
}

export function MakeFakeHttpRequest(): Array<number> {
  const obj = [1, 2, 3];
  return obj;
}

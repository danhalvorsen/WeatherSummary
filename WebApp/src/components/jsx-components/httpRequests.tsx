//import axios from "axios";
import axios, { AxiosResponse } from "axios";
import { resourceLimits } from "worker_threads";
// import { ResultFormat } from "./WeatherData";
import { IresultJson } from "../../Interfaces";

export async function MakeHttpRequest(): Promise<IresultJson[]> {
  

  const result = await axios.get(`http://localhost:3000/data`);
  return result.data;
}

export function MakeFakeHttpRequest(): Array<number> {
  const obj = [1, 2, 3];
  return obj;
}

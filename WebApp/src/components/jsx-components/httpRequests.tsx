
import axios, { AxiosResponse } from "axios";


import { IresultJson } from "../../Interfaces";

export async function MakeHttpRequest(node: string): Promise<IresultJson[]> {
  
 // const URL = "http://localhost:3000/data";

  const result = await axios.get(node);
  return result.data;
}

export function MakeFakeHttpRequest(): Array<number> {
  const obj = [1, 2, 3];
  return obj;
}

//import axios from "axios";
import axios, { AxiosResponse } from "axios";
import { resourceLimits } from "worker_threads";
// import { ResultFormat } from "./WeatherData";


export async function MakeHttpRequest():  Promise< Array<number>>
{

    type resultFormat = {
        source: Array<number>;
      } | Array<number>;

    let resultvalue :Array<number> = [];
      await axios.get<Array<number>>(`http://localhost:3000/source`).then((result) => {
       // console.log(result.data);
      
        resultvalue = result.data
      });
      
      return resultvalue
      

      
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

import { render } from "@testing-library/react";
import React from "react";
import { useState, useEffect } from "react";
import ShowHtml from "./ShowHtml";
import { IresultJson } from "../../Interfaces";

type Props = {
  city: string;
  date: string;
  fn: () => Promise<IresultJson>;
};

const WeatherData = ({ city, date, fn }: Props) => {
  const [result, setResult] = useState<Array<IresultJson>>();
  const [mynumber, setMynumber] = useState<number>();
  const [htmlElement, sethtmlElement] = useState<JSX.Element[]>();


  async function setComponentState(): Promise<void | Array<IresultJson>> {
    const getData = await fn()
    setMynumber(19);
    setResult(getData);

   
    //console.log(data.city);

    //const city = result[0].city;
   // const weatherStatus = result[0].weatherType;

    // const listItem = result.map((item:IresultJson)=>
    // <li key={item.toString()}>{item} </li>
    //   );
    // sethtmlElement(listItem)

      }


      // if(result !== undefined)
      //   if(result.city !== undefined)
      //       console.log('hello city: ' + result[0].data );
      // console.log(result?.city);

  
  return (
    <div>
      <div>
        {/* <ShowHtml data={list_items}/> */}
        
      </div>
      <div id="1000">Hello!</div>
      <button onClick={setComponentState}>CLICK ON ME</button>
      <ul>{htmlElement}</ul>
      <p>the city you've searched: <strong>{}</strong> and today's weather status is: <strong>{}</strong></p>
      
    </div>
  );
};

export default WeatherData;

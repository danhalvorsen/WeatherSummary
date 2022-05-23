import { render } from "@testing-library/react";
import React from "react";
import { useState, useEffect } from "react";
import ShowHtml from "./ShowHtml";

type Props = {
  city: string;
  date: string;
  fn: () => Array<number> | Promise<number[]>;
};

const WeatherData = ({ city, date, fn }: Props) => {

  const [result, setResult] = useState<Array<number>>([]);
  const [htmlElement, sethtmlElement] = useState<JSX.Element[]>();

  //let htmlElement: JSX.Element[] | undefined ;


  async function setComponentState(): Promise<void | number[]> {
    const getResault = await fn();
    setResult(getResault);

    const listitem = result.map((item)=>
     <li key={item.toString()}>{item} </li>
      );

    sethtmlElement(listitem)

      }

   


      
//     const numbers = [1, 2, 3, 4, 5];
//  const listItems = numbers.map((number) =>
//    <li>{number}</li>
//  );

          
      

  // useEffect(()=>{

  //   result.map((item)=>{
  //     const list_items = <li>{item} key={item.toString()}</li>
  //     setResultHtml(list_items)
  //   })

  //   return ()=>{     
  //     }
      
  //   }
  // )



  
  return (
    <div>
      <div>
        {/* <ShowHtml data={list_items}/> */}
        
      </div>
      <div id="1000">Hello!</div>
      <button onClick={setComponentState}>CLICK ON ME</button>
      <ul>{htmlElement}</ul>
    </div>
  );
};

export default WeatherData;

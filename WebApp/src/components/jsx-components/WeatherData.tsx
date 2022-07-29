import React, { useCallback } from "react";
import { useState, useEffect } from "react";
import { IresultJson as IResultJson } from "../../Interfaces";
import {ShowTable} from  './ShowTable'

type Props = {
  city: string;
  date: string;
  fn: (node: string) => Promise<Array<IResultJson>>;
};

const WeatherData = ({ city, date, fn }: Props) => {
  const [result, setResult] = useState<Array<IResultJson>>([]);

  const [htmlElement, sethtmlElement] = useState<JSX.Element[]>();

  const [flag , setFlag] = useState(false);


  const setComponentState = useCallback(async () => {
    const node = "http://localhost:3000/data";
    const data = await fn(node);
    if (data !== undefined && data !== null && data.length > 0) {
      setResult(data);
      setFlag(true);
    }

  }, [setResult, fn]);



  const weatherData = result.map((item) =>
    
  <div>{item.city}-{item.date}</div>);
  
  let showTable : JSX.Element | undefined;
  if (flag == true) {
   showTable = <ShowTable data={result}/>
  }

  return (
    <div>
      <div>
        {/* <ShowHtml data={list_items}/> */}
      </div>
      {/* <div id="1000">Hello!</div> */}
      <button type="button" className="btn btn-success" onClick={() => setComponentState()}>Search City</button>
      {/* <ul>{htmlElement}</ul> */}
      {/* <p>the city you've searched: <strong>{ }</strong> and today's weather status is: <strong>{ }</strong></p> */}
      {/* <div>{weatherData}</div> */}

      <div>
      {showTable}

      </div>
    </div>
  );
};

export default WeatherData;

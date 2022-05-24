import { render } from "@testing-library/react";
import React, { useCallback } from "react";
import { useState, useEffect } from "react";
import ShowHtml from "./ShowHtml";
import { IresultJson as IResultJson } from "../../Interfaces";

type Props = {
  city: string;
  date: string;
  fn: () => Promise<Array<IResultJson>>;
};

const WeatherData = ({ city, date, fn }: Props) => {
  const [result, setResult] = useState<Array<IResultJson>>([]);

  const [htmlElement, sethtmlElement] = useState<JSX.Element[]>();

  const setComponentState = useCallback(async () => {
    const data = await fn();
    if (data !== undefined && data !== null && data.length > 0) {
      setResult(data);
    }

  }, [setResult, fn]);


  const weatherData = result.map((item) =>
  <div>{item.city}</div>);

  return (
    <div>
      <div>
        {/* <ShowHtml data={list_items}/> */}

      </div>
      <div id="1000">Hello!</div>
      <button onClick={() => setComponentState()}>CLICK ON ME</button>
      {/* <ul>{htmlElement}</ul> */}
      <p>the city you've searched: <strong>{ }</strong> and today's weather status is: <strong>{ }</strong></p>
      <div>{weatherData}</div>
      
    </div>
  );
};

export default WeatherData;

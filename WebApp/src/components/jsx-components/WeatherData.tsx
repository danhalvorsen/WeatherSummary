import { render } from "@testing-library/react";
import React from "react";
import { useState } from "react";

 

type Props = {
  city: string;
  date: string;
  fn: () => Array<number>;
};

const WeatherData = ({ city, date, fn }: Props) => {
  const [result, setResult] = useState({});

  function setComponentState(): void {
    setResult(fn());
    console.log(result);
  }

  // const list_items = (result !== undefined) ? result.map((x) => <li>{x}</li>) : {};

  return (
    <div>
      {/* <div>
        <ul>{list_items}</ul>
      </div> */}
      <div id="1000">Hello!</div>
      <button onClick={setComponentState}>CLICK ON ME</button>
    </div>
  );
};

export default WeatherData;

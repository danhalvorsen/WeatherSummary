import { Children, FC, useContext } from "react";
import { sampleContext } from "./Form/WeatherForcastSearchState";

type lookupCityFieldhProps = {
  children?: JSX.Element | JSX.Element[],
  state?: boolean,
  cityName?: string,
  value?: string

};


export const LookupCityField = (props?: lookupCityFieldhProps): JSX.Element => {

  const things = useContext(sampleContext)
  
  
  return (
    <>

          
      <label>Search:</label> <input type="text" placeholder="City Name..." />

      <h2>{things as string}</h2>

      
    </>)
}
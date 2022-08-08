import { Children, FC, useContext } from "react";
import { SearchButton } from "./SearchButton";

type lookupCityFieldhProps = {
  children?: JSX.Element | JSX.Element[],
  state?: boolean,
  cityName?: string,
  value?: string

};


export const LookupCityField = (props?: lookupCityFieldhProps): JSX.Element => {

  
  
  return (
    <>

       <br/>   
      <label>Search:</label> <input type="text" placeholder="City Name..." value={props?.cityName} />
      <SearchButton/>


      
    </>)
}
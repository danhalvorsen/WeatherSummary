import { FC } from "react";

type lookupCityFieldhProps = {
    state?: boolean,
    children?: JSX.Element | JSX.Element[]
};

export const LookupCityField = (props?: lookupCityFieldhProps): JSX.Element => {
    return (
        <>

      <label>Search:</label> <input type="text" placeholder="City Name..."/>
     
   
        {props?.children}
        </>)
}
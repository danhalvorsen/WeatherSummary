import { Children, FC, useContext, useState } from "react";
import { SearchButton } from "./SearchButton";

type lookupCityFieldhProps = {
  children?: JSX.Element | JSX.Element[],
  state?: boolean;
  value?: string,
  cityName: (newCityName: string) => void
};


export const LookupCityField = (props?: lookupCityFieldhProps): JSX.Element => {

  let  buffer: string = '';

  const handleChange = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if(e.key == 'Enter')
    {
      props?.cityName(buffer)
      buffer = "";
    }
      
    else
    {

      if( e.key !== undefined &&
          (e.key == 'A' || e.key == 'a') || 
          (e.key == 'B' || e.key == 'b'))
      buffer += e.key;
    }
      
  }

  
  return (
    <>
      <div className='border border-success mx-5 mb-2'>
        <br />
        <label>Search:</label> <input type="text" placeholder="City Name..." onKeyDown={handleChange}  />
        <SearchButton />
      </div>


    </>)
}
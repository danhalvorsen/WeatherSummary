import { Children, FC, useContext, useState } from "react";
import { SearchButton } from "./SearchButton";

type lookupCityFieldhProps = {
  children?: JSX.Element | JSX.Element[],
  state?: boolean;
  value?: string,
  cityName: (newCityName: string) => void
};


export const 
LookupCityField = (props?: lookupCityFieldhProps): JSX.Element => {

  let  buffer: string = '';
  const alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';

  const handleChange = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if(e.key == 'Enter')
    {
      props?.cityName(buffer);
      buffer = "";
      return;
    } 
    if(e.key == 'Backspace')
    {
      buffer = buffer.substring(0, buffer.length-1);
    }

    if( e.key !== undefined && alphabet.includes(e.key))
      buffer += e.key;
    
  }

  
  return (
    <>
      <div className='border border-success mx-5 mb-2'>
        <br/>
        <label>Search:</label> <input type="text" placeholder="City Name..." onKeyDown={handleChange}  />
        <SearchButton />
      </div>


    </>)
}
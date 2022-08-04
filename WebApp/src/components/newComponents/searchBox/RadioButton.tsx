import { FC } from "react";
import { FromDate } from "./FromDate"
import { ToDate } from "./ToDate"



type RadioButtonProps = {
    state?: boolean
    children?: JSX.Element|JSX.Element[]
};

export const RadioButton = (props?: RadioButtonProps) : JSX.Element  => {


    if(props?.state) 
    return ( <><h2>RadioButton T</h2>{props?.children}</>) 
    else
    return (
         <>
            
            <label>RadioButton</label>
            <input type="radio"/>
            {props?.children}
         </>
         ) 

  
    
    
  

}


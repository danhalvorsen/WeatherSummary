import { FC } from "react";
import { FromDate } from "./FromDate"
import { ToDate } from "./ToDate"
import {WeekPick} from "./WeekPick"


type RadioButtonProps = {
    state?: boolean
    children?: JSX.Element|JSX.Element[]
};

export const RadioButton = (props?: RadioButtonProps) : JSX.Element  => {


    if(props?.state) 
    return ( (<><h2>TRUE</h2>{props?.children}</>)) 
    else
    return ( (<><h2>FALSE</h2>{props?.children}</>)) 

  
    
    
  

}


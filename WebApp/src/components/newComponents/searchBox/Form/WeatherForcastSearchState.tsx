import { Children } from "../../compTypes";
import React, { useState } from 'react';
import {WeatherForcastSearch} from '../WeatherForcastSearch'


export type weatherForcastSearchStatetype =   {
    children?: Children
    name: string,
    
};

export const WeatherForcastSearchState = (props :weatherForcastSearchStatetype) => {

    //Define States
    
    //const [cityName , setCityName] = useState('Stavanger');
    // const [choiceDate , setChoiceDate] = useState(new Date());
    // const [isChecked , setIsChecked] = useState( true );
    //const [weekNo , setWeekNo] = useState(14);
    // const [fromDate , setFromDate] = useState(new Date());
    // const [ToDate , setToDate] = useState(new Date());



    


    return(
        <h1>{props.name}</h1>
        
    )
}



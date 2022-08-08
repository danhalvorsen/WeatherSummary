import { Children } from "../../compTypes";
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from '../WeatherForcastSearch'
import { LookupCityField } from "../LookupCityField";




export type weatherForcastSearchStatetypeProps = {
    stringDate?: string | undefined;

};

export const sampleContext = React.createContext({});


export const WeatherForcastSearchState = (props: weatherForcastSearchStatetypeProps) => {

    //Define States

    
   
    const date= new Date().toISOString();
    const dateStatic = date


    const [cityName, setCityName] = useState('Stavanger'); 
    const [choiceDate, setChoiceDate] = useState(date);
    // const [isChecked, setIsChecked] = useState(true);
    // const [weekNo, setWeekNo] = useState(14);
    // const [fromDate, setFromDate] = useState(new Date());
    // const [ToDate, setToDate] = useState(new Date());

    
    //Test data to build component static
    // const memoizedCallback = useMemo(
    //     () => {
    //         if(props.stringDate !== undefined)
    //             setChoiceDate(props.stringDate);
    //     },
    //     [],
    //   );
console.log(typeof(date))

    return (
        <>
                <h3>WeatherForcastSearchState</h3>
                
                 <WeatherForcastSearch cityName={cityName} todayDate = {choiceDate} />
                 
         

        </>

    )
}



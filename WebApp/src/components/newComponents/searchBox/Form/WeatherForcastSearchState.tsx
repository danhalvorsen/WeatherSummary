import { Children } from "../../compTypes";
import React, { useState } from 'react';
import { WeatherForcastSearch } from '../WeatherForcastSearch'
import { LookupCityField } from "../LookupCityField";


export type weatherForcastSearchStatetype = {
    children?: Children
};

export const sampleContext = React.createContext({});


export const WeatherForcastSearchState = (props: weatherForcastSearchStatetype) => {

    //Define States

    const [cityName, setCityName] = useState('Stavanger');
    const [choiceDate, setChoiceDate] = useState(new Date());
    const [isChecked, setIsChecked] = useState(true);
    const [weekNo, setWeekNo] = useState(14);
    const [fromDate, setFromDate] = useState(new Date());
    const [ToDate, setToDate] = useState(new Date());




    return (
        <>


            <sampleContext.Provider value={cityName}>

                <h3>WeatherForcastSearchState</h3>

                {props?.children}
                

            </sampleContext.Provider>

        </>

    )
}



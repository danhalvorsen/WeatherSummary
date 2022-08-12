import '../../../../../src/App.css'
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from '../../searchBox/WeatherForcastSearch'
import { LookupCityField } from "../../searchBox/LookupCityField";
import { useEffect } from 'react';

export type weatherForcastSearchStatetypeProps = {
    children?: JSX.Element | JSX.Element[]
    stringDate: string | undefined;
};

export const sampleContext = React.createContext({});

export const WeatherForcastSearchState = (props: weatherForcastSearchStatetypeProps) => {
    //Define States
    const date = new Date().toISOString();
    const dateStatic = date;
    const [cityName, setCityName] = useState('Stavanger');
    const [choiceDate, setChoiceDate] = useState(date);
    // const [isChecked, setIsChecked] = useState(true);
    const [weekNo, setWeekNo] = useState(14);
    // const [fromDate, setFromDate] = useState(new Date());
    // const [ToDate, setToDate] = useState(new Date());



    const changeCityNameState = (cityName: string) => {
        
        setCityName(cityName)
        console.log(cityName)
    }

    useEffect(() => {
console.log(cityName)
      },[cityName]);

    return (
        < >
            <h1>STATE</h1>
            <div className='border border-dark'>
                <h3>WeatherForcastSearchState</h3>
                <div className='border border-success m-2'>
                    <WeatherForcastSearch cityName={changeCityNameState} />
                </div>
            </div>
            {props.children}
        </>
    )
}



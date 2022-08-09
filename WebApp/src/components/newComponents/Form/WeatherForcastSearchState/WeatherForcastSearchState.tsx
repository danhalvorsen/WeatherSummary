import '../../../../../src/App.css'
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from '../../searchBox/WeatherForcastSearch'
import { LookupCityField } from "../../searchBox/LookupCityField";

export type weatherForcastSearchStatetypeProps = {
    stringDate?: string | undefined;
};

export const sampleContext = React.createContext({});

export const WeatherForcastSearchState = (props: weatherForcastSearchStatetypeProps) => {
    //Define States
    const date = new Date().toISOString();
    const dateStatic = date
    const [cityName, setCityName] = useState('Stavanger');
    const [choiceDate, setChoiceDate] = useState(date);
    // const [isChecked, setIsChecked] = useState(true);
    const [weekNo, setWeekNo] = useState(14);
    // const [fromDate, setFromDate] = useState(new Date());
    // const [ToDate, setToDate] = useState(new Date());
    return (
        < >
           
            <div className='border border-primary'>
                <h3>WeatherForcastSearchState</h3>
                <div className='border'>
                    <WeatherForcastSearch cityName={cityName} date={choiceDate} weekNo={weekNo} />
                </div>
            </div>
        </>
    )
}



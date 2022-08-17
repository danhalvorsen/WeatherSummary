import '../../../../../src/App.css';
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from './WeatherForcastSearch';
import { LookupCityField } from '../../searchBox/LookupCityField';
import { useEffect } from 'react';
import { ListItem } from '../../resultBox/ListItem';
import { ListState } from '../../searchBox/ListState';

export type weatherForcastSearchStatetypeProps = {
  children?: JSX.Element | JSX.Element[];
  stringDate: string | undefined;
};

export const sampleContext = React.createContext({});

export const WeatherForcastSearchState = (
  props: weatherForcastSearchStatetypeProps,
) => {
  // Define States
  const [cityName, setCityName] = useState('Stavanger');
  const [choiceDate, setChoiceDate] = useState(new Date().toISOString());
  // const [isChecked, setIsChecked] = useState(true);
  const [weekNo, setWeekNo] = useState<number>();
  const [fromDate, setFromDate] = useState<string>();
  const [toDate, setToDate] = useState<string>();
  const changeCityNameState = (cityName: string) => {
    setCityName(cityName);
    };
  const changeChoiceDate = (date: string) => {
    setChoiceDate(date);
  };
  const changeChoiceFrom = (date: string) => {
    setFromDate(date);
  };
  const changeChoiceTo = (date: string) => {
    setToDate(date);
  };
  useEffect(() => {
    console.log('Works well');
    fetch(`https://localhost:5000/api/weatherforecast/date?DateQuery.Date=${choiceDate}&CityQuery.City=${cityName}`)
    .then(response => response.json())
        
  }),
    [];

  return (
    <>
      <h1>STATE {choiceDate}</h1>
      <div className="border border-dark">
        <h3>WeatherForcastSearchState</h3> <h1>the city name now is: {cityName}</h1>
        <div className="border border-success m-2">
          <WeatherForcastSearch
            cityName={changeCityNameState}
            choiceDate={changeChoiceDate}
            choiceFromDate={changeChoiceFrom}
            choiceToDate={changeChoiceTo}
          />
          <ListState />
        </div>
      </div>
      {props.children}
    </>
  );
};

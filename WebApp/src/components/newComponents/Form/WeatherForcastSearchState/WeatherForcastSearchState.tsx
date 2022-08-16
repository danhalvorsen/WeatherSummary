import '../../../../../src/App.css';
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from '../../searchBox/WeatherForcastSearch';
import { LookupCityField } from '../../searchBox/LookupCityField';
import { useEffect } from 'react';

export type weatherForcastSearchStatetypeProps = {
  children?: JSX.Element | JSX.Element[];
  stringDate: string | undefined;
};

export const sampleContext = React.createContext({});

export const WeatherForcastSearchState = (
  props: weatherForcastSearchStatetypeProps,
) => {
  // Define States
  const date = new Date().toISOString();
  const dateStatic = date;
  const [cityName, setCityName] = useState('Stavanger');
  const [choiceDate, setChoiceDate] = useState(date);
  // const [isChecked, setIsChecked] = useState(true);
  const [weekNo, setWeekNo] = useState(14);
  const [fromDate, setFromDate] = useState(new Date().toISOString());
  const [toDate, setToDate] = useState(new Date().toISOString());

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

  return (
    <>
      <h1>STATE {choiceDate}</h1>
      <div className="border border-dark">
        <h3>WeatherForcastSearchState</h3>
        <div className="border border-success m-2">
          <WeatherForcastSearch
            cityName={changeCityNameState}
            choiceDate={changeChoiceDate}
            choiceFromDate={changeChoiceFrom}
            choiceToDate={changeChoiceTo}
          />
        </div>
      </div>
      {props.children}
    </>
  );
};

import '../../../../../src/App.css';
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from './WeatherForcastSearch';
import { LookupCityField } from '../../searchBox/LookupCityField';
import { useEffect } from 'react';
import { ListItem } from '../../resultBox/ListItem';
import { ListState } from '../../searchBox/ListState';
import { WeatherForcastEnumType } from './WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';

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
  const [weekNo, setWeekNo] = useState<number>(20);
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

  const whichOneIsSelected = (typeName: WeatherForcastEnumType) => {
    switch (typeName) {
      case WeatherForcastEnumType.WeatherForcastSearchOneDate:
        console.log('Single date selected');
        makeSingleDateApiRequest(cityName, choiceDate);
        break;

      case WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo:
        console.log('week number selected');
        makeWeekNumberApiRequest(cityName, weekNo);
        break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates:
        console.log('Between two number selected');
        makeBetweenDatesApiRequest(cityName, fromDate, toDate, choiceDate);
        break;
    }
  };
  const makeSingleDateApiRequest = (cityName: string, choiceDate: string) => {
    fetch(
      `https://localhost:5000/api/weatherforecast/date?DateQuery.Date=${choiceDate}&CityQuery.City=${cityName}`,
    ).then((response) => response.json());
  };
  const makeWeekNumberApiRequest = (
    cityName: string,
    weekNo: number | undefined,
  ) => {
    fetch(
      `https://localhost:5000/api/weatherforecast/week?Week=${weekNo}&CityQuery.City=${cityName}`,
    ).then((response) => response.json());
  };
  const makeBetweenDatesApiRequest = (
    cityName: string,
    fromDate: string | undefined,
    toDate: string | undefined,
    choiceDate: string | undefined,
  ) => {
    if (fromDate && toDate !== undefined) {
      fetch(
        `https://localhost:5000/api/weatherforecast/between?BetweenDateQuery.From=${fromDate}&BetweenDateQuery.To=${toDate}&CityQuery.City=${cityName}`,
      ).then((response) => response.json());
    } else {
      fetch(
        `https://localhost:5000/api/weatherforecast/between?BetweenDateQuery.From=${choiceDate}&BetweenDateQuery.To=${choiceDate}&CityQuery.City=${cityName}`,
      ).then((response) => response.json());
    }
  };

  useEffect(() => {
    //check condition and make API request
  }),
    [];

  return (
    <>
      <h1>STATE {choiceDate}</h1>
      <div className="border border-dark">
        <h3>WeatherForcastSearchState</h3>{' '}
        <h1>the city name now is: {cityName}</h1>
        <div className="border border-success m-2">
          <WeatherForcastSearch
            cityName={changeCityNameState}
            choiceDate={changeChoiceDate}
            choiceFromDate={changeChoiceFrom}
            choiceToDate={changeChoiceTo}
            whichOneIsSelected={whichOneIsSelected}
          />
          <ListState />
        </div>
      </div>
      {props.children}
    </>
  );
};

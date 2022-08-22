import '../../../../../src/App.css';
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from './WeatherForcastSearch';
import { LookupCityField } from '../../searchBox/LookupCityField';
import { useEffect } from 'react';
import { ListItem } from '../../resultBox/ListItem';
import { ListState } from '../../searchBox/ListState';
import { WeatherForcastEnumType } from './WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { api } from './WeatherForcastSearch/SelectSearchOptionState/api';
import {
  myDate,
  weekNo,
  WeekNoValidator,
} from './WeatherForcastSearch/SelectSearchOptionState/apiTypes';

export type weatherForcastSearchStatetypeProps = {
  children?: JSX.Element | JSX.Element[];
  stringDate: string | undefined;
};

export const WeatherForcastSearchState = (
  props: weatherForcastSearchStatetypeProps,
) => {
  // Define States
  const [cityName, setCityName] = useState('Stavanger');
  const [oneDate, setOneDate] = useState<myDate>({
    value: new Date().toISOString(),
  });
  const [weekNumber, setWeekNumber] = useState<weekNo>({ value: -1000 });
  const [fromDate, setFromDate] = useState<myDate>({ value: '' });
  const [toDate, setToDate] = useState<myDate>({ value: '' });
  const baseURL = 'https://localhost:5000/api/';
  const changeCityNameState = (cityName: string) => {
    setCityName(cityName);
  };
  const changeChoiceDate = (date: myDate) => {
    setOneDate(date);
  };
  const changeChoiceFrom = (date: myDate) => {
    setFromDate(date);
  };
  const changeChoiceTo = (date: myDate) => {
    setToDate(date);
  };

  const whichOneIsSelected = (typeName: WeatherForcastEnumType) => {
    switch (typeName) {
      case WeatherForcastEnumType.WeatherForcastSearchOneDate:
        api(baseURL).makeSingleDateApiRequest(cityName, oneDate);
        break;

      case WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo:
        api(baseURL).makeWeekNumberApiRequest(cityName, weekNumber);
        break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates:
        api(baseURL).makeBetweenDatesApiRequest(cityName, fromDate, toDate);
        break;
    }
  };

  return (
    <>
      <h1>STATE {oneDate.value}</h1>
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

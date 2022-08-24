import '../../../../../src/App.css';
import { getWeek } from 'date-fns';
import React, { useCallback, useMemo, useState } from 'react';
import { WeatherForcastSearch } from './WeatherForcastSearch';
import { LookupCityField } from '../../searchBox/LookupCityField';
import { useEffect } from 'react';
import { ListItem } from '../../resultBox/ListItem';
import { ListState } from '../../searchBox/ListState';
import { WeatherForcastEnumType } from './WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { api } from '../../../../communication/api';
import {
  myDate,
  WeekNumber as WeekNumber,
} from '../../../../communication/apiTypes';
import {
  Client,
  WeatherForecastDto,
} from '../../../../communication/api.client.generated';

export type weatherForcastSearchStatetypeProps = {
  children?: JSX.Element | JSX.Element[];
  stringDate: string | undefined;
};

export const WeatherForcastSearchState = (
  props: weatherForcastSearchStatetypeProps,
) => {
  const thisWeekNumber = getWeek(new Date());

  // Define States
  const [cityName, setCityName] = useState('Stavanger');
  const [oneDate, setOneDate] = useState<myDate>({
    value: new Date().toISOString(),
  });
  const [weekNumber, setWeekNumber] = useState<WeekNumber>({
    value: thisWeekNumber,
  });
  const [fromDate, setFromDate] = useState<myDate>({ value: '' });
  const [toDate, setToDate] = useState<myDate>({ value: '' });
  const [weatherForcase, setWeatherForcase] = useState<WeatherForecastDto[]>(
    [],
  );
  //Define States for received data
  const [singleDateData, setSingleDateData] = useState(null);

  const baseURL = 'https://localhost:5000/api/';
  const changeCityNameState = (cityName: string) => {
    setCityName(cityName);
    console.log(cityName);
  };
  const changeChoiceDate = (date: myDate) => {
    setOneDate(date);
  };
  const changeChoiceWeekNo = (weekNo: WeekNumber) => {
    setWeekNumber(weekNo);
  };
  const changeChoiceFrom = (date: myDate) => {
    setFromDate(date);
  };
  const changeChoiceTo = (date: myDate) => {
    setToDate(date);
  };

  useEffect(() => {
    const apiClient = new Client();
    // run Fetch Api when page is loaded
    const getData = (async () => {
      //api(baseURL).makeSingleDateApiRequest(cityName, oneDate);
      const res = await apiClient.date(new Date(oneDate.value), cityName);
      console.log('data from api:' + res);
      setWeatherForcase(res);
      console.log('state:' + weatherForcase);
    })();
  }, [oneDate, cityName]);

  const whichOneIsSelected = (typeName: WeatherForcastEnumType) => {
    switch (typeName) {
      case WeatherForcastEnumType.WeatherForcastSearchOneDate:
        api(baseURL).makeSingleDateApiRequest(cityName, oneDate);
        break;

      case WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo:
        api(baseURL).makeWeekNumberApiRequest(cityName, weekNumber);
        break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates:
        api(baseURL).makeBetweenDatesApiRequest(cityName, {
          from: {
            value: fromDate.value,
          },
          to: {
            value: toDate.value,
          },
        });
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
            thisWeekNumber={weekNumber}
            choiceDate={changeChoiceDate}
            ChoiceWeekNo={changeChoiceWeekNo}
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

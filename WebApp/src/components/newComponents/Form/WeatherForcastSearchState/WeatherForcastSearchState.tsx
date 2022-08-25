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
import { ToDate } from '../../searchBox/ToDate';

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
  const [weatherForecast, setWeatherForecast] = useState<WeatherForecastDto[]>(
    [],
  );

  //Define States for received data
  const [singleDateData, setSingleDateData] = useState(null);

  const listToShow: JSX.Element = <ListState oneDay={weatherForecast} />;
  const baseURL = 'https://localhost:5000/api/';
  const changeCityNameState = (cityName: string) => {
    setCityName(cityName);
    //console.log(cityName);
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

  const whichOneIsSelected = async (typeName: WeatherForcastEnumType) => {
    const apiRequest = new Client();

    switch (typeName) {
      case WeatherForcastEnumType.WeatherForcastSearchOneDate: {
        // api(baseURL).makeSingleDateApiRequest(cityName, oneDate);
        //Request for One Date:
        try {
          const result = await apiRequest.date(
            new Date(oneDate.value),
            cityName,
          );
          setWeatherForecast(result);
        } catch (error) {
          console.log(error);
        }
        break;
      }

      case WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo:
        // api(baseURL).makeWeekNumberApiRequest(cityName, weekNumber);
        //Request for a Week number:
        try {
          const result = await apiRequest.week(weekNumber.value, cityName);
          setWeatherForecast(result);
        } catch (error) {
          console.log(error);
        }

        break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates: //   }, 
      //     value: fromDate.value, //   from: { // api(baseURL).makeBetweenDatesApiRequest(cityName, {
      //   to: {
      //     value: toDate.value,
      //   },
      // });
      {
        try {
          const resultBetween = await apiRequest.between(
            new Date(fromDate.value),
            new Date(toDate.value),
            cityName,
          );
          setWeatherForecast(resultBetween);
        } catch (error) {
          console.log(error);
        }

        break;
      }
    }
  };

  useEffect(() => {
    const apiClient = new Client();
    // run Fetch Api when page is loaded
    const getData = (async () => {
      //api(baseURL).makeSingleDateApiRequest(cityName, oneDate);
      try {
        const res = await apiClient.date(new Date(oneDate.value), cityName);

        // setWeatherForecast(res);
        // console.log(weatherForecast);

        //For between Dates:
        const resultBetween = await apiClient.between(
          new Date(fromDate.value),
          new Date(toDate.value),
          cityName,
        );
        setWeatherForecast(resultBetween);
        console.log(resultBetween);
      } catch (ex) {
        console.error('API Error');
      }
    })();
  }, [oneDate, cityName]);

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
          {listToShow}
        </div>
      </div>
      {props.children}
    </>
  );
};

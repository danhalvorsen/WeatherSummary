import '../../../../../src/App.css';
import { getWeek } from 'date-fns';
import { useState } from 'react';
import { WeatherForcastSearch } from './WeatherForcastSearch';
import { useEffect } from 'react';
import { ListState } from '../../searchBox/ListState';
import { WeatherForcastEnumType } from './WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
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
  const [currentOption, setCurrentOption] = useState<WeatherForcastEnumType>(
    WeatherForcastEnumType.WeatherForcastSearchOneDate,
  );
  const [isCompareActive, setIsCompareActive] = useState<boolean>(false);


  //const [singleDateData, setSingleDateData] = useState(null);
  //const baseURL = 'https://localhost:5000/api/';
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
  const changeCompareMode = () => {
    setIsCompareActive(!isCompareActive);
  };
  const whichOneIsSelected = async (typeName: WeatherForcastEnumType) => {
    const apiRequest = new Client();

    switch (typeName) {
      case WeatherForcastEnumType.WeatherForcastSearchOneDate: {
        // api(baseURL).makeSingleDateApiRequest(cityName, oneDate);
        //Request for One Date:
        setCurrentOption(WeatherForcastEnumType.WeatherForcastSearchOneDate);
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
        setCurrentOption(WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo);
        try {
          const result = await apiRequest.week(weekNumber.value, cityName);
          setWeatherForecast(result);
        } catch (error) {
          console.log(error);
        }

        break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates: {
        setCurrentOption(
          WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates,
        );
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
    (async () => {
      try {
        const result = await apiClient.date(new Date(oneDate.value), cityName);
        setWeatherForecast(result);
      } catch (error) {
        console.error('API Error' + error);
      }
    })();
  }, [oneDate, cityName]);

  return (
    <>
      <div>
        <div className="m-2">
          <WeatherForcastSearch
            cityName={changeCityNameState}
            thisWeekNumber={weekNumber}
            choiceDate={changeChoiceDate}
            ChoiceWeekNo={changeChoiceWeekNo}
            choiceFromDate={changeChoiceFrom}
            choiceToDate={changeChoiceTo}
            whichOneIsSelected={whichOneIsSelected}
            changeCompareMode={changeCompareMode}
          />
          <ListState compare={isCompareActive} weatherList={weatherForecast} />
        </div>
      </div>
      {props.children}
    </>
  );
};

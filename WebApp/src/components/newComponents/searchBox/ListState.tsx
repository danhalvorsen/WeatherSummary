import { List } from './List';
import { WeatherForecastDto } from '../../../../../WebApp/src/communication/api.client.generated';
import { useState } from 'react';
import { ListCompare } from './ListCompare';

type props = {
  weatherList: WeatherForecastDto[];
  compare: boolean;
  children?: JSX.Element | JSX.Element[];
};

export const ListState: React.FC<props> = (props) => {
  const [providerYr, setProviderYr] = useState<WeatherForecastDto>();
  const [providerOpenWeather, setProviderOpenWeather] =
    useState<WeatherForecastDto>();

  const listOpenWeather: WeatherForecastDto[] = [];
  const listYr: WeatherForecastDto[] = [];
  let cityName: string | undefined = '';

  props.weatherList.forEach((row) => {
    cityName = row.city;

    if (row.source?.dataProvider == 'OpenWeather') {
      listOpenWeather.push(row);
    }
    if (row.source?.dataProvider == 'Yr') {
      listYr.push(row);
    }
  });

  /////////////////////create for compare by dates
  let twoArray: WeatherForecastDto[] = [];
  const compareItems = props.weatherList.map((line) => {
    twoArray.push(line);
    if (twoArray.length == 2) {
      const thisDay = line.date?.toDateString();
      const unicKey = Math.random().toString() + thisDay;

      const listCompare: JSX.Element = (
        <ListCompare key={unicKey} array={twoArray} />
      );
      twoArray = [];
      return listCompare;
    }
  });

  /////////////////

  const renderList = (
    <div>
      <List row={listOpenWeather} background="success" />
      <List row={listYr} background="danger" />
    </div>
  );
  const compareList =  compareItems ;
  const showList = props.compare ?    compareList: renderList;

  return (
    <>
      <div className="border border-danger mt-5 m-3">
        <h2>{cityName}</h2>
      </div>
      {showList}
    </>
  );
};

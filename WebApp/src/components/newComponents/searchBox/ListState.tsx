import { List } from './List';
import { WeatherForecastDto } from '../../../../../WebApp/src/communication/api.client.generated';
import { WeatherForcastEnumType } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { useState } from 'react';

type props = {
  weatherList: WeatherForecastDto[];
  compare: boolean;
};

export const ListState: React.FC<props> = (props) => {
  const [providerYr, setProviderYr] = useState<WeatherForecastDto>();
  const [providerOpenWeather, setProviderOpenWeather] =
    useState<WeatherForecastDto>();

  const listOpenWeather: WeatherForecastDto[] = [];
  const listYr: WeatherForecastDto[] = [];
  let cityName: string | undefined = '';

  props.weatherList.map((row) => {
    // row.source?.dataProvider == "OpenWeather" && <List row={row} provider={"OpenWeather"} />
    cityName = row.city;

    if (row.source?.dataProvider == 'OpenWeather') {
      listOpenWeather.push(row);
    }

    if (row.source?.dataProvider == 'Yr') {
      listYr.push(row);
    }
  });

  const renderList = (
    <div>
      <List row={listOpenWeather} background="success" />
      <List row={listYr} background="danger" />
    </div>
  );
  const compareList = (
    <div>
      <h1>Compare List </h1>
    </div>
  );
  const showList = props.compare ? compareList : renderList;

  return (
    <>
      <div className="border border-danger mt-5 m-3">
        <h2>{cityName}</h2>
        {showList}
      </div>
    </>
  );
};

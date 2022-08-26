import { List } from './List';
import { WeatherForecastDto } from '../../../../../WebApp/src/communication/api.client.generated';
import { WeatherForcastEnumType } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { useState } from 'react';

type props = {
  weatherList: WeatherForecastDto[];
  currentOption: WeatherForcastEnumType;
};

export const ListState: React.FC<props> = (props) => {
  const [providerYr, setProviderYr] = useState<WeatherForecastDto>();
  const [providerOpenWeather, setProviderOpenWeather] =
    useState<WeatherForecastDto>();

    const listOpenWeather:WeatherForecastDto[] = [];
    const listYr:WeatherForecastDto[] = [];


   props.weatherList.map((row) => {
    // row.source?.dataProvider == "OpenWeather" && <List row={row} provider={"OpenWeather"} />
    
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

  return (
    <>
      <div className="border border-danger mt-5 m-3">
        <h2>List State{}</h2>
        <h2>
          There are <strong>{props.weatherList.length} </strong> providers to
          show their Data
        </h2>
        <h3>the current option is : {props.currentOption}</h3>
        {renderList}
        {/* <List /> */}
      </div>
    </>
  );
};

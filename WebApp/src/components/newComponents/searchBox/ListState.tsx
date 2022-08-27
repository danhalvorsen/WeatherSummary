import { List } from './List';
import { WeatherForecastDto } from '../../../../../WebApp/src/communication/api.client.generated';
import { WeatherForcastEnumType } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { useState } from 'react';
import { ListCompare } from './ListCompare';

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

  const mergedList: WeatherForecastPairDto[] = [];
  let arrayIndex = 0;
  let lastDay: number | undefined = 28;
  let internalIndex = -1;
  let currentItem: WeatherForecastPairDto;

  props.weatherList.forEach((row) => {
    // row.source?.dataProvider == "OpenWeather" && <List row={row} provider={"OpenWeather"} />
    cityName = row.city;
    console.log(row.date?.getDate());
    if (row.date?.getDate() != lastDay) {
      lastDay = row.date?.getDate();
      arrayIndex += 1;
      internalIndex = -1;
    } else {
      internalIndex += 1;
      if (internalIndex == 0) {
        currentItem = new WeatherForecastPairDto();
        currentItem.first = row;
      } else {
        currentItem.second = row;
        mergedList.push(currentItem);
      }
    }

    if (row.source?.dataProvider == 'OpenWeather') {
      listOpenWeather.push(row);
    }

    if (row.source?.dataProvider == 'Yr') {
      listYr.push(row);
    }
    console.log(mergedList);
  });

  // console.log(props.weatherList[0].date?.getDate())

  const renderList = (
    <div>
      <List row={listOpenWeather} background="success" />
      <List row={listYr} background="danger" />
    </div>
  );
  const compareList = (
    <div>
      {/* <ListCompare/> */}
      {/* {generator} */}
    </div>
  );
  const showList = props.compare ? compareList : renderList;

  return (
    <>
      <div className="border border-danger mt-5 m-3">
        <h2>{cityName}</h2>
        {/* {showList} */}
        {renderList}
      </div>
    </>
  );
};

export class WeatherForecastPairDto {
  first?: WeatherForecastDto;
  second?: WeatherForecastDto;
}

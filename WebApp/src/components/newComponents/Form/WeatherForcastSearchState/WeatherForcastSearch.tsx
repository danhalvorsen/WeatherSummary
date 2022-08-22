import { Children } from '../compTypes';
import { LookupCityField } from '../../searchBox/LookupCityField';
import {
  SelectSearchOptionState,
  WeatherForcastEnumType,
} from './WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { propsType } from '../compTypes';
import { myDate } from './WeatherForcastSearch/SelectSearchOptionState/apiTypes';

type props = {
  cityName: (newCityName: string) => void;
  choiceDate: (date: myDate) => void;
  choiceFromDate: (date: myDate) => void;
  choiceToDate: (date: myDate) => void;
  whichOneIsSelected: (typeName: WeatherForcastEnumType) => void;
};

export const WeatherForcastSearch: React.FC<props> = (props) => {
  return (
    <div className="border border-danger mt-5 m-3">
      <h3>WeatherForcastSearch</h3>
      <LookupCityField cityName={props.cityName} />
      <SelectSearchOptionState
        choiceDate={props.choiceDate}
        choiceFromDate={props.choiceFromDate}
        choiceToDate={props.choiceToDate}
        whichOneIsSelected={props.whichOneIsSelected}
      />
    </div>
  );
};

import { Children } from '../Form/compTypes';
import { LookupCityField } from './LookupCityField';
import { SelectSearchOptionState } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { propsType } from '../Form/compTypes';

type props = {
  cityName: (newCityName: string) => void;
  choiceDate: (date: string) => void;
  choiceFromDate: (date: string) => void;
  choiceToDate: (date: string) => void;
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
      />
    </div>
  );
};

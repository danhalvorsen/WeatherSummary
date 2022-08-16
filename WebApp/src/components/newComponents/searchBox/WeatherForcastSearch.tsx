import { Children } from "../Form/compTypes";
import { LookupCityField } from "./LookupCityField";
import { SelectSearchOptionState } from "../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState";
import { propsType } from "../Form/compTypes";

type props = {
  cityName: (newCityName: string) => void;
};

export const WeatherForcastSearch: React.FC<props> = (props) => {
  return (
    <div className="border border-danger mt-5 m-3">
      <h3>WeatherForcastSearch</h3>
      <LookupCityField cityName={props.cityName} />
      <SelectSearchOptionState
        radioButtonCheckedOneDate={true}
        radioButtonCheckedForWeekNo={false}
        radioButtonCheckedForBetweenDates={false}
      />
    </div>
  );
};

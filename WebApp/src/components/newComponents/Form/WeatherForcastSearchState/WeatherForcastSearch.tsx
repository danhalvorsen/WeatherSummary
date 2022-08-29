import { Children } from '../compTypes';
import { LookupCityField } from '../../searchBox/LookupCityField';
import {
  SelectSearchOptionState,
  WeatherForcastEnumType,
} from './WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';
import { propsType } from '../compTypes';
import {
  myDate as WFDate,
  WeekNumber as WFWeekNumber,
} from '../../../../communication/apiTypes';

type props = {
  cityName: (newCityName: string) => void;
  choiceDate: (date: WFDate) => void;
  ChoiceWeekNo: (weekNo: WFWeekNumber) => void;
  choiceFromDate: (date: WFDate) => void;
  choiceToDate: (date: WFDate) => void;
  whichOneIsSelected: (typeName: WeatherForcastEnumType) => void;
  thisWeekNumber: WFWeekNumber;
  changeCompareMode: () => void;
};

export const WeatherForcastSearch: React.FC<props> = (props) => {
  return (
    <div className="mt-2 m-3">
      <LookupCityField cityName={props.cityName} />
      <SelectSearchOptionState
        choiceDate={props.choiceDate}
        ChoiceWeekNo={props.ChoiceWeekNo}
        choiceFromDate={props.choiceFromDate}
        choiceToDate={props.choiceToDate}
        whichOneIsSelected={props.whichOneIsSelected}
        thisWeekNumber={props.thisWeekNumber}
        changeCompareMode={props.changeCompareMode}
      />
    </div>
  );
};

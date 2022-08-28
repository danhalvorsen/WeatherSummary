import { useState } from 'react';
import { Children } from '../../../compTypes';
import { propsType } from '../../../compTypes';
import { WeatherForcastSearchOneDate } from './WeatherForcastSearchTypes/WeatherForcastSearchOneDate';
import { WeatherForcastSearchTypeWeekNo } from './WeatherForcastSearchTypes/WeatherForcastSearchTypeWeekNo';
import { WeatherForcastSearchTypeBetweenTwoDates } from './WeatherForcastSearchTypes/WeatherForcastSearchTypeBetweenTwoDates';
import { myDate, WeekNumber } from '../../../../../../communication/apiTypes';
import { CompareWeatherInputCheck } from './WeatherForcastSearchTypes/CompareWeatherInputCheck';

export enum WeatherForcastEnumType {
  WeatherForcastSearchOneDate = 1,
  WeatherForcastSearchTypeWeekNo = 100,
  WeatherForcastSearchTypeBetweenTwoDates = 200,
}
type props = {
  choiceDate: (date: myDate) => void;
  ChoiceWeekNo: (weekNo: WeekNumber) => void;
  choiceFromDate: (date: myDate) => void;
  choiceToDate: (date: myDate) => void;
  whichOneIsSelected: (typeName: WeatherForcastEnumType) => void;
  thisWeekNumber: WeekNumber;
  changeCompareMode: () => void;
};
export const SelectSearchOptionState: React.FC<props> = (props) => {
  const [stateOneDate, setStateOneDate] = useState<boolean>(true);
  const [stateWeekNo, setStateWeekNo] = useState<boolean>(false);
  const [stateBetweenDates, setStateBetweenDates] = useState<boolean>(false);

  const setState = (
    oneDate: boolean,
    weekNo: boolean,
    betweenDates: boolean,
  ): void => {
    setStateOneDate(oneDate);
    setStateWeekNo(weekNo);
    setStateBetweenDates(betweenDates);
  };

  const changeState = (typeName: WeatherForcastEnumType): void => {
    props.whichOneIsSelected(typeName);

    switch (typeName) {
      case WeatherForcastEnumType.WeatherForcastSearchOneDate:
        setState(true, false, false);
        break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo:
        setState(false, true, false);
        break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates:
        setState(false, false, true);
        break;
    }
  };

  return (
    <div className="m-3 mt-5">
      <div>
        <WeatherForcastSearchOneDate
          onChange={changeState}
          radioButtonChecked={stateOneDate}
          choiceDate={props.choiceDate}
        />
        <WeatherForcastSearchTypeWeekNo
          onChange={changeState}
          radioButtonChecked={stateWeekNo}
          ChoiceWeekNo={props.ChoiceWeekNo}
          thisWeekNumber={props.thisWeekNumber}
        />
        <WeatherForcastSearchTypeBetweenTwoDates
          onChange={changeState}
          radioButtonChecked={stateBetweenDates}
          choiceFromDate={props.choiceFromDate}
          choiceToDate={props.choiceToDate}
        />
        <CompareWeatherInputCheck changeCompareMode={props.changeCompareMode} />
      </div>
    </div>
  );
};

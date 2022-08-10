import { FC, useState } from 'react';
import { Children } from "../../../compTypes";
import { propsType } from '../../../compTypes';
import { WeatherForcastSearchOneDate } from './WeatherForcastSearchTypes/WeatherForcastSearchOneDate';
import { WeatherForcastSearchTypeWeekNo } from './WeatherForcastSearchTypes/WeatherForcastSearchTypeWeekNo';
import { WeatherForcastSearchTypeBetweenTwoDates } from './WeatherForcastSearchTypes/WeatherForcastSearchTypeBetweenTwoDates';

export enum WeatherForcastEnumType {WeatherForcastSearchOneDate = 1, WeatherForcastSearchTypeWeekNo = 100, WeatherForcastSearchTypeBetweenTwoDates = 200}

type SelectSearchOptionStateProps = {
  children?: Children;
  radioButtonCheckedOneDate: boolean;
  radioButtonCheckedForWeekNo: boolean;
  radioButtonCheckedForBetweenDates: boolean;
};
export const SelectSearchOptionState: FC<propsType> = (props) => {

  const [stateOneDate, setStateOneDate] = useState<boolean>(false);
  const [stateWeekNo, setStateWeekNo] = useState<boolean>(false);
  const [stateBetweenDates, setStateBetweenDates] = useState<boolean>(false);

  const setState = (oneDate:boolean, weekNo:boolean, betweenDates:boolean): void =>  {
    setStateOneDate(oneDate);
    setStateWeekNo(weekNo);
    setStateBetweenDates(betweenDates);
  }

  const changeState = (typeName: WeatherForcastEnumType): void => {
    switch (typeName) {
      case WeatherForcastEnumType.WeatherForcastSearchOneDate: setState(true, false, false); break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeWeekNo: setState(false, true, false); break;
      case WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates: setState(false, false, true); break;
    }
  }

  return (
    <div className='border border-info m-3 mt-5 pt-2 border-2'>
      <div>
        <h3>SelectSearchOptionState</h3>
        <WeatherForcastSearchOneDate onChange={changeState} radioButtonChecked={stateOneDate} />
        <WeatherForcastSearchTypeWeekNo onChange={changeState} radioButtonChecked={stateWeekNo} />
        <WeatherForcastSearchTypeBetweenTwoDates onChange={changeState} radioButtonChecked={stateBetweenDates} />
      </div>
      {props.children}
    </div>
  )
}



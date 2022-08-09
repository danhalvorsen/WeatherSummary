import { FC } from 'react';
import { Children } from "../../../compTypes";
import { SelectSearchOption } from "../../../../searchBox/SelectSearchOption";
import { propsType } from '../../../compTypes';
import { WeatherForcastSearchOneDate } from './WeatherForcastSearchTypes/WeatherForcastSearchOneDate';
import { WeatherForcastSearchTypeWeekNo } from './WeatherForcastSearchTypes/WeatherForcastSearchTypeWeekNo';
import { WeatherForcastSearchTypeBetweenTwoDates } from './WeatherForcastSearchTypes/WeatherForcastSearchTypeBetweenTwoDates';

type SelectSearchOptionStateProps = {
  children?: Children;
  radioButtonCheckedOneDate: boolean;
  radioButtonCheckedForWeekNo: boolean;
  radioButtonCheckedForBetweenDates: boolean;
};
export const SelectSearchOptionState: FC<propsType> = (props) => {
  return (
    <div className='border border-info m-3 mt-5 pt-2 border-2'>
      <div>
        <h3>SelectSearchOptionState</h3>
        <WeatherForcastSearchOneDate radioButtonChecked={true} />
        <WeatherForcastSearchTypeWeekNo radioButtonChecked={false} />
        <WeatherForcastSearchTypeBetweenTwoDates radioButtonChecked={false} />
      </div>
    </div>
  )
}



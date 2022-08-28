import { FromDate } from '../../../../../searchBox/FromDate';
import { RadioButton } from '../../../../../searchBox/RadioButton';
import { ToDate } from '../../../../../searchBox/ToDate';
import { Children } from '../../../../compTypes';
import { myDate } from '../../../../../../../communication/apiTypes';
import { WeatherForcastEnumType } from '../SelectSearchOptionState';

type WeatherForcastSearchTypeBetweenTwoDatesProps = {
  children?: Children;
  radioButtonChecked: boolean;
  onChange: (typeName: WeatherForcastEnumType) => void;
  choiceFromDate: (date: myDate) => void;
  choiceToDate: (date: myDate) => void;
};

export const WeatherForcastSearchTypeBetweenTwoDates: React.FC<
  WeatherForcastSearchTypeBetweenTwoDatesProps
> = (props): JSX.Element => {
  return (
    <>
      <div className="m-1">
        <span>Between Dates: </span>
        <RadioButton
          enabled={props.radioButtonChecked}
          onChange={props.onChange}
          motherName={
            WeatherForcastEnumType.WeatherForcastSearchTypeBetweenTwoDates
          }
        />
        <FromDate choiceFromDate={props.choiceFromDate} />{' '}
        <ToDate choiceToDate={props.choiceToDate} />
      </div>

      {props.children}
    </>
  );
};

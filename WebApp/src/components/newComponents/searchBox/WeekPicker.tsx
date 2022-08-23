import { isPropertySignature } from 'typescript';
import { weekNo } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/apiTypes';

type weekPickerProps = {
  ChoiceWeekNo: (weekNo: weekNo) => void;
  thisWeekNumber: weekNo
};
//            ChoiceWeekNo={changeChoiceWeekNo}

export const WeekPicker: React.FC<weekPickerProps> = (
  weekPickerProps,
): JSX.Element => {
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const valueText = event.target.value;
    const value: number = +valueText;
    const myLocalDate = { value };
    weekPickerProps.ChoiceWeekNo(myLocalDate);
  };

  return (
    <>
      <input
        type="number"
        placeholder="Week Number"
        onChange={handleChange}
        value={weekPickerProps.thisWeekNumber.value}
      />
    </>
  );
};

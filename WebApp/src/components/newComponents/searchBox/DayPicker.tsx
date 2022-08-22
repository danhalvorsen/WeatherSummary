import { myDate } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/apiTypes';

type props = {
  choiceDate: (date: myDate) => void;
};
export const DayPicker: React.FC<props> = (props): JSX.Element => {
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    const myLocalDate = { value };
    props.choiceDate(myLocalDate);
  };
  return (
    <>
      <input type="datetime-local" onChange={handleChange} />
    </>
  );
};

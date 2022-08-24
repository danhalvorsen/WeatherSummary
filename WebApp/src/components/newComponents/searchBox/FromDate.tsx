import { myDate } from '../../../communication/apiTypes';

type props = {
  choiceFromDate: (date: myDate) => void;
};
export const FromDate: React.FC<props> = (props): JSX.Element => {
  const handleChangeFrom = (event: React.ChangeEvent<HTMLInputElement>) => {
    const value = event.target.value;
    const myLocalDate = { value };
    props.choiceFromDate(myLocalDate);
    //props.choiceFromDate(event.target.value);
  };
  return (
    <>
      <input type="datetime-local" onChange={handleChangeFrom} />
    </>
  );
};

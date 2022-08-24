import { myDate } from "../../../communication/apiTypes";


type props = {
  choiceToDate: (date: myDate) => void;
};

export const ToDate: React.FC<props> = (props): JSX.Element => {
  const handleChangeTo = (event: React.ChangeEvent<myDate>) => {
    
    const value= event.target.value
    const myLocalDate = {value}
    props.choiceToDate(myLocalDate);
  };
  return (
    <>
      <input type="datetime-local" onChange={handleChangeTo} />
    </>
  );
};

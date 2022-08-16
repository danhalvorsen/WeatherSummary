type props = {
  choiceToDate: (date: string) => void;
};
export const ToDate: React.FC<props> = (props): JSX.Element => {
  const handleChangeTo = (event: React.ChangeEvent<HTMLInputElement>) => {
    props.choiceToDate(event.target.value);
  };
  return (
    <>
      <input type="datetime-local" onChange={handleChangeTo} />
    </>
  );
};

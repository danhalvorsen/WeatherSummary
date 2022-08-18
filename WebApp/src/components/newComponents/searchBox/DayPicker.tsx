type props = {
  choiceDate: (date: string) => void;
};
export const DayPicker: React.FC<props> = (props): JSX.Element => {
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    props.choiceDate(event.target.value);
  };
  return (
    <>
      <input type="datetime-local" onChange={handleChange} />
    </>
  );
};

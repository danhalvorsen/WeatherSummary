type props = {
    choiceFromDate:(date: string) => void
    }
export const FromDate: React.FC<props> = (props): JSX.Element => {
  const handleChangeFrom = ( event : React.ChangeEvent<HTMLInputElement>)=>{
    props.choiceFromDate(event.target.value);
  }
  return (
    <>
      <input type="datetime-local"  onChange={handleChangeFrom} />
    </>
  );
};

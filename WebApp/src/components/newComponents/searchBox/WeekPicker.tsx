import { Children } from "../compTypes";

type SelectSearchOptionProps = {
  children?: Children;
};


export const WeekPicker: React.FC = (props) => {

  return (
    <>

      <form action="/action_page.php">
  <label htmlFor="cars">Select a Week:</label>
    <select name="cars" id="cars">
    <option value="10">10</option>
    <option value="11">11</option>
    <option value="12">12</option>
    <option value="13">12</option>
    <option value="14">14</option>
  </select>

</form>      

      {props?.children}

    </>
  );
};


  

import { Children } from "../compTypes";

type SelectSearchOptionProps = {
  children?: Children;
};


export const DayPicker: React.FC = (props)=>{

    return (
        <>
    <div>
      <label>Pick a Day:</label> <input type="text" placeholder="Insert Date"/>
      {props?.children}
    </div>
     
        </>
    )
    }




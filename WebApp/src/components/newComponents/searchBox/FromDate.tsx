import { Children } from "../compTypes";

type SelectSearchOptionProps = {
  children?: Children;
};


export const FromDate: React.FC = (props) => {

  return (
    <>
<div></div>
<label>From:</label> <input type="text" placeholder="Insert Date"/>
      {props?.children}

    </>
  );
};

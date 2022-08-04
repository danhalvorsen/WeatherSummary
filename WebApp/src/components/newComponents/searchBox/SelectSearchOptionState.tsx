import { Children } from "../compTypes";



type SelectSearchOptionStateProps = {
  children?: Children
};

export const SelectSearchOptionState: React.FC<SelectSearchOptionStateProps>
  = (props: SelectSearchOptionStateProps) => {
    return (
      <div>
        <h3>SelectSearchOptionState</h3>
        {props?.children}
      </div>
    )
  }



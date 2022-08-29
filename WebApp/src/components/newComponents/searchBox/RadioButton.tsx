import { Children } from '../Form/compTypes';
import { WeatherForcastEnumType } from '../Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/SelectSearchOptionState';

type RadioButtonProps = {
  children?: Children;
  enabled: boolean;
  motherName: WeatherForcastEnumType;
  onChange: (typeName: WeatherForcastEnumType) => void;
};

export const RadioButton = (props: RadioButtonProps): JSX.Element => {
  const missingMotherNameConstant = '____Missing_Mother_Name____';

  const motherNameVerified =
    props.motherName !== undefined
      ? missingMotherNameConstant
      : props.motherName;
  const renderIt = (motherName: string): JSX.Element => {
    return (
      <>
        <input
          type="radio"
          checked={props?.enabled}
          onClick={(e) => props.onChange(props.motherName)}
          // eslint-disable-next-line @typescript-eslint/no-empty-function
          onChange={() => {}}
        />
      </>
    );
  };

  return motherNameVerified === missingMotherNameConstant
    ? renderIt(missingMotherNameConstant)
    : renderIt(motherNameVerified);
};

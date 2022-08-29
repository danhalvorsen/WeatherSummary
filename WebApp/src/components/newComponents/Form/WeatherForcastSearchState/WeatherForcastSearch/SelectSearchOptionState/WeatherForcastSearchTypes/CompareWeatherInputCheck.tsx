import { FromDate } from '../../../../../searchBox/FromDate';
import { RadioButton } from '../../../../../searchBox/RadioButton';
import { ToDate } from '../../../../../searchBox/ToDate';
import { Children } from '../../../../compTypes';
import { myDate } from '../../../../../../../communication/apiTypes';
import { WeatherForcastEnumType } from '../SelectSearchOptionState';

type CompareWeatherInputCheckProps = {
  children?: Children;
  changeCompareMode: () => void;
};

export const CompareWeatherInputCheck: React.FC<
  CompareWeatherInputCheckProps
> = (CompareWeatherInputCheckProps): JSX.Element => {
  const handleChange = () => {
    CompareWeatherInputCheckProps.changeCompareMode();
    return;
  };
  return (
    <>
      <div className="m-3 pt-2 ">
        <strong>Compare mode:</strong>{' '}
        <input type="checkbox" defaultChecked={false} onChange={handleChange} />
      </div>
    </>
  );
};

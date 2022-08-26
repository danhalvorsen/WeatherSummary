import { FromDate } from '../../../../../searchBox/FromDate';
import { RadioButton } from '../../../../../searchBox/RadioButton';
import { ToDate } from '../../../../../searchBox/ToDate';
import { Children } from '../../../../compTypes';
import { myDate } from '../../../../../../../communication/apiTypes';
import { WeatherForcastEnumType } from '../SelectSearchOptionState';

type CompareWeatherInputCheckProps = {
  children?: Children;
  changeCompareMode:() => void
};

export const CompareWeatherInputCheck: React.FC<CompareWeatherInputCheckProps> = (CompareWeatherInputCheckProps): JSX.Element => {
  const handleChange = ()=>{
    CompareWeatherInputCheckProps.changeCompareMode()
    return
  }
  return (
    <>
      <div className="border border-dark m-3 mt-5 pt-2 border-2">
        Compare Providers mode: {' '}
        <input type="checkbox" defaultChecked={false} onChange={handleChange} />
      </div>
    </>
  );
};

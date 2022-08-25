import { List } from './List';
import { WeatherForecastDto } from '../../../../../WebApp/src/communication/api.client.generated';

type props = {
  oneDay: WeatherForecastDto[];
};

export const ListState: React.FC<props> = (props) => {
  return (
    <>
      <div className="border border-danger mt-5 m-3">
        <h2>List State</h2>
        <h2>
          There are <strong>{props.oneDay.length} </strong> providerts to show
          their Data
        </h2>

        <List />
      </div>
    </>
  );
};

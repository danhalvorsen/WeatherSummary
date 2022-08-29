import { WeatherForecastDto } from '../../../communication/api.client.generated';
import { ListItemProvider } from './ListItemProvider';

type props = {
  array: WeatherForecastDto[];
  children?: JSX.Element | JSX.Element[];
};

export const ListCompare: React.FC<props> = (props) => {
  const style = `mx-3 mt-3 py-1 bg-info`;

  let thisDay: string | undefined;
  const compareTowDays = props.array.map((item) => {
    thisDay = item.date?.toDateString();
    const unicKey = Math.random().toString() + thisDay;

    return <ListItemProvider key={unicKey} eachDay={item} />;
  });

  return (
    <>
      <div className={style}>
        <h3>
          <strong>{thisDay}</strong>
        </h3>
        <div>{compareTowDays}</div>
      </div>
    </>
  );
};

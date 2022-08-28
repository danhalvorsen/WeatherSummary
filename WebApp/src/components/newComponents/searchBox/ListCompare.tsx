import { useState } from 'react';
import { WeatherForecastDto } from '../../../communication/api.client.generated';
import { List } from './List';
import { ListItem } from './ListItem';
import { ListItemProvider } from './ListItemProvider';

type props = {
  array: WeatherForecastDto[];
  children?: JSX.Element | JSX.Element[];
};

export const ListCompare: React.FC<props> = (props) => {
  const style = `border border-info m-3 mt-5 pt-2 border-2 bg-info`;

  const [color, setColor] = useState<boolean>(false);

  let thisDay: string | undefined;
  const compareTowDays = props.array.map((item) => {
    thisDay = item.date?.toDateString();
    const unicKey = Math.random().toString() + thisDay
    

    return <ListItemProvider key={unicKey} eachDay={item} />;
    setColor(true);
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

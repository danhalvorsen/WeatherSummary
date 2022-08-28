import { useState } from 'react';
import { WeatherForecastDto } from '../../../communication/api.client.generated';
import { List } from './List';
import { ListItem } from './ListItem';
type props = {
  array: WeatherForecastDto[];
};

export const ListCompare: React.FC<props> = (props) => {
  const style = `border border-info m-3 mt-5 pt-2 border-2 bg-primary`;

  const [color, setColor] = useState<boolean>(false);

  let thisDay: string | undefined;
  const compareTowDays = props.array.map((item) => {
    thisDay = item.date?.toDateString();

    return <ListItem eachDay={item} />;
    setColor(true);
  });

  return (
    <>
      <div className={style}>
        <h3>
          List Compare <strong>{thisDay}</strong>
        </h3>
       <div className=''>{compareTowDays}</div>
      </div>
    </>
  );
};

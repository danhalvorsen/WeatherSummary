import { WeatherForecastDto } from '../../../communication/api.client.generated';
import { ListItem } from './ListItem';
type props = {
  row: WeatherForecastDto[];
  background: string;
};

export const List: React.FC<props> = (props) => {
  const style = `mx-3 m-2 mt-1 pb-1 border-1 bg-${props.background}`;

  let ProviderName: string | undefined = '';

  const renderList = props.row.map((item) => {
    ProviderName = item.source?.dataProvider;
    return <ListItem key={item.date?.toDateString()} eachDay={item} />;
  });

  return (
    <>
      <div className={style}>
        <h2>{ProviderName}</h2>
        <h2></h2>

        {renderList}
      </div>
    </>
  );
};

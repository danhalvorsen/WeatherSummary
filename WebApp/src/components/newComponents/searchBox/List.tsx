import { WeatherForecastDto } from '../../../communication/api.client.generated';
import { ListItem } from './ListItem';
type props = {
  row: WeatherForecastDto[];
  background: string;
};

export const List: React.FC<props> = (props) => {
  const style = `border border-info m-3 mt-5 pt-2 border-2 bg-${props.background}`;

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

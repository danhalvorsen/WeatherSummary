import { WeatherForecastDto } from '../../../communication/api.client.generated';

type props = {
  eachDay: WeatherForecastDto;
  children?: JSX.Element | JSX.Element[];
};

export const ListItemProvider: React.FC<props> = (props) => {
  return (
    <>
      <div className="border border-secondary m-1 bg-light mx-5">
        <h5>{props.eachDay.source?.dataProvider}</h5>
        <p>
          Temperture:<strong> {props.eachDay.temperature}</strong> - Humidity :
          <strong> {props.eachDay.humidity}</strong> - Cloud fraction :
          <strong> {props.eachDay.cloudAreaFraction}</strong> - Pressure :
          <strong> {props.eachDay.pressure}</strong> - Rain probability :
          <strong> {props.eachDay.pressure}</strong> - Wind Speed :
          <strong> {props.eachDay.windspeedGust}</strong>
        </p>
      </div>
    </>
  );
};

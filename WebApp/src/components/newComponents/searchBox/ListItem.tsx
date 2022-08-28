import { WeatherForecastDto } from '../../../communication/api.client.generated';

type props = {
  eachDay: WeatherForecastDto;
  };

export const ListItem: React.FC<props> = (props) => {
  return (
    <>
      <div className="border border-secondary m-1 bg-light mx-5">
        <h5>{props.eachDay.date?.toDateString()}</h5>
        <p>
          Temperture:<strong> {props.eachDay.temperature}</strong> - Humidity :
          <strong> {props.eachDay.humidity}</strong>- Amount rain :
          <strong> {props.eachDay.amountRain}</strong> - Cloud fraction :
          <strong> {props.eachDay.cloudAreaFraction}</strong>- Pressure :
          <strong> {props.eachDay.pressure}</strong> - Rain probability :
          <strong> {props.eachDay.probOfRain}</strong>
        </p>
      </div>
    </>
  );
};

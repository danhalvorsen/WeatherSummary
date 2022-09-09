namespace WeatherWebAPI.Factory
{
    public interface IFactory<T,StrategyEnumType> where StrategyEnumType : struct, IConvertible
    {
        T Build(StrategyEnumType Type);
    }
}
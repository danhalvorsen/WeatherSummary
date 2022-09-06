using AutoMapper;
using WeatherWebAPI.Contracts.BaseContract;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory.Strategy.YR;

namespace WeatherWebAPI.Factory
{
    public interface IFactory<T,StrategyEnumType> where StrategyEnumType : struct, IConvertible
    {
        T Build(StrategyEnumType Type);
    }
}
import { useState } from 'react';
import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import StatusCodeNotOkError from './apiErrors';
import {
  WeekNumber,
  WeekNoValidator,
  MyDateValidator,
  myDate,
  BetweenTwoDatesValidator,
  theObjectIsEmpty as isValid,
  twoDates,
} from './apiTypes';

export const api = (baseUrl: string) => {
  const queryCityByDate = (oneDate: myDate, cityName: string): string =>
    `${baseUrl}weatherforecast/date?DateQuery.Date=${oneDate.value}&CityQuery.City=${cityName}`;
  const queryCityByWeekNo = (weekNo: WeekNumber, cityName: string): string =>
    `${baseUrl}weatherforecast/week?Week=${weekNo.value}&CityQuery.City=${cityName}`;
  const queryCityBetweenToDates = (dates: twoDates, city: string): string =>
    `${baseUrl}weatherforecast/between?BetweenDateQuery.From=${dates.from.value}&BetweenDateQuery.To=${dates.to.value}&CityQuery.City=${city}`;

  const makeSingleDateApiRequest = async (
    city: string,
    date: myDate,
  ): Promise<any> => {
    const url = queryCityByDate(date, city);
    const dateValidator = new MyDateValidator();
    const result = dateValidator.validate(date);

    if (isValid(result)) {
      const res = await fetch(url);
      if (res.ok) {
        return res.json();
      } else {
        throw new StatusCodeNotOkError(res);
      }
    }
  };
  const makeWeekNumberApiRequest = async (
    cityName: string,
    weekNo: WeekNumber,
  ): Promise<any> => {
    const weekNoValidator = new WeekNoValidator();
    const result = weekNoValidator.validate(weekNo);
    // if (result !== {}) {
    if (isValid(result)) {
      const url = queryCityByWeekNo(weekNo, cityName);
      const res = await fetch(url);
      if (res.ok) {
        return await res.json();
      }
      throw new StatusCodeNotOkError(res);
    }
  };

  const makeBetweenDatesApiRequest = async (
    cityName: string,
    dates: twoDates,
  ): Promise<any> => {
    const betweenDateValidator = new BetweenTwoDatesValidator();
    const validationResult = betweenDateValidator.validate(dates);
    if (isValid(validationResult)) {
      const url = queryCityBetweenToDates(dates, cityName);
      const res = await fetch(url);
      if (res.ok) {
        return await res.json();
      }
      throw new StatusCodeNotOkError(res);
    }
  };
  return {
    makeSingleDateApiRequest,
    makeWeekNumberApiRequest,
    makeBetweenDatesApiRequest,
  };
};

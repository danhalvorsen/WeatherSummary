import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import {
    weekNo,
    WeekNoValidator,
} from '../components/newComponents/Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/apiTypes';

const validator = new WeekNoValidator();

test('the weeknumber validator should not accept week number above 52', () => {
    const sut: weekNo = { value: 10 };
    const result: ValidationErrors<weekNo> = validator.validate(sut);
    expect(result).not.toBeNull();
    console.log(result);
});

test('the weeknumber validator should not accept negative week number below 0', () => {
    const currentWeekNo = -100;
    const sut = { value: currentWeekNo };
    const result: ValidationErrors<weekNo> = validator.validate(sut);
    expect(result).not.toBeNull();
    console.log(result);
});

test('the week number validator should accept any number between 1..52', () => {
    const sut: weekNo = { value: 1 };
    const result: ValidationErrors<weekNo> = validator.validate(sut);
    expect(result).toEqual({});
    console.log(result);
});

test('Check if tripple equals a a good way if checking validation result', () => {
    const sut: weekNo = { value: 10 };
    const result: ValidationErrors<weekNo> = validator.validate(sut);
    expect(result).toMatchObject({});
});

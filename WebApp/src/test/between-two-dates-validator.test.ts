import { ResultBox } from './../components/newComponents/resultBox/ResultBox';
import { twoDates, BetweenTwoDatesValidator, theObjectIsEmpty } from './../components/newComponents/Form/WeatherForcastSearchState/WeatherForcastSearch/SelectSearchOptionState/apiTypes';

const validator = new BetweenTwoDatesValidator();


test('validator should return True if from date is less than to date', () => {
    const sut: twoDates = {
        from: {
            value: '2022-08-22T08:26:30.840Z'

        },
        to: {
            value: '2022-08-22T08:26:30.840Z'
        }
    };

    const result = validator.validate(sut);
    expect(theObjectIsEmpty(result)).toEqual(true);
});


test('Validator should fail when from is greater than to', () => {
    //subject under test
    const sut: twoDates = {
        from: {
            value: '2022-08-23'
        },
        to: {
            value: '2022-08-22'
        }
    };

    const result = validator.validate(sut);
    expect(theObjectIsEmpty(result)).toEqual(false);
}
);
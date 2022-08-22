import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import StatusCodeNotOkError from './apiErrors';
import { weekNo, WeekNoValidator, MyDateValidator, myDate, theObjectIsEmpty as isObjectEmpty } from './apiTypes';



export const api = (baseUrl: string) => {
    const queryCityByDate = (oneDate: myDate, cityName: string): string =>
        `${baseUrl}weatherforecast/date?DateQuery.Date=${oneDate}&CityQuery.City=${cityName}`;
    const queryCityByWeekNo = (weekNo: weekNo, cityName: string): string =>
        `${baseUrl}weatherforecast/week?Week=${weekNo}&CityQuery.City=${cityName}`;
    const queryCityBetweenToDates = (
        from: myDate,
        to: myDate,
        city: string,
    ): string =>
        `${baseUrl}weatherforecast/between?BetweenDateQuery.From=${from.value}&BetweenDateQuery.To=${to.value}&CityQuery.City=${city}`;


    const makeSingleDateApiRequest = async (
        city: string,
        date: myDate,
    ): Promise<any> => {
        const url = queryCityByDate(date, city);
        const dateValidator = new MyDateValidator();
        const result = dateValidator.validate(date);
        const emptyObject = {}

        //if (result == {}) {
        if (isObjectEmpty(result)) {
            console.log("We've got an Error from single date request: ");
        }
        else {
            console.log("going to fetch date....")
            const res = await fetch(url);
            if (res.ok) {
                return res.json();
            } else {
                console.log("Error: 404")
                throw new StatusCodeNotOkError(res);
            }
        }

    };
    const makeWeekNumberApiRequest = (cityName: string, weekNo: weekNo) => {
        weekNo;
        console.log(
            '******************************************' +
            weekNo +
            +'value:' +
            weekNo.value,
        );
        const weekNoValidator = new WeekNoValidator();
        const result: ValidationErrors<weekNo> = weekNoValidator.validate(weekNo);
        console.log('validation of weekNo' + result);
        if (result !== {}) {
            //We have an error in weekNo type
            console.log('The object is not valid');
            console.log(result.value);
            return [];
        }

        fetch(queryCityByWeekNo(weekNo, cityName)).then((response) =>
            response.json(),
        );
    };

    const makeBetweenDatesApiRequest = async (
        cityName: string,
        fromDate: myDate,
        toDate: myDate,
    ): Promise<any> => {
        const url = queryCityBetweenToDates(fromDate, toDate, cityName);
        const dateValidator = new MyDateValidator();
        const result = dateValidator.validate(fromDate);
        if (result !== {}) {
            console.log(result);
            console.log("We've got an error!");
        }
        else
            fetch(url)
                .then((response) =>
                    response.json()
                );



    };
    return {
        makeSingleDateApiRequest,
        makeWeekNumberApiRequest,
        makeBetweenDatesApiRequest,
    };
};

import { Validator } from 'fluentvalidation-ts';

export type weekNo = {
    value: number;
};

export class WeekNoValidator extends Validator<weekNo> {
    constructor() {
        super();
        this.ruleFor('value')
            .greaterThan(0)
            .withMessage('week number should be greater or equal to 1');
        this.ruleFor('value')
            .lessThanOrEqualTo(52)
            .withMessage('Week number should be 1 .. 52');
    }
}

export type myDate = {
    value: string;
};

export class MyDateValidator extends Validator<myDate> {
    constructor() {
        super();
        this.ruleFor('value')
            .notEmpty()
        this.ruleFor('value')
            .minLength(8)
            .withMessage('the Date you entered is too short.');
        this.ruleFor('value')
            .maxLength(15)
            .withMessage('the Date you entered is too long.');
        this.ruleFor('value')
            .must(this.isValid)
            .withMessage(
                'Error parsing raw date to a Date object. Please check your in-data',
            );
    }

    isValid = (value: string): boolean => {
        const date = new Date(value);
        return !isNaN(date.getTime()) ? true : false;
    };
}

import { Validator } from 'fluentvalidation-ts';

export type weekNo = {
    value: number;
};

export class WeekNoValidator extends Validator<weekNo> {
    constructor() {
        super();
        this.ruleFor('value')
            .greaterThan(0)
            .withMessage('week number should be greater or equal to 1')
        this.ruleFor('value')
            .lessThanOrEqualTo(52)
            .withMessage('Week number should be 1 .. 52');

    }
}

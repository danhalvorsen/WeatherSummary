import { Validator } from 'fluentvalidation-ts';
import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import { ProductQuery } from '../Data/CommunicationService';

export const isValid = <T>(object: ValidationErrors<T>): boolean => {
  return Object.keys(object).length === 0 ? true : false;
};

export class UrlValidator extends Validator<ProductQuery> {
  constructor() {
    super();
    this.ruleFor('parameter').notEmpty()
    .withMessage('There was an error on URL.');
  }
}

export class IdValidator extends Validator<ProductQuery> {
  constructor() {
    super();
    this.ruleFor('id')
      .greaterThanOrEqualTo(1)
      .withMessage('There was an error finding the page.');
  }
}

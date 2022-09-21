import { ProductType } from './../src/components/Form/productType';
import { Validator } from 'fluentvalidation-ts';
import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import { ProductQuery } from '../Data/CommunicationService';

export const isValid = <T>(object: ValidationErrors<T>): boolean => {
  return Object.keys(object).length === 0 ? true : false;
};

export class UrlValidator extends Validator<ProductQuery> {
  static validate(sampleUrl: ProductQuery) {
      throw new Error("Method not implemented.");
  }
  constructor() {
    super();
    this.ruleFor('parameter')
      .notEmpty()
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

export class ProductValidator extends Validator<ProductType> {
  constructor() {
    super();
    this.ruleFor('price')
      .greaterThan(0)
      .withMessage('The price should be greater than 0');

    this.ruleFor('id')
      .greaterThanOrEqualTo(1)
      .withMessage('product id should greater than 1');

    this.ruleFor('description')
      .minLength(5)
      .withMessage('description should have at least 5 character');

    this.ruleFor('brand')
      .notEmpty()
      .withMessage('brand field should not be empty');

    this.ruleFor('title')
      .notEmpty()
      .withMessage('title field should not be empty');
    this.ruleFor('category')
      .notEmpty()
      .withMessage('category field should not be empty');
    this.ruleFor('imageurl')
      .notEmpty()
      .withMessage('image url field should not be empty');
  }
}

import { ProductQuery } from "../Data/CommunicationService";
import { queryStringValidator } from "../Validator-OrderingSystem/Validator";

const myQueryStringValidator = new queryStringValidator();

test('validate that the validator pass the parameter', () => {
    const sut: ProductQuery = {
      baseUrl: 'http://localhost',
      parameter: '/products',
    };
    const result = myQueryStringValidator.validate(sut);
    expect(result).toStrictEqual({});
  });
  
  test('validate that the validator does not pass the empty parameter', () => {
    const sut: ProductQuery = {
      baseUrl: 'http://localhost',
      parameter: '',
    };
    const result = myQueryStringValidator.validate(sut);
    expect(result).not.toStrictEqual({});
  });
  
  test('Id should not be equal or less than 0', () => {
    const sut: ProductQuery = {
      baseUrl: 'http://localhost',
      parameter: '/products',
      id: 0,
    };
    expect(sut.id).not.toBeGreaterThanOrEqual(1);
  });
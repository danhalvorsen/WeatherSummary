import { ProductQuery } from "../Data/CommunicationService";
import { UrlValidator } from "../Validator-OrderingSystem/Validator";


const sut = new UrlValidator()
test('validate that the validator pass the parameter', () => {
    const sampleUrl: ProductQuery = {
      baseUrl: 'http://localhost',
      parameter: '/products',
    };
    const result = sut.validate(sampleUrl);
    expect(result).toStrictEqual({});
  });

  test('validate that the validator does not pass the empty parameter', () => {
    const sampleUrl: ProductQuery = {
      baseUrl: 'http://localhost',
      parameter: '',
    };
    const result = sut.validate(sampleUrl);
    expect(result).not.toStrictEqual({});
  });

  test('Id types we got is valid', () => {
    const productQuery: ProductQuery = {
      baseUrl: 'http://localhost',
      parameter: '/products',
      id: 5,
    };
    const result = sut.validate(productQuery);
    expect(result).toStrictEqual({});
  });
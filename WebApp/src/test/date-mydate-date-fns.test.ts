import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import { myDate, MyDateValidator } from '../communication/apiTypes';

const validator = new MyDateValidator();

test('validate that the validator fails with error messsage', () => {
  const sut: myDate = { value: 'THIS IS NOT A DATE' };

  const result = validator.validate(sut);
  expect(result).not.toBeNull();
  console.log(result);
});

test('validator should accept a valid raw string with proper data', () => {
  const sut: myDate = { value: '02/02/2022' };

  const result = validator.validate(sut);
  expect(result).toStrictEqual({});
  console.log(result);
});

test('validator should not fail when input data contains garbage at the end of raw string (value)', () => {
  const sut: myDate = { value: '02.02.2022\n\n' };

  const result = validator.validate(sut);
  expect(result).toStrictEqual({});
  console.log(result);
});

test('validator should not fail when input data contains garbage at the end of raw string (value)', () => {
  const sut: myDate = { value: '2.2.2022\n\n' };

  const result = validator.validate(sut);
  expect(result).toStrictEqual({});
  console.log(result);
});

test('validator should not fail when input data contains garbage at the end of raw string (value)', () => {
  const sut: myDate = { value: '12.12.2022\n\n\n' };

  const result = validator.validate(sut);
  expect(result).toStrictEqual({});
  console.log(result);
});

test('validator should not fail when input data contains garbage at the end of raw string (value)', () => {
  const sut: myDate = { value: '32.9.2022\n\n' };

  const result = validator.validate(sut);
  expect(result).not.toStrictEqual({});
  console.log(result);
});

test('validator should not fail when input data is equal to empty object {}', () => {
  const sut: myDate = { value: '2022-08-22T08:26:30.840Z' };
  const emptyObject = {};
  const result = validator.validate(sut);
  expect(result).toEqual(emptyObject);
});

test('validator should pass for Today date(default date)', () => {
  const sut: myDate = { value: '2022-08-22T08:26:30.840Z' };

  const result = validator.validate(sut);
  expect(result).toStrictEqual({});
  console.log(result);
});

test('validator should return True if isValid works correctly', () => {
  const sut: myDate = { value: '2022-08-22T08:26:30.840Z' };

  const result = validator.isValid(sut.value);
  expect(result).toBe(true);
});

import { render, screen } from '@testing-library/react';
import { BrowserRouter } from 'react-router-dom';
import { ProductQuery } from '../Data/CommunicationService';
import { CartButton } from '../src/components/cart/CartButton';
import { queryStringValidator } from '../Validator/Validator';

const myQueryStringValidator = new queryStringValidator();

test('show cart in page', () => {
  render(
    <BrowserRouter>
      <CartButton />
    </BrowserRouter>,
  );

  const spanElement = screen.getByRole('Cart');
  expect(spanElement).toBeInTheDocument();
});

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

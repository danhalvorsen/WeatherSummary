import { render, screen } from '@testing-library/react';
import { basename } from 'path';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ProductQuery } from '../Data/CommunicationService';
import NavigationBar from '../src/components/Form/NavigationBar';
import ProductsState from '../src/components/Form/ProductsState/ProductsState';
import { UrlValidator } from '../Validator-OrderingSystem/Validator';

const validator = new UrlValidator();

test('Load Navigation bar in front page', () => {
  render(
    <BrowserRouter>
      <NavigationBar />
    </BrowserRouter>,
  );
  const homeLink = screen.getByText(/Home/i);

  expect(homeLink).toBeInTheDocument();
});

test('validate that the validator pass the parameter', () => {
  const sampleUrl: ProductQuery = {
    baseUrl: 'http://localhost',
    parameter: '/products',
  };
  const result = validator.validate(sampleUrl);
  expect(result).toStrictEqual({});
});

test('validate that the validator does not pass the empty parameter', () => {
  const sampleUrl: ProductQuery = {
    baseUrl: 'http://localhost',
    parameter: '',
  };
  const result = validator.validate(sampleUrl);
  expect(result).not.toStrictEqual({});
});

test('Id types we got is valid', () => {
  const sampleUrl: ProductQuery = {
    baseUrl: 'http://localhost',
    parameter: '/products',
    id: 5,
  };
  expect(typeof sampleUrl.id).toBe('number');
});

test('Id should not be equal or less than 1', () => {
  const sampleUrl: ProductQuery = {
    baseUrl: 'http://localhost',
    parameter: '/products',
    id: 0,
  };
  expect(sampleUrl.id).not.toBeGreaterThanOrEqual(1);
});


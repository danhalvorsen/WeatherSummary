import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { basename } from 'path';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { getAllProducts, ProductQuery } from '../Data/CommunicationService';
import NavigationBar from '../src/components/Form/NavigationBar';
import Detail from '../src/components/Form/ProductsState/Products/Detail';
import ProductItem from '../src/components/Form/ProductsState/Products/ProductItem';
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

test('show more link should be a link to another page', () => {
  render(
    <ProductItem
      data={{
        id: 0,
        title: '',
        description: '',
        customerprice: 0,
        boughtprice: 0,
        stock: 0,
        brand: '',
        category: '',
        imageurl: '',
        coupon: 0,
        price: 0,
      }}
    />,
    { wrapper: BrowserRouter },
  );
  const element = screen.getByTestId('custom-element');
});

test('Click on Show More should be goes to Detail page', () => {
  const user = userEvent.click;
  render(
    <ProductItem
      data={{
        id: 0,
        title: '',
        description: '',
        customerprice: 0,
        boughtprice: 0,
        stock: 0,
        brand: '',
        category: '',
        imageurl: '',
        coupon: 0,
        price: 0,
      }}
    />,
    { wrapper: BrowserRouter },
  );
  expect(screen.getByText(/Show More/i)).toBeInTheDocument();

  user(screen.getByText(/Show More/i));

  expect(screen.getByText(/Price/i)).toBeInTheDocument();
});

// test('DataTypes should be valid', () => {

// //render()

// const baseUrl = 'http://localhost:3002';
// const productsQuery = new ProductQuery();
// productsQuery.baseUrl = baseUrl;
// productsQuery.parameter = '/products';

// const products = getAllProducts(productsQuery);
// products.then((products)=>{
//   const mytype= typeof(products)
//   expect()
//   })

// });

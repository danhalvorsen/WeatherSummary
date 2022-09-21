import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { basename } from 'path';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { getAllProducts, ProductQuery } from '../Data/CommunicationService';
import NavigationBar from '../src/components/Form/NavigationBar';
import Detail from '../src/components/Form/ProductsState/Products/Detail';
import ProductItem from '../src/components/Form/ProductsState/Products/ProductItem';
import ProductsState from '../src/components/Form/ProductsState/ProductsState';
import { ValidationErrors } from 'fluentvalidation-ts/dist/ValidationErrors';
import { ProductType } from '../src/components/Form/productType';
import {
  UrlValidator,
  ProductValidator,
  isValid,
} from '../Validator-OrderingSystem/Validator';

const urlValidator = new UrlValidator();

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
  const result = urlValidator.validate(sampleUrl);
  expect(result).toStrictEqual({});
});

test('validate that the validator does not pass the empty parameter', () => {
  const sampleUrl: ProductQuery = {
    baseUrl: 'http://localhost',
    parameter: '',
  };
  const result = urlValidator.validate(sampleUrl);
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

test('ProductValidator should accept received data ', () => {
  const product: ProductType = {
    id: 1,
    title: 'Microsoft Surface Laptop 4',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen. Do it all with a perfect balance of sleek design, speed, immersive audio, and significantly longer battery life than before.',
    price: 1900,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageurl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(product);
  expect(result).toStrictEqual({});
});

test('ProductValidator should not pass with empty title ', () => {
  const product: ProductType = {
    id: 1,
    title: '',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen. Do it all with a perfect balance of sleek design, speed, immersive audio, and significantly longer battery life than before.',
    price: 1900,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageurl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(product);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with short description ', () => {
  const product: ProductType = {
    id: 1,
    title: 'Apple watch',
    description: 'Nice',
    price: 1900,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageurl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(product);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with price of 0', () => {
  const product: ProductType = {
    id: 1,
    title: 'Apple watch',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics.',
    price: 0,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageurl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(product);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with empty Image', () => {
  const product: ProductType = {
    id: 1,
    title: 'Apple watch',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics.',
    price: 6500,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageurl: '',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(product);
  expect(result).not.toStrictEqual({});
});

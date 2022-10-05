import { ProductType } from '../src/components/Form/productType';
import { ProductValidator } from '../Validator/Validator';

test('ProductValidator should not pass with price of 0', () => {
  const sut: ProductType = {
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
    imageUrl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should accept id that be greater than 0 ', () => {
  const sut: ProductType = {
    id: 0,
    title: 'Microsoft Surface Laptop 4',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen. Do it all with a perfect balance of sleek design, speed, immersive audio, and significantly longer battery life than before.',
    price: 1900,
    customerprice: 14900,
    boughtprice: 10900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageUrl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with short description ', () => {
  const sut: ProductType = {
    id: 1,
    title: 'Apple watch',
    description: 'Nice',
    price: 1900,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageUrl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with empty brand ', () => {
  const sut: ProductType = {
    id: 1,
    title: 'Microsoft surface',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen. Do it all with a perfect balance of sleek design, speed, immersive audio, and significantly longer battery life than before.',
    price: 1900,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: '',
    category: 'Laptops',
    imageUrl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with empty title ', () => {
  const sut: ProductType = {
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
    imageUrl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with empty category', () => {
  const sut: ProductType = {
    id: 1,
    title: 'Apple watch',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics.',
    price: 6500,
    customerprice: 14900,
    boughtprice: 14900,
    stock: 18,
    brand: 'Microsoft',
    category: '',
    imageUrl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should not pass with empty Image', () => {
  const sut: ProductType = {
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
    imageUrl: '',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).not.toStrictEqual({});
});

test('ProductValidator should accept received data ', () => {
  const sut: ProductType = {
    id: 1,
    title: 'Microsoft Surface Laptop 4',
    description:
      'Style and speed. Stand out on HD video calls backed by Studio Mics. Capture ideas on the vibrant touchscreen. Do it all with a perfect balance of sleek design, speed, immersive audio, and significantly longer battery life than before.',
    price: 1900,
    customerprice: 14900,
    boughtprice: 10900,
    stock: 18,
    brand: 'Microsoft',
    category: 'Laptops',
    imageUrl:
      'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
    coupon: 50,
  };
  const productValidator = new ProductValidator();
  const result = productValidator.validate(sut);
  expect(result).toStrictEqual({});
});

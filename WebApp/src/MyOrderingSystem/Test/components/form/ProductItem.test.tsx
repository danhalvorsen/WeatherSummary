import React from 'react';
import { render, screen } from '@testing-library/react';
import ProductItem from '../../../src/components/Form/ProductsState/Products/ProductItem';
import { ProductType } from '../../../src/components/Form/productType';
import { BrowserRouter } from 'react-router-dom';
import userEvent from '@testing-library/user-event'



test('"Show More" in this component should be a link ', () => {
  const sut: ProductType = {
    id: 1,
    title: 'Microsoft Surface Laptop 4',
    description: 'Style and speed. Stand out on ',
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
  const sutKey = 10;

  render(
    <BrowserRouter>
      <ProductItem key={sutKey} data={sut} />
    </BrowserRouter>,
  );
  const spanElement = screen.getByRole('custom-element');
  expect(spanElement).toBeInTheDocument();
});

test('"Show More" in this component should be a link ', () => {
  const sut: ProductType = {
    id: 1,
    title: 'Microsoft Surface Laptop 4',
    description: 'Style and speed. Stand out on ',
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
  const sutKey = 10;

  render(
    <BrowserRouter>
      <ProductItem key={sutKey} data={sut} />
    </BrowserRouter>,
  );
  const spanElement = screen.getByRole('custom-element');
  expect(spanElement).toBeInTheDocument();
});

function setup(jsx: any) {
  return {
    user: window.navigator.clipboard,
    ...render(jsx),
  };
}

// test('click on Show More should goes to a proper page', async () => {
//     const user = userEvent.setup()

//   const sut: ProductType = {
//     id: 1,
//     title: 'Microsoft Surface Laptop 4',
//     description: 'Style and speed. Stand out on ',
//     price: 1900,
//     customerprice: 14900,
//     boughtprice: 14900,
//     stock: 18,
//     brand: 'Microsoft',
//     category: 'Laptops',
//     imageurl:
//       'https://fdn.gsmarena.com/imgroot/news/21/09/surface-laptops/-1200/gsmarena_001.jpg',
//     coupon: 50,
//   };
//   const sutKey = 10;

//   render(
//     <BrowserRouter>
//       <ProductItem key={sutKey} data={sut} />
//     </BrowserRouter>,
//   );

//   // verify page content for default route
//   expect(screen.getByText(/Show More/i)).toBeInTheDocument();

//   // verify page content for expected route after navigating
//   await user.click(screen.getByText(/Show More/i));
//   expect(screen.getByText(/you have/i)).toBeInTheDocument();
// });

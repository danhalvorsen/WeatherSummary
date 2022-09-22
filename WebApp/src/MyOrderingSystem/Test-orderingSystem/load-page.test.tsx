import { render, screen } from '@testing-library/react';
import { BrowserRouter} from 'react-router-dom';
import NavigationBar from '../src/components/Form/NavigationBar';

test('Load Navigation bar in front page', () => {
  render(
    <BrowserRouter>
      <NavigationBar />
    </BrowserRouter>,
  );
  const homeLink = screen.getByText(/Home/i);
  expect(homeLink).toBeInTheDocument();
});

// test('show more link should be a link to another page', () => {
//   render(
//     <ProductItem
//       data={{
//         id: 0,
//         title: '',
//         description: '',
//         customerprice: 0,
//         boughtprice: 0,
//         stock: 0,
//         brand: '',
//         category: '',
//         imageurl: '',
//         coupon: 0,
//         price: 0,
//       }}
//     />,
//     { wrapper: BrowserRouter },
//   );
//   const element = screen.getByTestId('custom-element');
// });

// test('Click on Show More should be goes to Detail page', () => {
//   const user = userEvent.click;
//   render(
//     <ProductItem
//       data={{
//         id: 0,
//         title: '',
//         description: '',
//         customerprice: 0,
//         boughtprice: 0,
//         stock: 0,
//         brand: '',
//         category: '',
//         imageurl: '',
//         coupon: 0,
//         price: 0,
//       }}
//     />,
//     { wrapper: BrowserRouter },
//   );
//   expect(screen.getByText(/Show More/i)).toBeInTheDocument();
// });

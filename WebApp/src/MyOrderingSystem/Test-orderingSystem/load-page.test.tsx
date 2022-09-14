import { render, screen } from '@testing-library/react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import NavigationBar from '../src/components/Form/NavigationBar';
import ProductsState from '../src/components/Form/ProductsState/ProductsState';

test('Load Navigation bar in front page', () => {
  render(
    <BrowserRouter>
      <NavigationBar />
    </BrowserRouter>,
  );
  const homeLink = screen.getByText(/Home/i);

  expect(homeLink).toBeInTheDocument();
});

// test('Should load data', () => {
//   render(
//     <BrowserRouter>
//       <Routes>
//         <Route path="/" element={<ProductsState />} />
//       </Routes>
//     </BrowserRouter>,
//   );
//   const loadData = screen.getByText(/Home/i);

//   expect(homeLink).toBeInTheDocument();
// });

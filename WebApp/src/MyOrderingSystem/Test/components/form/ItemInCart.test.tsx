import { render } from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import {
  CartItem,
  ShoppingCartContext,
} from '../../../src/components/context/ShoppingCartContext';
import {
  ItemInCart,
  ItemInCartProps,
} from '../../../src/components/shoppingCart/ItemInCart';

test('check props value', () => {
  const sut: CartItem = {
    id: 5,
    quantity: 2,
  };
  const sutId = 1;
  // render( <ItemInCart key={sutId} item={sut} /> )
  // const anchorElement = screen
});

test('something new', () => {});

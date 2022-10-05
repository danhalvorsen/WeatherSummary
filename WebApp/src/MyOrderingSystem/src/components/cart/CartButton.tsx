import { Link } from 'react-router-dom';
import { useShoppingCart } from '../context/ShoppingCartContext';

export const CartButton = () => {
  const { cartItemsQuantity, loginRole } = useShoppingCart();

  const styles = {
    borderRound: {
      color: 'white',
      backgroundColor: 'red',
      padding: '5px',
      border: '1px solid black',
      borderRadius: '20px',
    },
  };

  const basketNumber = cartItemsQuantity || 0;

  const user = () => {
    if (loginRole !== '') {
      return <span>Hello, {loginRole}</span>;
    }
  };

  return (
    <>
      {user()}
      <Link to="/shoppingCart">
        <button role="Cart">Cart</button>
      </Link>
      <span style={styles.borderRound}>{basketNumber}</span>
    </>
  );
};

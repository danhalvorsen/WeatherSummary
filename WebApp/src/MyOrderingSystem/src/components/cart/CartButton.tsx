import { Link } from 'react-router-dom';
import { useShoppingCart } from '../context/ShoppingCartContext';

export const CartButton = () => {
  const stateFunctionality = useShoppingCart();

  const styles = {
    borderRound: {
      color: 'white',
      backgroundColor: 'red',
      padding: '5px',
      border: '1px solid black',
      borderRadius: '20px',
    },
  };

  const basketNumber = stateFunctionality.cartItemsQuantity || 0;
  return (
    <>
      <Link to="/shoppingCart">
        <button>Cart</button>
      </Link>
      <span style={styles.borderRound}>{basketNumber}</span>
    </>
  );
};

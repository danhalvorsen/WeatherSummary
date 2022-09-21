import React from 'react';
import { useSelector} from 'react-redux';
import { Link } from 'react-router-dom';
import { useShoppingCart } from '../context/ShoppingCartContext';
import {
  decrement,
  selectCount,
  Increment
} from '../features/cart/CartSlice';


export const CartButton = () => {
  const stateFunctionality = useShoppingCart()

  const styles = {
    borderRound: {
      color: 'white',
      backgroundColor: 'red',
      padding: '5px',
      border: '1px solid black',
      borderRadius: '20px',
    },
  };

  // const basketNumber = useSelector(selectCount);
  const basketNumber = stateFunctionality.cartQuantity || 0
  return (
    <>
     <Link to="/shoppingCart"><button>Cart</button></Link> 
      <span style={styles.borderRound}>{basketNumber}</span>
      {/* <span style={styles.borderRound}>{basketNumber}</span> */}
    </>
  );
};

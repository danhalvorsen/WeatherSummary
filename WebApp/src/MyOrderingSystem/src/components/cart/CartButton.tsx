import React from 'react';
import { useSelector} from 'react-redux';
import {
  decrement,
  selectCount,
  Increment
} from '../features/cart/CartSlice';


export const CartButton = () => {
  const styles = {
    borderRound: {
      color: 'white',
      backgroundColor: 'red',
      padding: '5px',
      border: '1px solid black',
      borderRadius: '20px',
    },
  };

  const basketNumber = useSelector(selectCount);

  return (
    <>
      <button>Cart</button>
      <span style={styles.borderRound}>{basketNumber}</span>
    </>
  );
};

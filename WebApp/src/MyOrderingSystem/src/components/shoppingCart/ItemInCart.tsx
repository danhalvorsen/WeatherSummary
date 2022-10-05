import { useEffect } from 'react';
import { CartItem, useShoppingCart } from '../context/ShoppingCartContext';

export type ItemInCartProps = {
  item: CartItem;
};
let total: number;
// eslint-disable-next-line @typescript-eslint/no-unused-vars
total = 0;
export const ItemInCart = ({ item }: ItemInCartProps) => {
  const { increaseQuantity, decreaseQuantity, removeFromCart, products , loadProducts } =
    useShoppingCart();

    useEffect(()=>{
      loadProducts();
    },[])

  const myItem = products?.find((product) => product.id == item.id);

  if (myItem == null) return null;

  total += myItem.price * item.quantity;

  const shoppingPreview = (
    <div>
      <div style={{ margin: '50px' }}>
        <div style={{ border: '3px' }}>
          <div>
            <img
              style={{ height: '80px', width: '100px' }}
              src={myItem.imageUrl}
            />
            <strong>{myItem.title}</strong>{' '}
            <span>
              x <strong style={{ color: 'red' }}>{item.quantity}</strong>
            </span>
            <div style={{ marginLeft: '10%' }}>
              Price: {myItem.price}{' '}
              <span style={{ marginLeft: '10%' }}>
                sum: {myItem.price * item.quantity}{' '}
              </span>{' '}
            </div>
            <div>
              <button
                onClick={() => removeFromCart(item.id)}
                style={{ marginLeft: '25%', color: 'red' }}
              >
                &times;
              </button>
            </div>
            <span>
              {' '}
              <button
                onClick={() => decreaseQuantity(item.id)}
                style={{ marginLeft: '10%', color: 'red' }}
              >
                -
              </button>
              <button
                onClick={() => increaseQuantity(item.id)}
                style={{ marginLeft: '1%', color: 'red' }}
              >
                +
              </button>
            </span>
          </div>
        </div>
        <hr />
      </div>
      <div></div>
    </div>
  );

  return <div>{shoppingPreview}</div>;
};

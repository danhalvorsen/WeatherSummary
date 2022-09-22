import { CartItem, useShoppingCart } from '../context/ShoppingCartContext';

type ItemInCartProps = {
  item: CartItem;
};

export const ItemInCart = ({ item }: ItemInCartProps) => {
  const stateFunctionality = useShoppingCart();
  const productList = stateFunctionality.productList;

  const myItem = productList?.find((product) => product.id == item.id);
  if (myItem == null) return null;

  return (
    <>
      {' '}
      <div style={{ margin: '50px' }}>
        <div style={{ border: '3px' }}>
          <div>
            <img
              style={{ height: '80px', width: '100px' }}
              src={myItem.imageurl}
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
              <button style={{ marginLeft: '30%', color: 'red' }}>
                &times;
              </button>
            </div>
          </div>
        </div>
        <hr />
      </div>
    </>
  );
};

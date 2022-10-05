import { useShoppingCart } from '../context/ShoppingCartContext';
import { ItemInCart } from './ItemInCart';

export const ShoppingCart = () => {
  //   const stateFunctionality = useShoppingCart();
  //   const items = stateFunctionality.cartItems;
  const { cartItems: myCartItems, products } = useShoppingCart();

  let sum = 0;
  myCartItems.forEach((item) => {
    const itemQuantity = item.quantity;
    const price = products?.find((i) => i.id == item.id)?.price || 0;
    sum += price * itemQuantity;
  });
  return (
    <>
      <h1>My shopping list</h1>

      {myCartItems.map((item) => {
        return <ItemInCart key={item.id} item={item} />;
      })}
      <div>
        {(myCartItems.find((item) => item) !== undefined && (
          <h2 style={{ marginLeft: '15%' }}>Total Price:{sum}</h2>
        )) || <h2 style={{ padding: '250px' }}>the Shopping Cart is Empty</h2>}
      </div>
    </>
  );
};

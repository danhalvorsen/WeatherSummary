import { useShoppingCart } from '../context/ShoppingCartContext';
import { ItemInCart } from './ItemInCart';

export const ShoppingCart = () => {
  const stateFunctionality = useShoppingCart();
  const items = stateFunctionality.myItems;
  return (
    <>
      <h1>My shopping list</h1>

      {items.map((item) => {
        return <ItemInCart key={item.id} item={item} />;
      })}
    </>
  );
};

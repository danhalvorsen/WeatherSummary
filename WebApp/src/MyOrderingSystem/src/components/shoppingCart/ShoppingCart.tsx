import { useShoppingCart } from '../context/ShoppingCartContext';
import { ItemInCart } from './ItemInCart';

export const ShoppingCart = () => {
  const stateFunctionality = useShoppingCart();
  console.log('------');
  const itemData = stateFunctionality.myData
  return (
    <>
      <h1>My shopping list</h1>

      {stateFunctionality.myItems.map((item) => {
        return (
          
            <ItemInCart item={item} />
          
        );
      })}
    </>
  );
};

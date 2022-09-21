import { useShoppingCart } from '../context/ShoppingCartContext';

export const ShoppingCart = () => {
  const stateFunctionality = useShoppingCart();
  console.log('------');
  console.log(stateFunctionality.myItems[0]);
  return (
    <>
      <h1>My shopping list</h1>

      {stateFunctionality.myItems.map((item) => (
        <div>
          <p>{item.id}</p>
          <p>{stateFunctionality.myItems[0].id}</p>
        </div>
      ))}
    </>
  );
};

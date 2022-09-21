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
      <p>the title is :<strong>{myItem.title}</strong> </p>
    </>
  );
};

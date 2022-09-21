import { useContext, createContext, ReactNode, useState } from 'react';
type ShoppingCartProviderProps = {
  children: ReactNode | JSX.Element;
};

type ShoppingCartContest = {
  getItemQuantity: (id: number) => number;
  addItemToCart: (id: number) => void;
  cartQuantity: number;
   myItems: CartItem[];
};
type CartItem = {
  id: number;
  quantity: number;
};

const ShoppingCartContext = createContext({} as ShoppingCartContest);

export function useShoppingCart() {
  return useContext(ShoppingCartContext);
}

export function ShoppingCartProvider({ children }: ShoppingCartProviderProps) {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  function getItemQuantity(id: number) {
    return cartItems.find((item) => item.id == id)?.quantity || 0;
  }
  function addItemToCart(id: number) {
    // if you have the item(its id) in the list, so you should just increase its quantity

    setCartItems((currentItems) => {
      if (currentItems.find((item) => item.id == id) == null) {
        //add this item to the state
        return [...currentItems, { id: id, quantity: 1 }];
      } else {
        //search into state to fine the item and add its quantity
        return currentItems.map((item) => {
          if (item.id == id) {
            return { ...item, quantity: item.quantity + 1 };
          } else {
            return item;
          }
        });
      }
    });

    return;
  }
  let totalQuantity = 0;
  cartItems.forEach((item) => {
    totalQuantity += item.quantity;
  });
  const cartQuantity = totalQuantity;

  const myItems = cartItems
  console.log(myItems)

  return (
    <ShoppingCartContext.Provider
      value={{ getItemQuantity, addItemToCart, cartQuantity , myItems }}
    >
      {children}
    </ShoppingCartContext.Provider>
  );
}

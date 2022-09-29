import {
  useContext,
  createContext,
  ReactNode,
  useState,
  useEffect,
} from 'react';
import {
  getAllProducts,
  ProductQuery,
} from '../../../Data/CommunicationService';
import { ProductType } from '../Form/productType';
type ShoppingCartProviderProps = {
  children: ReactNode | JSX.Element;
};

export type ShoppingCartContext = {
  getItemQuantity: (id: number) => number;
  increaseQuantity: (id: number) => void;
  decreaseQuantity: (id: number) => void;
  removeFromCart: (id: number) => void;
  cartItemsQuantity: number;
  cartItems: CartItem[];
  products: ProductType[] | undefined;
  loginRole: string;
  changeLoginRole: (role: string) => void;
};
export type CartItem = {
  id: number;
  quantity: number;
};

const ShoppingCartContext = createContext({} as ShoppingCartContext);

export function useShoppingCart() {
  return useContext(ShoppingCartContext);
}

export function ShoppingCartProvider({ children }: ShoppingCartProviderProps) {
  const [products, setProducts] = useState<ProductType[]>();
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [loginRole , setLoginRole] = useState('');

  function getItemQuantity(id: number) {
    return cartItems.find((item) => item.id == id)?.quantity || 0;
  }
  function increaseQuantity(id: number) {
    // if you have the item(its id) in the list, so you should just increase its quantity, otherwise you should add it to the cartItems state.

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
  function decreaseQuantity(id: number) {
    // here we have the item in the state, so search for its id, if its quantity was 1, then remove it from the list, otherwise decrease its quantity
    setCartItems((currentItems) => {
      if (currentItems.find((item) => item.id == id)?.quantity == 1) {
        return currentItems.filter((item) => item.id !== id);
      } else {
        return currentItems.map((item) => {
          if (item.id == id) {
            return { ...item, quantity: item.quantity - 1 };
          } else {
            return item;
          }
        });
      }
    });

    return;
  }
  function removeFromCart(id: number) {
    setCartItems((currentItems) => {
      return currentItems.filter((item) => item.id !== id);
    });
  }

  //calculate sum of the quantity of each item in the basket to show in round rea badged
  let cartItemsQuantity = 0;
  cartItems.forEach((item) => {
    cartItemsQuantity += item.quantity;
  });

  const baseUrl = 'http://localhost:3002';
  const productsQuery = new ProductQuery();
  productsQuery.baseUrl = baseUrl;
  productsQuery.parameter = '/products';
  useEffect(() => {
    const Data = getAllProducts(productsQuery);
    Data.then((result) => {
      setProducts(result);
    });
  }, []);

  function changeLoginRole(role: string){
    setLoginRole(role);
  }
  return (
    <ShoppingCartContext.Provider
      value={{
        getItemQuantity,
        increaseQuantity,
        decreaseQuantity,
        removeFromCart,
        cartItemsQuantity,
        cartItems,
        products: products,
        loginRole,
        changeLoginRole
      }}
    >
      {children}
    </ShoppingCartContext.Provider>
  );
}

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

type ShoppingCartContest = {
  getItemQuantity: (id: number) => number;
  increaseQuantity: (id: number) => void;
  decreaseQuantity: (id: number) => void;
  removeFromCart: (id: number) => void;
  cartQuantity: number;
  myItems: CartItem[];
  productList: ProductType[] | undefined;
};
export type CartItem = {
  id: number;
  quantity: number;
};

const ShoppingCartContext = createContext({} as ShoppingCartContest);
//const ShoppingCartContext = createContext<ShoppingCartContest | {}>({});

export function useShoppingCart() {
  return useContext(ShoppingCartContext);
}

export function ShoppingCartProvider({ children }: ShoppingCartProviderProps) {
  //I need a state to get my data from mock server to be able to send it shopping cart view
  const [products, setProducts] = useState<ProductType[]>();
  //needs special state for our shopping cart to keep items in the cart
  const [cartItems, setCartItems] = useState<CartItem[]>([]);

  function getItemQuantity(id: number) {
    // this ? after an object is a "Optional chaining" . if you have item in the object it return quantity of the object, otherwise it return default vale, 0.
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
    setCartItems((currentItems)=>{
            return currentItems.filter(item => item.id !== id)
    })
  }

  //calculate sum of the quantity of each item in the basket to show in round rea badged
  let totalQuantity = 0;
  cartItems.forEach((item) => {
    totalQuantity += item.quantity;
  });
  const cartQuantity = totalQuantity;

  const myItems = cartItems;
  const productsList = products;

  //needs to have json data of products here
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

  return (
    <ShoppingCartContext.Provider
      value={{
        getItemQuantity,
        increaseQuantity,
        decreaseQuantity,
        removeFromCart,
        cartQuantity,
        myItems,
        productList: productsList,
      }}
    >
      {children}
    </ShoppingCartContext.Provider>
  );
}

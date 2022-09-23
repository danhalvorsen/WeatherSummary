export type Children = JSX.Element | JSX.Element[];

export type ProductType = {
  id: number;
  title: string;
  description: string;
  customerprice: number;
  boughtprice: number;
  stock: number;
  brand: string;
  category: string;
  imageUrl: string;
  coupon: number;
  price: number;
};

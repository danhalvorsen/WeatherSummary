export type Children = JSX.Element | JSX.Element[];

export type productsType = {
  id: number;
  title: string;
  description: string;
  customerprice?: number | null;
  boughtprice?: number | null;
  stock: number;
  brand: string;
  category: string;
  imageurl: string;
  coupon: number;
  price?: number | null;
}

// export type productType = {
//   id: number;
//   title: string;
//   description: string;
//   customerPrice: number;
//   boughtPrice: number;
//   stock: number;
//   brand: string;
//   category: string;
//   imageUrl: string;
//   coupon: number;
// };

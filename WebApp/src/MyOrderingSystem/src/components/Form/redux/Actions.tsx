import { ProductType } from "../productType";

export const ADD_TO_CART = '[PRODUCT] ADD_TO_CART';

// export function addToCart(product: ProductType){
//     return {type: ADD_TO_CART , payload: product}

// }

export const addToCart = (product: ProductType)=>{
return {type: ADD_TO_CART , payload: product}
}

import { ProductType } from '../productType';
import { ADD_TO_CART } from '../redux/Actions';

type actionType = {
  type: string;
  payload: ProductType;
};

export const cartReducer = (state = [], action: actionType) => {
  switch (action.type) {
    case ADD_TO_CART:
      return [...state, action.payload];
      default: return state;
  }
};

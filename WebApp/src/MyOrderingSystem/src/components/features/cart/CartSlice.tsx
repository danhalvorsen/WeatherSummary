import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { ProductType } from '../../Form/productType';

export interface CounterState {
  value: number;
  cart?: ProductType[];
}
const initialState: CounterState = {
  value: 0,
  // ,    cart: []
};
export const cartSlice = createSlice({
  name: 'counter',
  initialState,
  reducers: {
    Increment: (state) => {
      state.value += 1;
    },
    decrement: (state) => {
      state.value -= 1;
    },
    // addToCart : (state, action: PayloadAction<ProductType>)=>{
    //     state.cart = [action.payload]
    // }
  },
});

export const selectCount = (state: { counter: { value: any } }) =>
  state.counter.value;
export const { Increment, decrement } = cartSlice.actions;

export default cartSlice.reducer;

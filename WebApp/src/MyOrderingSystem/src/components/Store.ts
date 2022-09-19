import { combineReducers, configureStore } from '@reduxjs/toolkit';
import reducer from './features/cart/CartSlice';

const rootReducer = combineReducers({ counter: reducer });

export const store = configureStore({
  reducer: rootReducer,
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

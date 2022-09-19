import React, { useState } from 'react';
import { RootState } from "../../Store";

import { useSelector, useDispatch } from 'react-redux';
import {
  decrement,
  selectCount,

  Increment
} from './CartSlice';

export function Cart() {
  const count = useSelector(selectCount);
  const dispatch = useDispatch();

  return (
    <div>
      <div>
        <button
          onClick={() => dispatch(Increment())}
        >
          +
        </button>
        <span>{count}</span>
        <button
          onClick={() => dispatch(decrement())}
        >
          -
        </button>
      </div>
      </div>
  );
}

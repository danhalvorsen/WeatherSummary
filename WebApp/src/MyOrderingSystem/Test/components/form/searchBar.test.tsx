/* eslint-disable @typescript-eslint/no-unused-vars */
import { render, fireEvent } from '@testing-library/react';
import React, { useState } from 'react';
import ProductsState from '../../../src/components/Form/ProductsState/ProductsState';
import SearchBar from '../../../src/components/Form/ProductsState/SearchBar';

const [searchFilter, setSearchFilter] = useState('d');
let text;
test('component should receive correct props ', () => {
  const setStateMock = jest.fn();
  const useStateMock: any = (useState: any) => [useState, setStateMock];
  jest.spyOn(React, 'useState').mockImplementation(useStateMock);

  const { getByText } = render(<SearchBar setSearchFilter={setStateMock} />);
  text = getByText('iphone');
  expect('iphone').toBeInTheDocument();

  render(<ProductsState />);
  render(<SearchBar setSearchFilter={setSearchFilter} />);
  setSearchFilter('iphone');
});

// test('test input value' , ()=>{

//     const setSearch = jest.fn((value) => {})

//     const { queryByPlaceholderText } = render(<SearchBar setSearch={setSearch} />)
//     const searchInput = queryByPlaceholderText('Insert')

//     fireEvent.change(searchInput, { target: { value: 'test' } })

//     expect(searchInput.value).toBe('test')

// })

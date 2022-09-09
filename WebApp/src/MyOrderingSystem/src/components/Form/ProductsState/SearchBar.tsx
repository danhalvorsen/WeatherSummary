import React from 'react';

export default function SearchBar() {
  return (
    <div className="border border-secondary p-1 m-2 mb-5">
      <label>Search:</label>{' '}
      <input type="text" placeholder="Insert product Name" />
    </div>
  );
}

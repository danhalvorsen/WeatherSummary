import React from 'react';

export default function SearchBar() {
  //Style
  const styles = {
    border: {
      padding: '5px',
      margin: '2px',
      border: '1px solid blue',
      
    },
  };
  return (
    <div style={styles.border}>
      <label>Search:</label>{' '}
      <input type="text" placeholder="Insert product Name" />
    </div>
  );
}

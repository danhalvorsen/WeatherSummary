import React, { useEffect } from 'react';

type SearchBarProps = {
  setSearchFilter: React.Dispatch<React.SetStateAction<string>>;
};
export default function SearchBar(props: SearchBarProps) {
  //Style
  const styles = {
    border: {
      padding: '5px',
      margin: '2px',
      border: '1px solid blue',
    },
  };

  // useEffect(()=>{
  //     props.setFilter('?q=laptop');

  // },[])

  return (
    <form>
      <div style={styles.border}>
        <label>Search:</label>{' '}
        <input
          type="search"
          placeholder="Insert product Name"
          onChange={(e) => {
            props.setSearchFilter(e.target.value);
          }}
        />
      </div>
    </form>
  );
}

import React from 'react';



type SearchBarProps = {
  setSearchFilter: React.Dispatch<React.SetStateAction<string>>;
};

export default function SearchBar({ setSearchFilter }: SearchBarProps) {
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
      <input
        type="search"
        onChange={(e) => {
          e.preventDefault();
          setTimeout(() => {
            setSearchFilter(e.target.value);
          }, 1500);
        }}
      />
    </div>
  );
}

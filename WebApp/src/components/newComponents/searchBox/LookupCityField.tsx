interface FormElements extends HTMLFormControlsCollection {
  cityNameInput: HTMLInputElement;
}
interface UsernameFormElement extends HTMLFormElement {
  readonly elements: FormElements;
}

type lookupCityFieldhProps = {
  children?: JSX.Element | JSX.Element[];
  state?: boolean;
  value?: string;
  cityName: (newCityName: string) => void;
};

export const LookupCityField = (props?: lookupCityFieldhProps): JSX.Element => {
  //   let buffer: string;
  //   const alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
  // const handleChange = (e: React.KeyboardEvent<HTMLInputElement>) => {
  //     if (e.key == 'Enter') {
  //         props?.cityName(buffer)
  //         buffer = ''
  //         return
  //     }
  //     if (e.key == 'Backspace') {
  //         buffer = buffer.substring(0, buffer.length - 1)
  //     }

  //     if (e.key !== undefined && alphabet.includes(e.key)) buffer += e.key
  // }

  const handleSubmit = (event: React.FormEvent<UsernameFormElement>): void => {
    event.preventDefault();
    props?.cityName(event.currentTarget.elements.cityNameInput.value);
    console.log(event.currentTarget.elements.cityNameInput.value);
  };
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const setInter = setInterval(() => {
      props?.cityName(e.target.value);
      clearInterval(setInter);
    }, 3000);
    // clearInterval(setInter);
  };

  return (
    <>
      <div className="border border-success mx-5 mb-2">
        <br />
        <form onSubmit={handleSubmit}>
          <label>Search:</label>
          <input
            type="text"
            id="cityNameInput"
            placeholder="City Name..."
            onChange={handleChange}
            // onKeyDown={handleChange}
          />
          {/* <SearchButton /> */}
        </form>
      </div>
    </>
  );
};

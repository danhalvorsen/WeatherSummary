interface IinputType {
    City : (event: string) => void
}

export default function InputCity(props: IinputType){

    const passText = (e: any)=> props.City(e.target.value);

    return(
        <>
        <label>city name: </label> <input type='text' onChange={passText}></input>
        </>
    )
}
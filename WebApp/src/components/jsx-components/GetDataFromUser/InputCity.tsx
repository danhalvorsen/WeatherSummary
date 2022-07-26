interface IinputType {
    City : (event: string) => void
}

export default function InputCity(props: IinputType){

    


   var cityName: string;

   const getText = (e: any)=>{
    cityName = e.target.value;
    }

    const passText = (event:any)=>{ 
        event.preventDefault()
        props.City(cityName);
    }

    return(
        <>
        <form onSubmit={passText}>
        <label>city name: </label> <input type='text' onChange={getText}></input>
        </form>
        </>
    )
}
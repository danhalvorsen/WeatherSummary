
interface IfuncType {
    func : ()=>void
}


export function ButtonSearch (props: IfuncType){

    const submit = ()=>{

    }

    return (
        <>
              <button type="button" className="btn btn-success" onClick={props.func}>Search City2</button>

        </>
    )
}

interface IfuncType {
    func: () => void | JSX.Element
}


export function ButtonSearch (props: IfuncType){

    return (
        <>
              <button type="button" className="btn btn-success" onClick={props.func}>Search City2</button>

        </>
    )
}
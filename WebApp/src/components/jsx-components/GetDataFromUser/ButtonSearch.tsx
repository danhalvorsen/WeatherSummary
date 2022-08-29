import { Dispatch, FC, SetStateAction } from 'react'

interface IFlagType {
    setFlag: Dispatch<SetStateAction<boolean>>
}

export const ButtonSearch: FC<IFlagType> = ({ setFlag }) => {
    return (
        <>
            <button
                type="button"
                className="btn btn-success"
                onClick={() => {
                    setFlag(true)
                }}
            >
                Search City2
            </button>
        </>
    )
}

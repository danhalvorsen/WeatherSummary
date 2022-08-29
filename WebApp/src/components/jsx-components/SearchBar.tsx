import React, { ChangeEvent } from 'react'
import { Checkbox } from './Checkbox'

export type ISearchbarProps = {
    onSubmit: string
    fn: (e: ChangeEvent) => void
}

class SearchBar extends React.Component<ISearchbarProps> {
    state = { term: '' }

    onInputSubmit = (event: any) => {
        event.preventDefault()
        this.props.fn(event)
    }

    render() {
        return (
            <div>
                <form onSubmit={this.onInputSubmit}>
                    <div className="container">
                        <div className="mb-3">
                            <label
                                htmlFor="exampleFormControlTextarea1"
                                className="form-label"
                            >
                                Search for a City:
                            </label>
                            <input
                                className="form-control"
                                id="exampleFormControlTextarea1"
                                placeholder="Enter a location name"
                                onChange={(e) =>
                                    this.setState({ term: e.target.value })
                                }
                            ></input>

                            <Checkbox />
                        </div>
                    </div>
                </form>
            </div>
        )
    }
}
export default SearchBar

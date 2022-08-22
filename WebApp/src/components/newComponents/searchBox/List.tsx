import { ListItem } from './ListItem'

export const List: React.FC = (props) => {
    return (
        <>
            <div className="border border-info m-3 mt-5 pt-2 border-2">
                <h3>List</h3>
                <ListItem />
                <ListItem />
                <ListItem />
                <ListItem />
            </div>
        </>
    )
}

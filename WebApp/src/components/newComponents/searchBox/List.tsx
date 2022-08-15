import { ListItem } from './ListItem'

export const List: React.FC = (props) => {
    return (
        <>
            <div className="border border-primary m-2">
                <h3>List</h3>
                {props?.children}
                <ListItem />
                <ListItem />
                <ListItem />
                <ListItem />
            </div>
        </>
    )
}

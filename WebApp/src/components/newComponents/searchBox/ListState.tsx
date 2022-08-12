import { List } from "./List";

export const ListState: React.FC = (props) => {
    return (
        <>
            <div className="border border-dark m-3 mt-5">
                <h2>List State</h2>
                {props?.children}
                <List />
            </div>
        </>
    );
};

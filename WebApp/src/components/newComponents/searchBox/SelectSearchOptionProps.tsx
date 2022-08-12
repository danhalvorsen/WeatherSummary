import { Children } from "../Form/compTypes";

type SelectSearchOptionProps = {
    children?: Children;
};

export const SelectSearchOption: React.FC<SelectSearchOptionProps> = (
    props: SelectSearchOptionProps
) => {
    return (
        <div>
            <h3>SelectSearchOption</h3>
            {props?.children}
        </div>
    );
};

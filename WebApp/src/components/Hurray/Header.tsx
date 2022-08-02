import { ReactChildren, FC, PropsWithChildren } from 'react';
import ReactDOM from 'react-dom';

type headerProps = {
    title: string
    children: JSX.Element
};

const Page: React.FC<headerProps> = ({
    title,
    children,
}) => (
    <div>
        <h1>{title}</h1>
        {children}
    </div>
);

type navProps = {
    title: string
    children: JSX.Element
};

const Nav: React.FC<navProps> = ({
    title,
    children,
}) => (
    <div>
        <h1>{title}</h1>
        {children}
    </div>
)

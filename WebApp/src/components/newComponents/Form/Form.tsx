interface FormProps {
  children?: JSX.Element | JSX.Element[];
}

export const Form = (props: FormProps) => {
  return <div className="border border-primary m-2">{props?.children}</div>;
};

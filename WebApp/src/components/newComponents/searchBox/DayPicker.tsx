export const DayPicker = (): JSX.Element => {
    const onSubmitInput: React.ChangeEvent = (
        date: React.FormEventHandler<HTMLInputElement>
    ) => {}
    return (
        <>
            <input
                type="datetime-local"
                placeholder="Your ....."
                onChange={onSubmitInput}
            />
        </>
    )
}

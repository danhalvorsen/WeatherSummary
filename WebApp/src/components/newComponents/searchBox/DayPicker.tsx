export const DayPicker = () => {
    const onSubmitInput = (
        date: React.FormEventHandler<HTMLInputElement>
    ) => {};
    return (
        <>
            <input type="datetime-local" placeholder="YYYY-MM-DD" />
        </>
    );
};

export const TodayDateShow = () => {
    const todayDate = new Date().toLocaleDateString("en-us", {
        weekday: "long",
        month: "long",
        day: "numeric"
    });

    return (
        <>
            <strong> {todayDate}</strong>
        </>
    );
};

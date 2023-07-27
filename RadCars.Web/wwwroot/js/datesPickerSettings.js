const minutesAfter = 30 * 60 * 1000;
const dayAfter = 24 * 60 * 60 * 1000;
const updateIntervalMs = 5000;

let startTimeDifference = null;

const now = new Date();
const startMinTime = new Date(now.getTime() + minutesAfter); // 30 minutes from now

const fpStart = flatpickr("#start-time", {
    enableTime: true,
    dateFormat: "d.m.Y в H:i часа",
    defaultDate: startMinTime,
    minDate: startMinTime,
    minuteIncrement: 1,
    time_24hr: true,
    locale: 'bg',
    onChange: function (selectedDates, dateStr, instance) {
        const now = new Date();
        startTimeDifference = selectedDates[0] - now;

        // Update the end time picker when the start time is changed
        const endMinTimeFromStart = new Date(selectedDates[0].getTime() + dayAfter); // 24 hours from start time
        if (fpEnd.selectedDates[0] < endMinTimeFromStart) {
            fpEnd.setDate(endMinTimeFromStart);
        }
        fpEnd.set('minDate', endMinTimeFromStart);
    }
});

const fpEnd = flatpickr("#end-time", {
    enableTime: true,
    dateFormat: "d.m.Y в H:i часа",
    defaultDate: new Date(startMinTime.getTime() + dayAfter),
    minDate: new Date(startMinTime.getTime() + dayAfter),
    minuteIncrement: 1,
    time_24hr: true,
    locale: 'bg'
});

setInterval(function () {
    const now = new Date();
    const startMinTime = new Date(now.getTime() + minutesAfter); // 30 minutes from now

    if (fpStart.selectedDates[0] < startMinTime) {
        fpStart.setDate(startMinTime);
    }
    fpStart.set('minDate', startMinTime);

    const startTime = fpStart.selectedDates[0];
    const endMinTimeFromStart = new Date(startTime.getTime() + dayAfter); // 24 hours from start time

    if (fpEnd.selectedDates[0] < endMinTimeFromStart) {
        fpEnd.setDate(endMinTimeFromStart);
    }
    fpEnd.set('minDate', endMinTimeFromStart);
}, updateIntervalMs);

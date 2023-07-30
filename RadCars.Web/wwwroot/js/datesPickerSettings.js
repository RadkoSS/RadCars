const minutesAfter = 5 * 60 * 1000; //ToDo: fix this! For testing it is 5 minutes. It should be 30 minutes!
const hoursAfter = 0.2 * 60 * 60 * 1000; //ToDo: fix this! For testing it is 0.2 hours (12 minutes). It should be 24 hours!
const updateIntervalMs = 3000;

let startTimeDifference = null;

const now = new Date();
const startMinTime = new Date(now.getTime() + minutesAfter); // minutes from now

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
        const endMinTimeFromStart = new Date(selectedDates[0].getTime() + hoursAfter); // hours from start time
        if (fpEnd.selectedDates[0] < endMinTimeFromStart) {
            fpEnd.setDate(endMinTimeFromStart);
        }
        fpEnd.set('minDate', endMinTimeFromStart);
    }
});

const fpEnd = flatpickr("#end-time", {
    enableTime: true,
    dateFormat: "d.m.Y в H:i часа",
    defaultDate: new Date(startMinTime.getTime() + hoursAfter),
    minDate: new Date(startMinTime.getTime() + hoursAfter),
    minuteIncrement: 1,
    time_24hr: true,
    locale: 'bg'
});

setInterval(function () {
    const now = new Date();
    const startMinTime = new Date(now.getTime() + minutesAfter); // minutes from now

    if (fpStart.selectedDates[0] < startMinTime) {
        fpStart.setDate(startMinTime);
    }
    fpStart.set('minDate', startMinTime);

    const startTime = fpStart.selectedDates[0];
    const endMinTimeFromStart = new Date(startTime.getTime() + hoursAfter); // hours from start time

    if (fpEnd.selectedDates[0] < endMinTimeFromStart) {
        fpEnd.setDate(endMinTimeFromStart);
    }
    fpEnd.set('minDate', endMinTimeFromStart);
}, updateIntervalMs);

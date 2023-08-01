const connection = new signalR.HubConnectionBuilder()
    .withUrl("/auctionHub")
    .build();

connection.start()
    .catch(err => console.error(err.toString()));

const timerIntervals = {};

const allAuctions = document.querySelectorAll(".card");

allAuctions.forEach((auction) => {
    const auctionId = auction.id.toLowerCase();

    const timeToCountdown = auction.querySelector(".time");
    if (!timeToCountdown) {
        return;
    }

    const timeParts = timeToCountdown.value.replace(' г.', '').split(/[\s.]+/);

    const countdownDate = new Date(timeParts[2], timeParts[1] - 1, timeParts[0], ...timeParts[3].split(':'));

    window.addEventListener("load",
        () => {
            startCountdown(auctionId, countdownDate);
            timerIntervals[auctionId] = setInterval(startCountdown, 1000, auctionId, countdownDate);
        });
});

const startCountdown = (auctionId, countdownDate) => {
    auctionId = auctionId.toLowerCase();
    
    const auction = document.getElementById(auctionId);
    
    const daysElem = auction.querySelector(".days"),
        hoursElem = auction.querySelector(".hours"),
        minutesElem = auction.querySelector(".minutes"),
        secondsElem = auction.querySelector(".seconds"),
        timer = auction.querySelector(".timer");

    if (!timer) {
        return;
    }

    const formatTime = (time, string) => {
        if (string === "ден") {
            return time == 1 ? `${time} ${string}` : `${time} дни`;
        } else if (string === "час") {
            return time == 1 ? `${time} ${string}` : `${time} ${string}а`;
        } else if (string === "минута") {
            return time == 1 ? `${time} ${string}` : `${time} минути`;
        } else {
            return time == 1 ? `${time} ${string}` : `${time} секунди`;
        }
    };

    const now = new Date().getTime();
    const countdown = new Date(countdownDate).getTime();

    const difference = (countdown - now) / 1000;

    if (difference < 1) {
        endCountdown(auctionId);
    }

    const days = Math.floor(difference / (60 * 60 * 24));
    const hours = Math.floor((difference % (60 * 60 * 24)) / (60 * 60));
    const minutes = Math.floor((difference % (60 * 60)) / 60);
    const seconds = Math.floor(difference % 60);

    daysElem.innerHTML = formatTime(days, "ден");
    hoursElem.innerHTML = formatTime(hours, "час");
    minutesElem.innerHTML = formatTime(minutes, "минута");
    secondsElem.innerHTML = formatTime(seconds, "секунда");
};

const endCountdown = (auctionId) => {
    auctionId = auctionId.toLowerCase();
    const auction = document.getElementById(auctionId);

    if (!auction) {
        return;
    }

    const timerInterval = timerIntervals[auctionId];

    clearInterval(timerInterval);

    delete timerIntervals[auctionId];

    const timer = auction.querySelector(".timer");
    if (timer) {
        timer.classList.add("visually-hidden");
    }
};

connection.on("AllPageBidPlaced", (auctionId, amount) => {
    auctionId = auctionId.toLowerCase();
    const auction = document.getElementById(auctionId);

    if (!auction) {
        return;
    }
    const timer = auction.querySelector(".timer");

    if (!timer) {
        return;
    }
    const timerText = timer.querySelector(".timerText");

    timerText.textContent = `Моментна цена ${amount.toFixed(2)} лв. Приключва след:`;
});

connection.on("AllPageAuctionStarted", (auctionId, endTime, startingPrice) => {
    auctionId = auctionId.toLowerCase();
    const auction = document.getElementById(auctionId);
    
    if (!auction) {
        return;
    }
    const timer = auction.querySelector(".timer");

    if (!timer) {
        return;
    }

    endCountdown(auctionId);

    const countdownDate = new Date(endTime);
    const auctionActions = auction.querySelector(".auctionControls");

    const timerText = timer.querySelector(".timerText");
    timerText.textContent = `Начална цена ${startingPrice.toFixed(2)} лв. Приключва след:`;

    timer.classList.remove("visually-hidden");
    startCountdown(auctionId, countdownDate);
    timerIntervals[auctionId] = setInterval(startCountdown, 1000, auctionId, countdownDate);

    if (auctionActions) {
        auctionActions.classList.add("visually-hidden");
    }
});

connection.on("AllPageAuctionEnded", (auctionId) => {
    auctionId = auctionId.toLowerCase();
    const auction = document.getElementById(auctionId);
    
    if (!auction) {
        return;
    }

    endCountdown(auctionId);

    const auctionInfoContainer = auction.querySelector(".auctionInfoContainer");

    const auctionEndAnnounce = document.createElement("div");
    auctionEndAnnounce.classList.add("text-center", "mb-3");

    const announceText = document.createElement("h4");
    announceText.classList.add("text-decoration-underline", "fw-bold");
    announceText.textContent = "Търгът приключи!";
    auctionEndAnnounce.appendChild(announceText);

    auctionInfoContainer.prepend(auctionEndAnnounce);

    const auctionActions = auction.querySelector(".auctionControls");

    if (auctionActions) {
        auctionActions.classList.remove("visually-hidden");
    }

    const timer = auction.querySelector(".timer");

    if (timer) {
        timer.remove();
    }
});
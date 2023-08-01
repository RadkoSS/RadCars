import { post } from './webApiRequester.js';

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/auctionHub")
    .build();

connection.start()
    .catch(err => console.error(err.toString()));

const timeToCountdown = document.getElementById("time");

let timeParts = timeToCountdown && timeToCountdown.value.replace(' г.', '').split(/[\s.]+/);

let countdownDate = timeParts && new Date(timeParts[2], timeParts[1] - 1, timeParts[0], ...timeParts[3].split(':'));

let timerInterval;

const daysElem = document.getElementById("days"),
    hoursElem = document.getElementById("hours"),
    minutesElem = document.getElementById("minutes"),
    secondsElem = document.getElementById("seconds"),
    timer = document.getElementById("timer");

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

const startCountdown = () => {
    if (!timer) {
        return;
    }

    const now = new Date().getTime();
    const countdown = new Date(countdownDate).getTime();

    const difference = (countdown - now) / 1000;

    if (difference < 1) {
        endCountdown();
    }

    let days = Math.floor(difference / (60 * 60 * 24));
    let hours = Math.floor((difference % (60 * 60 * 24)) / (60 * 60));
    let minutes = Math.floor((difference % (60 * 60)) / 60);
    let seconds = Math.floor(difference % 60);

    daysElem.innerHTML = formatTime(days, "ден");
    hoursElem.innerHTML = formatTime(hours, "час");
    minutesElem.innerHTML = formatTime(minutes, "минута");
    secondsElem.innerHTML = formatTime(seconds, "секунда");
};

const endCountdown = () => {
    clearInterval(timerInterval);
    if (timer) {
        timer.classList.add("visually-hidden");
    }
};

window.addEventListener("load",
    () => {
        startCountdown();
        timerInterval = setInterval(startCountdown, 1000);
    });

const currentUserId = document.getElementById("userId");

const auctionActions = document.getElementById("auctionControls");

const auctionInfoContainer = document.getElementById("auctionInfoContainer");

const bidsDisplay = document.getElementById("bids");
bidsDisplay.scrollTo(0, bidsDisplay.scrollHeight);

const bidFormContainer = document.getElementById("bidFormContainer");

let bidForm = document.getElementById("bidForm");
let bidInput = document.getElementById("bidAmount");

let lastHighestBid = bidInput && parseFloat(bidInput.min);
let minimumStep = bidInput && Number(bidInput.step);

if (bidForm) {
    bidForm.addEventListener("submit", handleBids);
}

async function handleBids(event) {
    event.preventDefault();

    if (!bidInput) {
        bidInput = document.getElementById("bidAmount");
    }

    const bidAmount = Number(bidInput.value);
    const auctionId = document.getElementById("auctionId").value;

    const bidsTotalCount = await post("/api/auction/bids/count", auctionId);

    if (bidsTotalCount === 0) {

        if (bidAmount >= lastHighestBid) {
            connection.invoke("PlaceBid", auctionId, bidAmount)
                .catch(err => console.error(err));

            bidsDisplay.innerHTML = '';

            bidInput.value = '';

            lastHighestBid = bidAmount;
        }

    }
    else if (bidAmount >= lastHighestBid + minimumStep) {
        connection.invoke("PlaceBid", auctionId, bidAmount)
            .catch(err => console.error(err));

        bidInput.value = '';
    }
}

connection.on("BidPlaced", (amount, userFullName, userName, createdOn) => {
    const bidContainer = document.createElement("div");
    bidContainer.classList.add("card", "mb-2", "p-2");

    const dateElement = document.createElement("span");
    dateElement.classList.add("card-header");
    dateElement.textContent = `${createdOn}:`;

    bidContainer.appendChild(dateElement);

    const bidElement = document.createElement("p");
    bidElement.classList.add("card-text", "fw-bold");
    bidElement.textContent = `- ${userFullName} (${userName}) предлага ${amount.toFixed(2)} лв.`;

    if (bidInput) {
        bidInput.min = `${amount}`;
        lastHighestBid = amount;
    }

    bidContainer.appendChild(bidElement);

    bidsDisplay.appendChild(bidContainer);

    bidsDisplay.scrollTo(0, bidsDisplay.scrollHeight);
});

connection.on("AuctionStarted", (auctionId, creatorId, endTime, startingPrice, minimumBid) => {
    console.log(`AUCTION WITH ID: ${auctionId} HAS NOW STARTED!`);

    if (auctionActions) {
        auctionActions.classList.add("visually-hidden");
    }

    countdownDate = new Date(endTime);
    document.getElementById("timerText").textContent = "Край след:";
    timer.classList.remove("visually-hidden");
    startCountdown();
    timerInterval = setInterval(startCountdown, 1000);

    const announceContainer = document.createElement("div");
    announceContainer.classList.add("card", "mb-2", "p-2");

    const auctionStartedAnnounce = document.createElement("span");
    auctionStartedAnnounce.classList.add("card-header");
    auctionStartedAnnounce.textContent = `Търгът вече е активен! Нека наддаването започне!`;

    announceContainer.appendChild(auctionStartedAnnounce);

    const greeting = document.createElement("p");
    greeting.classList.add("card-text", "fw-bold");
    greeting.textContent = `- Успех на всички участници!`;

    announceContainer.appendChild(greeting);

    if (bidsDisplay.childElementCount === 1) {
        bidsDisplay.innerHTML = '';
    }

    bidsDisplay.appendChild(announceContainer);

    if (currentUserId && creatorId !== currentUserId.value) {
        // Create the form
        const form = document.createElement("form");
        form.id = "bidForm";
        form.method = "post";

        // Create the form group
        const formGroup = document.createElement("div");
        formGroup.classList.add("form-group", "mb-2");

        // Create the label
        const label = document.createElement("label");
        label.classList.add("mb-2");
        label.for = "bidAmount";
        label.textContent = "Сума на наддаване";

        // Create the input
        const input = document.createElement("input");
        input.type = "number";
        input.classList.add("form-control");
        input.id = "bidAmount";
        input.name = "bidAmount";
        input.placeholder = "Направете своето наддаване";
        input.min = `${startingPrice}`;
        input.step = `${minimumBid}`;
        input.required = true;

        minimumStep = minimumBid;
        lastHighestBid = startingPrice;

        // Create the button
        const button = document.createElement("button");
        button.type = "submit";
        button.classList.add("btn", "btn-primary");
        button.textContent = "Потвърди";

        // Append everything to the form
        formGroup.appendChild(label);
        formGroup.appendChild(input);
        form.appendChild(formGroup);
        form.appendChild(button);

        form.addEventListener("submit", handleBids);

        bidForm = form;
        bidInput = input;

        bidFormContainer.appendChild(form);
    }
});

connection.on("AuctionEnded", (auctionId, lastBidTime, lastBidAmount, winnerFullNameAndUserName) => {
    console.log(`AUCTION WITH ID: ${auctionId} HAS ENDED!`);

    if (bidForm) {
        bidFormContainer.remove(bidForm);
    }

    if (auctionActions) {
        auctionActions.classList.remove("visually-hidden");
    }

    endCountdown();

    const winnerInfoContainer = document.createElement("div");
    winnerInfoContainer.classList.add("text-center", "mb-3");
    winnerInfoContainer.id = "winnerInfoContainer";

    const winnerAnnounce = document.createElement("h3");

    const lastBidDisplay = document.createElement("div");
    lastBidDisplay.classList.add("card", "mb-2", "p-2", "text-danger");

    const lastBidHeader = document.createElement("span");
    lastBidHeader.classList.add("card-header");

    const lastBidContent = document.createElement("p");
    lastBidContent.classList.add("card-text", "fw-bold");

    if (!lastBidTime || !winnerFullNameAndUserName || lastBidAmount <= 0) {
        winnerAnnounce.textContent = 'Край на търга. Няма победител.';
        winnerAnnounce.classList.add("text-danger");
        lastBidHeader.textContent = "Търгът приключи без наддавания:";
        lastBidContent.textContent = "- Няма победител.";
    } else {
        winnerAnnounce.textContent = `Търгът е спечелен от ${winnerFullNameAndUserName} за сумата от ${lastBidAmount.toFixed(2)} лв.`;
        lastBidHeader.textContent = `Край на търга! Последно наддаване в ${lastBidTime}:`;
        lastBidContent.textContent = `- Победител: ${winnerFullNameAndUserName} с цена от ${lastBidAmount.toFixed(2)} лв.`;
    }

    winnerInfoContainer.appendChild(winnerAnnounce);
    auctionInfoContainer.appendChild(winnerInfoContainer);

    lastBidDisplay.appendChild(lastBidHeader);
    lastBidDisplay.appendChild(lastBidContent);

    bidsDisplay.appendChild(lastBidDisplay);

    bidsDisplay.scrollTo(0, bidsDisplay.scrollHeight);
});

connection.onclose(e => {
    console.log('Connection closed. Error: ', e);
});
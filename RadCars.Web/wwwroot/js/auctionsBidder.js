const connection = new signalR.HubConnectionBuilder()
    .withUrl("/auctionHub")
    .build();

connection.start()
    .catch(err => console.error(err.toString()));

const bidsDisplay = document.getElementById("bids");
const bidForm = document.getElementById("bidForm");

if (bidForm) {
    bidForm.addEventListener("submit", handleBids);
}

async function handleBids(event) {
    event.preventDefault();
    const bidInput = document.getElementById("bidAmount");

    const bidAmount = Number(bidInput.value);
    const auctionId = document.getElementById("auctionId").value;
    connection.invoke("PlaceBid", auctionId, bidAmount)
        .catch(err => console.error(err));
    bidInput.value = '';
}

connection.on("BidPlaced", (amount, userFullName, userName, createdOn) => {
    const bidContainer = document.createElement("div");
    bidContainer.classList.add("card");

    const dateElement = document.createElement("span");
    dateElement.innerText = `${createdOn}:`;

    bidContainer.appendChild(dateElement);

    const bidElement = document.createElement("p");
    bidElement.textContent = `- ${userFullName} (${userName}) предлага ${amount.toFixed(2)} лв.`;
    bidElement.classList.add("fw-bold");

    bidContainer.appendChild(bidElement);
    
    bidsDisplay.appendChild(bidContainer);
});

connection.on("AuctionStarted", (auctionId) => {
    console.log(`AUCTION WITH ID: ${auctionId} HAS NOW STARTED!`);
});

connection.on("AuctionEnded", (auctionId) => {
    console.log(`AUCTION WITH ID: ${auctionId} HAS ENDED!`);
});
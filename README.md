# RadCars-ASP-NET-CORE-PROJECT

RadCars 🚗
RadCars is a cutting-edge car selling application designed to help users find the perfect car based on their preferences. Built with the latest technologies, RadCars aims to provide a seamless experience for both car enthusiasts and casual browsers.

Home page:
![Screenshot 2023-08-10 144614](https://github.com/RadkoSS/RadCars/assets/94465605/0fa9d03c-2629-49ab-a6c5-94eed7f8cb13)

🌟 Features overview:

Search Functionality: Quickly find car Auctions and Listings based on various criteria such as Car Make, Car Model, City, Engine Type, Description, Title, Total Mileage, Price range etc.
User Profiles: Create listings and auctions, save your favorite listings so you get notified about price changes. You can add Auctions to favorites too! Users can place bids on auctions that have started.
Areas separation: There is an Administrator Area where Admins have full control over all the Auctions and Listings.
Web API: Enhancing the user experience and ensuring that the data presented is always up-to-date.
Real-world Car Data: Seeded to the Database, thanks to https://auto.basebuy.ru/
Real-world Cities Data: The cities of Bulgaria seeded to the Database, thanks to https://simplemaps.com/data/bg-cities

🌟 Details about the RadCars application's features: 

Background Jobs scheduling: Auctions are ensured to begin at the desired StartTime and then end at the chosen EndTime thanks to background jobs scheduling. 
Real-Time dynamic refreshing: Real-time refreshing with SignalR for the Auctions ensuring good user experience and instant refresh of pages when a change is being made.
Real-Time Bidding system: Real-Time bids with SignalR for the Auctions, only non-creator Users can bid to an Auction with amounts that meet the rules set by the Creator of the Auction.
Email notifications: Implemented SendGrid for email notifications on new registration and related to the user events in the application - for example, the Creator of the Auction is being notified on Auction End and users are getting notifications when a Listing they had added to their Favorites gets a price decrease by more than 4%.
Responsive design: The application has a responsive design and is working well with different screen sizes.

Installation:

Clone the Repository:
git clone https://github.com/yourusername/RadCars.git

Navigate to the Project Directory:
cd RadCars

Install Required Packages:
dotnet restore

Run the Application:
dotnet run

Getting Started 🚀

Prerequisites:
Update the secrets.json for both the RadCars web app, and the Web API,
Update-Database command,
Create a RadCarsHangfire database for the Background jobs storage,
Run the project!

Testing 🧪

To run the automated tests for RadCars, simply execute:
dotnet test


Built With 🛠️

The architecure of the [ASP.NET MVC Template by Nikolay Kostov(https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master)]
.NET 6 and ASP NET CORE 6: The backbone of the web application.
Entity Framework Core 6: For robust database operations.
Hangfire.SQL: The scheduler providing the application with managing background jobs.
SignalR: For real-time changes and functionalities.
AutoMapper: Efficient object-object mapping.

All the technologies used in this project:

In the back-end:

.NET 6,
ASP NET CORE MVC 6, 
ASP NET CORE WEB API,
EntityFramowork 6 with LazyLoadingProxies,
Hangfire,
SignalR,
AutoMapper,
SendGrid,
NUnit Testing Framework,
Moq,
MoqQueryable for Mocking collection for the Repositories,
ByteSizeLib

In the front-end:

Razor View syntax,
Bootstrap 5,
JQuery,
Vanilla JS,
SweetAlert 2,
Toastr,
FlatPickr,
CSS

Contributing 🤝
We welcome contributions from the community! If you'd like to contribute, please fork the repository and create a pull request. For major changes, open an issue first to discuss your proposed changes.

License 📄
RadCars is licensed under the MIT License. See the LICENSE.md file for more details.

Special Thanks 🙏
A heartfelt appreciation to SoftUni and all the lecturers for their invaluable guidance and support throughout the learning journey that led to the creation of this project.

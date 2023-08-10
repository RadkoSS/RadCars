# RadCars - ASP.NET CORE PROJECT

- **RadCars** 🚗 is a cutting-edge car sales application designed to help users find the perfect car based on their preferences. Built with the latest technologies, RadCars aims to provide a seamless experience for both car enthusiasts and casual browsers.

![HomePage](https://github.com/RadkoSS/RadCars/assets/94465605/b0e5b8a3-629a-4942-83ee-cd3dc45fd933)
![HomePage](https://github.com/RadkoSS/RadCars/assets/94465605/f5a5ac20-1dbb-4c2f-afd9-c44467dfd2d1)

### 📌 Table of Contents

- [Features Overview 🌟](#features-overview)
- [Details about some features 🌟](#details-about-features)
- [User permissions and functionalities](#user-permissions-and-functionalities)
- [Getting Started](#getting-started)
- [Built With 🛠️](#built-with)
- [Demonstrations with Pictures and GIFs](#demonstrations-with-pictures-and-gifs)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

### Features Overview 🌟

- **Search Functionality:** Quickly find car Auctions and Listings based on various criteria.
- **User Profiles:** Create listings, auctions, and save your favorites.
- **Areas Separation:** Admins have full control over all Auctions and Listings.
- **Web API:** Enhancing the user experience with dynamic data updates.
- **Real-world Data:** Seeded car and city data.

### Details about some features 🌟

- **Search Functionality:** Quickly find car Auctions and Listings based on various criteria such as Car Make, Car Model, City, Engine Type, Description, Title, Total Mileage, Price range etc.
- **User Profiles:** Create listings and auctions, save your favorite listings so you get notified about price changes. You can add Auctions to favorites too! Users can place bids on auctions that have started.
- **Areas separation:** There is an Administrator Area where Admins have full control over all the Auctions and Listings.
- **Web API:** Enhancing the user experience and ensuring that the data presented is always up-to-date.
- **Real-world Car Data:** Seeded to the Database, thanks to [TyresAddict](https://tiresaddict.com/help/databases/cars/)
- **Real-world Cities Data:** The cities of Bulgaria seeded to the Database, thanks to [SimpleMaps](https://simplemaps.com/data/bg-cities)
- **Background Jobs scheduling:** Auctions are ensured to begin at the desired StartTime and then end at the chosen EndTime thanks to background jobs scheduling. 
- **Real-Time dynamic refreshing:** Real-time refreshing with SignalR for the Auctions ensuring good user experience and instant refresh of pages when a change is being made.
- **Real-Time Bidding system:** Real-Time bids with SignalR for the Auctions, only non-creator Users can bid to an Auction with amounts that meet the rules set by the Creator of the Auction.
- **Email notifications:** Implemented SendGrid for email notifications on new registration and related to the user events in the application - for example, the Creator of the Auction is being notified on Auction End and users are getting notifications when a Listing they had added to their Favorites gets a price decrease by more than 4%.
- **Responsive design:** The application has a responsive design and is working well with different screen sizes.

### User permissions and functionalities ⛔

- **Users can:** Create, Edit, Deactivate (SoftDelete) and Delete Listings that they have created.
- **Users can:** Create, Edit and Deactivate (SoftDelete) Auction that they have created. They can Edit and Deactivate Auctions that have not yet started. Editing started Auction is not allowed, neither is Deactivating.
- **Users can:** Reactivate an Auction if it ended with no bids or they had previously deactivated before its start.
- **Users and Admins can:** Add Listings and Auctions that they have not created to their collections of Favorites. There are to separated collections - one for Listings and one for Auctions - they can be found in the navigation "Моите: -> Обяви / Търгове -> Любими"
- **Users and Admins can:** See their own active and deactivated Listings and Auctions. There are to separated collections - one for Listings and one for Auctions"
- **Users and Admins can:** See their own Expired and Ongoing Auctions.
- **Admins can:** See all the active and deactivated Listings and Auctions in the Admin area and can modify them.
- **Admins can:** See all the Users of the application and their online status.
- **Admins can:** See, Create, Edit and SoftDelete the Categories of the Car features and also see, create, edit and softDelete all features of each Category.
- **Admins can:** Create, Edit, Deactivate (SoftDelete) and Delete any Listing and Create, Edit, Deactivate (SoftDelete) or Delete any Auction at any time (Deactivating an Auction cancels the background jobs for that Auction if it's not started or is active and also removes all the bids that have been placed if there are any).
- **Admins can:** Reactivate Auction after it ended (deleting all the bids if there are any) or Reactivate Auction that has been Deactivated.

### Getting Started 🚀

- **Update the secrets.json for both the RadCars web app, and the Web API**,
- **Update-Database command**,
- **Create a RadCarsHangfire database for the Background jobs storage**,
- **Run the project!**

### Built With 🛠️

- The architecure of the **[ASP.NET MVC Template by Nikolay Kostov](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master)**
- **.NET 6 and ASP NET CORE 6**: The backbone of the web application.
- **SQL Server**: for the database
- **Entity Framework Core 6** with LazyLoading: For robust database operations.
- **CloudinaryDotNet**: Images uploading to cloud.
- **Hangfire** with SQL Server: The scheduler providing the application with managing background jobs.
- **SignalR**: For real-time changes and functionalities.
- **AutoMapper**: Efficient object-object mapping.
- **SendGrid**: for sending emails,
- **HTML Sanitizer**: for sanitizing user-generated data before sending it to the database
- **Recaptcha V3**,
- **CsvHelper**: for seeding the database from .CSV files
- **NUnit Testing Framework**: for UnitTesting,
- **Moq**: for mocking dependencies,
- **MoqQueryable.Moq**: for mocking collections for the Repositories,
- **ByteSizeLib**
- **Razor View syntax + Bootstrap 5 + CSS + VanillaJS**,
- **JQuery**,
- **SweetAlert 2**: for alerts to the users,
- **Toastr**: for user notifications on interactions,
- **FlatPickr**: for picking dates

### Demonstration pictures and GIFs 📽️




### Contributing 🤝
- I welcome contributions from the community! If you'd like to contribute, please fork the repository and create a pull request. For major changes, open an issue first to discuss your proposed changes.

### License 📄
- RadCars is licensed under the MIT License. See the LICENSE.md file for more details.

### Special Thanks 🙏
- A heartfelt appreciation to SoftUni and all the lecturers for their invaluable guidance and support throughout the learning journey that led to the creation of this project.

# 🚗 RadCars - ASP.NET CORE PROJECT

- **RadCars** 🚗 is a cutting-edge car sales application designed to help users find the perfect car based on their preferences. Built with the latest technologies, RadCars aims to provide a seamless experience for both car enthusiasts and casual browsers.

![HomePage](https://github.com/RadkoSS/RadCars/assets/94465605/b0e5b8a3-629a-4942-83ee-cd3dc45fd933)
![HomePage](https://github.com/RadkoSS/RadCars/assets/94465605/f5a5ac20-1dbb-4c2f-afd9-c44467dfd2d1)

## 📌 Table of Contents

- [Features Overview](#features-overview)
- [Architecture of the project](#architecture-of-the-project)
- [Details about some features](#details-about-some-features)
- [User permissions and functionalities](#user-permissions-and-functionalities)
- [Getting Started](#getting-started)
- [Built With](#built-with)
- [Current Database diagram](#current-database-diagram)
- [Demonstrations with GIFs and pictures](#demonstrations-with-gifs-and-pictures)
- [Contributing](#contributing)
- [License](#license)
- [Special Thanks](#special-thanks)

## Architecture of the project
- The architecture of this project is inspired by the **[ASP.NET MVC Template by Nikolay Kostov](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master)**
  
- **[RadCars.Web](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Web)**: Contains the code for the web application with all the views, controllers, hubs, background job services, etc.
- **[RadCars.Web.ViewModels](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Web.ViewModels)**: Contains all the view models and form models for the application.
- **[RadCars.Api](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Api)**: Contains the code for the Web API.
- **[RadCars.Data](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Data)**: Contains the configurations for all the entities, the implementations of the repositories and the DB context and the seeding for the DB.
- **[RadCars.Data.Models](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Data.Models)**: Contains all the entity classes for the application.
- **[RadCars.Data.Common](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Data.Common)**: Provides abstract generics classes and interfaces, which hold information about the entities. Provides also interfaces for generic repositories - IDeletableEntityRepository and IRepository.
- **[RadCars.Services.Data](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Services.Data)**: Contains the business logic of the application, separated into different services.
- **[RadCars.Services.Data.Models](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Services.Data.Models)**: Contains the service models (DTOs).
- **[RadCars.Services.Data.Tests](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Services.Data.Tests)**: Contains all the unit tests for the business logic.
- **[RadCars.Services.Mapping](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Services.Mapping)**: Contains the IQueryable extension methods for AutoMapper and the logic for registering all mappings. 
- **[RadCars.Services.Messaging](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Services.Messaging)**: Contains an implementation of the SendGrid API for easy emailing services.
- **[RadCars.Infrastructure](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Web.Infrastructure)**: Contains all the extension methods for the web application and registers services
- **[RadCars.Common](https://github.com/RadkoSS/RadCars/tree/master/RadCars.Common)**: Contains global constants, exception and notification messages accessible from every project inside the solution.

## Features Overview

- **Search Functionality:** Quickly find car Auctions and Listings based on various criteria.
- **User Profiles:** Create listings, auctions, and save your favorites.
- **Areas Separation:** Admins have full control over all Auctions and Listings.
- **Web API:** Enhancing the user experience with dynamic data updates.
- **Responsive and dynamic design:** Real time updates on pages that need to stay up to date.
- **Real-world Data:** Seeded car makes and models and also all cities of Bulgaria to the database.

## Details about some features

- **Search Functionality:** Quickly find car Auctions and Listings based on various criteria such as Car Make, Car Model, City, Engine Type, Description, Title, Total Mileage, Price range etc.
- **User Profiles:** Create listings and auctions, save your favorite listings so you get notified about price changes. You can add Auctions to favorites too! Users can place bids on auctions that have started.
- **Areas separation:** There is an Administrator Area where Admins have full control over all the Auctions and Listings.
- **Web API:** Enhancing the user experience and ensuring that the data presented is always up-to-date.
- **Real-world Car Data:** Seeded to the Database, thanks to [TyresAddict](https://tiresaddict.com/help/databases/cars/)
- **Real-world Cities Data:** The cities of Bulgaria seeded to the Database, thanks to [SimpleMaps](https://simplemaps.com/data/bg-cities)
- **Background Jobs scheduling:** Auctions are ensured to begin at the desired StartTime and then end at the chosen EndTime thanks to background jobs scheduling with Hangfire (the Hangfire dashboard is secured for Admin access only in production). 
- **Real-Time dynamic refreshing:** Real-time refreshing with SignalR for the Auctions ensuring good user experience and instant refresh of pages when a change is being made.
- **Real-Time Bidding system:** Real-Time bids with SignalR for the Auctions, only non-creator Users can bid to an Auction with amounts that meet the rules set by the Creator of the Auction.
- **Email notifications:** Implemented SendGrid for email notifications on new registration and related to the user events in the application - for example, the Creator of the Auction is being notified on Auction End and users are getting notifications when a Listing they had added to their Favorites gets a price decrease by more than 4%.
- **Responsive design:** The application has a responsive design and is working well with different screen sizes.

## User permissions and functionalities

- **Users can:** Create, Edit, Deactivate (SoftDelete) and Delete Listings that they have created.
- **Users can:** Create, Edit and Deactivate (SoftDelete) Auction that they have created. They can Edit and Deactivate Auctions that have not yet started. When Editing a pending Auction end time and start time are always being chose again. Editing started Auction is not allowed, neither is Deactivating.
- **Users can:** Reactivate an Auction if it ended with no bids or they had previously deactivated before its start.
- **Users and Admins can:** Add Listings and Auctions that they have not created to their collections of Favorites. There are to separated collections - one for Listings and one for Auctions - they can be found in the navigation "Моите: -> Обяви / Търгове -> Любими"
- **Users and Admins can:** See their own active and deactivated Listings and Auctions. There are to separated collections - one for Listings and one for Auctions"
- **Users and Admins can:** See their own Expired and Ongoing Auctions.
- **Admins can:** See all the active and deactivated Listings and Auctions in the Admin area and can modify them.
- **Admins can:** See all the Users of the application and their online status.
- **Admins can:** See, Create, Edit and SoftDelete the Categories of the Car features and also see, create, edit and softDelete all features of each Category.
- **Admins can:** Create, Edit, Deactivate (SoftDelete) and Delete any Listing and Create, Edit, Deactivate (SoftDelete) or Delete any Auction at any time (Deactivating an Auction cancels the background jobs for that Auction if it's not started or is active and also removes all the bids that had been placed if there were any). Editing an Auction always requires choosing new start and end times.
- **Admins can:** Reactivate Auction after it ends (deleting all the bids if there were any) or Reactivate Auction that was Deactivated.

## Getting Started

- **Update the secrets.json for both the RadCars web app and the Web API**,
- **Update-Database command**,
- **Run the project!**

## Test credentials

**1. Normal user**
   - Email: user77@gmail.com or Username: tegav_user
      - Password: user69

**2. Admin user**
   - Email: admin77@gmail.com or Username: tegav_admin
      - Password: admin69
  
## Built With

- The architecure of the **[ASP.NET MVC Template by Nikolay Kostov](https://github.com/NikolayIT/ASP.NET-Core-Template/tree/master)**
- **.NET 6 and ASP NET CORE 6**: The backbone of the web application.
- **SQL Server**: For the database
- **Entity Framework Core 6** with LazyLoading: For robust database operations.
- **CloudinaryDotNet**: Images uploading to cloud.
- **Hangfire** with SQL Server: The scheduler providing the application with managing background jobs.
- **SignalR**: For real-time changes and functionalities.
- **AutoMapper**: Efficient object-object mapping.
- **SendGrid**: For sending emails,
- **HTML Sanitizer**: For sanitizing user-generated data before sending it to the database
- **Recaptcha V3**,
- **CsvHelper**: For seeding the database from .CSV files
- **NUnit Testing Framework**: For UnitTesting,
- **Moq**: For mocking dependencies,
- **MoqQueryable.Moq**: For mocking collections for the Repositories,
- **ByteSizeLib**
- **Razor View syntax + Bootstrap 5 + CSS + VanillaJS**,
- **JQuery**,
- **SweetAlert 2**: For alerts to the users,
- **Toastr**: For user notifications on interactions,
- **FlatPickr**: For picking dates

## Current Database diagram
- ![CurrentDbDiagramOfRadCars](https://github.com/RadkoSS/RadCars/assets/94465605/f3317742-9303-4b0c-91e0-db3858f6835d)

## Demonstration with GIFs and pictures

- Auction that has started, visited by logged-in user that is not the creator: 
- ![StartedAuctionVisitByNonCreatorUser](https://github.com/RadkoSS/RadCars/assets/94465605/b3cceedd-8537-4c91-9504-5def0483d4c6)

- Making the first Bid of an Auction:
- ![MakingTheFirstBid](https://github.com/RadkoSS/RadCars/assets/94465605/1e2c9e27-0f3b-4774-b812-b4a51cc4dd79)

- Real-time refresh when someone else makes a Bid on the Auction you are viewing:
- ![WatchingBids](https://github.com/RadkoSS/RadCars/assets/94465605/b3cea2ec-897f-4ea0-91fe-5cc3237de9ff)

- Real-time refresh when someone wins the Auction by placing a Bid with amount higher or equal to the Blitz price (if there is a Blitz price set by the creator):
- ![winnerAnnounceByBlitzPrice](https://github.com/RadkoSS/RadCars/assets/94465605/b4f61a65-dd74-42c5-93df-9623d8df0e84)

- Emails sent to the Creators of the Auctions notifiying them about the outcome of their Auctions:
- ![AuctionEndedEmailWithWinner](https://github.com/RadkoSS/RadCars/assets/94465605/362402c1-4153-4b69-bc52-7469d34533a7)

- ![AuctionEndedEmailWithNoWinner](https://github.com/RadkoSS/RadCars/assets/94465605/a0891d7b-5b0f-45c1-985c-5a7967c32792)

- Emails sent to users about a price decrease for a Listing they have in their favorites:
- ![ListingPriceChangeEmail](https://github.com/RadkoSS/RadCars/assets/94465605/30ec39f9-3a7f-4266-a7b2-e6c704ac3925)

- Add and remove an Auction from favorites
- ![favoriteAndUnfavoriteAuction](https://github.com/RadkoSS/RadCars/assets/94465605/b036fcca-e987-4eb9-9e57-47c124169259)

- Marking an image for deletion when editing an Auction or a Listing:
- ![markingAnImageForDeletionOnEdit](https://github.com/RadkoSS/RadCars/assets/94465605/81d85294-0d45-4196-b0f0-3f3ecaac7e2e)

- Reverting changes when editing an Auction or a Listing (the image "marked" for deletion now will not get deleted after submit):
- ![revertingChangesOnEdit](https://github.com/RadkoSS/RadCars/assets/94465605/bb92c44b-2e65-45b3-923b-cc26da1aa61a)

- Preview uploaded images when creating an Auction or a Listing:
- ![imagePreviewOnUpload](https://github.com/RadkoSS/RadCars/assets/94465605/c50a1bed-a74d-4f4c-9c92-3203619a1437)

- All Listings view for normal user:
- ![AllListings](https://github.com/RadkoSS/RadCars/assets/94465605/0e143fca-2074-4aa1-95af-450d8d22a69a)

- All Auctions view for normal user:
- ![AllAuctions](https://github.com/RadkoSS/RadCars/assets/94465605/c6bf5592-c102-4a41-9981-29d0de15d253)

- All my active Listings:
- ![AllMyActiveListings](https://github.com/RadkoSS/RadCars/assets/94465605/0253c52e-e261-486e-a107-46815380fe94)

- All my deactivated Listings:
- ![AllMyDeactivateListings](https://github.com/RadkoSS/RadCars/assets/94465605/a522f273-7c69-49d4-a8f4-39ec98303a37)

- All my favorite Listings:
- ![AllFavoriteListings](https://github.com/RadkoSS/RadCars/assets/94465605/bd215806-9bd5-4ad9-b35a-4a31e1760432)

- All my active Auctions:
- ![AllMyActiveAuctions](https://github.com/RadkoSS/RadCars/assets/94465605/951bce55-1d06-4cd2-8cc4-b61721bf6725)

- All my ended Auctions:
- ![AllMyEndedAuctions](https://github.com/RadkoSS/RadCars/assets/94465605/a9a7505b-23bd-48c9-8349-2d80e3eed804)

- Admin dashboard:
- ![AdminIndexPage](https://github.com/RadkoSS/RadCars/assets/94465605/8e12f0ff-5d45-4221-8ad2-8ab3459c37a2)
- ![AdminAllDeactivatedListings](https://github.com/RadkoSS/RadCars/assets/94465605/cf167af6-afac-4201-a01e-2d9d6d8366c0)
- ![AdminAllDeactivatedAuctions](https://github.com/RadkoSS/RadCars/assets/94465605/5a3c9c12-f328-4338-b5ec-de40d2da586e)
- ![AdminAllUsersLists](https://github.com/RadkoSS/RadCars/assets/94465605/ded60e3c-4039-458e-83e4-0e1624b3c836)
- ![AdminAllFeatureCategories](https://github.com/RadkoSS/RadCars/assets/94465605/403e549d-cf80-4b26-b5ac-1527d6ab40e0)
- ![AdminAllFeaturesOfCategory](https://github.com/RadkoSS/RadCars/assets/94465605/c3ca0b79-3721-47f1-81a8-355741f7dea4)

- Hangfire dashboard (in Production environment the dashboard is visible only to the Admins):
- ![HangfireDashboard](https://github.com/RadkoSS/RadCars/assets/94465605/1ac29e81-aed0-4e2b-826d-67dc9a70f091)

## Contributing
🤝 I welcome contributions from the community! If you'd like to contribute, please fork the repository and create a pull request. For major changes, open an issue first to discuss your proposed changes.

## License
📄 RadCars is licensed under the MIT License. See the LICENSE.md file for more details.

## Special Thanks
🙏 A heartfelt appreciation to SoftUni and all the lecturers for their invaluable guidance and support throughout the learning journey that led to the creation of this project.

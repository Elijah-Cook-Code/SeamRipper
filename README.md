# SeamRipper
SeamRipper

# Please use the refactor-api-branch for my capstone
(I didnt merge it because I want to revisit the main branch later)

# SETUP INSTRUCTIONS (api version)

1) download the refactor-api-integration branch  

2) open the brach/sln in vs code  

3) open the package manager console in vs  

4) cd to the directory that contains the SeamRipperAPI project  

5) once there run "dotnet run"- (leave running)  

6) should build and launch the api-  

7) it should create a local host, something like this in the package manager console "http://Localhost:5047/"  

8) copy that  

9) then go to the "SeamRipper" Project in the sln, navigate to the mauiprogram.cs file  

10) go to line 32 in the code or around there and find this line  

-----builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5047/") });  ---

replace the local host with yours if a different one was created, if "http://Localhost:5047/" was created for  
your local host as well no need to change this in the maui-program  

11) make sure your api project is still running in the background/pmc  

12) next set "SeamRipper" in sln to main project/ or check to make sure it is  

13) run it  

14) the program should launch and use the api!

# notes:
    
if the above works fine, you can repeat this everytime to run the application, but since/if the above worked you should 
be able to do a dual start in vs, just change the config start up projects to run  
---------SeamRipper    
---------SeamRipperAPI  
once you do that you should be able to run it without doing the above everytime. That is up to you!

SIDE NOTES:
The delete button on the client measurements page just clears the measurements and resets them to zero which is what I
intended, it just may throw you off, Im going to change the icon to make that more clear 

# SETUP PROJECT MAIN BRANCH (this is the offline version)
  -You should be able to just download this branch and run it  
  -should work 

# trouble shooting: (things to try if you have issues)  
------delete the old db and repeat the process above  
DB location: (will also print in the output)  
📂 Database Path: C:\Users\Eli\AppData\Local\Packages\com.companyname.seamripper_9zz4h110yvjzm\LocalState\SeamRipper.db  

    

## 1. Project Overview
  
Overview
  "At The Seams" is a client measurement and tracking system designed for tailoring and alterations. The application allows tailors and seamstresses to store, retrieve, 
  and manage client information along with their associated measurements. It is built using Blazor (MAUI Hybrid) with MudBlazor, and it leverages SQLite as the database.

Problem & Solution
  Tailors often struggle to keep track of client measurements efficiently, relying on paper records or scattered digital notes. At The Seams provides a centralized, 
  user-friendly interface where client details and their corresponding measurements are stored securely, ensuring quick access, easy updates, and reliable record-keeping.

Technical Stack
    Framework: .NET MAUI Blazor Hybrid
    UI Library: MudBlazor (for better UI components)
    Database: SQLite (Entity Framework Core)
    Backend Logic: C# with Dependency Injection for service-based architecture
    Design Pattern: Model-View-Component (MVC)

Core Features

Client Management
    Add, update, delete client information.
    Store name, notes, and date of registration.
    Associate multiple measurements with a client.

Measurement Tracking
    Each client can have multiple measurements stored.
    Fields include chest, waist, trouser length, sleeve length, and more.

Database Integration (CRUD Operations)
    Uses Entity Framework Core (SQLite) to manage client records.
    Clients and their corresponding measurements are stored, retrieved, and updated efficiently.
    Foreign key relationships ensure that deleting a client removes associated measurements.

Blazor Hybrid UI
    A modern and responsive UI with MudBlazor components.
    Provides easy navigation and a clean layout for managing client records.

Asynchronous Operations
    All database interactions are async to improve performance.
    Avoids blocking the UI by using async/await with Entity Framework Core.

- **Future Goals:**  
  - **Goal 2:** Use information in DB to expedite drafting, by performing calculations on measurements from the DB
  - **Goal 3:** Create Garment object based on selected Client, by parsing the Clients data, taking the required measurements for the Garment(object) requested based on its requirements  

### API Integration (Refactor Branch)

- **API Usage:**  
  The `refactor-api-integration` branch relies fully on a self-hosted Web API for all client and measurement operations. This ensures clear separation of concerns between UI, business logic, and data access.

- **Main vs. API Branches:**  
  - `main`: local service logic only (offline capable)
  - `refactor-api-integration`: API-driven (recommended for real-world use)

- **API Details:**
  - CRUD operations for both `ClientInfo` and `ClientMeasurements` are routed via HTTP calls.
  - API hosted using ASP.NET Core Web API.
  - All logic moved into `ClientRepository` and `ClientsController.cs`.

---

### Database Interaction

**Database Setup (EF Core)**
- Managed via `ClientContext.cs`
- Two main models:
  - `ClientInfo`: basic client data
  - `ClientMeasurements`: measurement records linked by `ClientId`

**One-to-Many Relationship:**  
Each `ClientInfo` can have one or more `ClientMeasurements`.

**Automatic Migrations:**  
DB schema updates run on app startup via `MauiProgram.cs`

---

### UI (Blazor Hybrid w/ MudBlazor)

- **Clients.razor**
  - Add/edit/delete/search clients
  - Export all/selected as CSV
  - Generate random clients (via API)

- **ClientMeasurements.razor**
  - Edit & clear measurements per client
  - Delete selected measurements and/or clients
  - Export all/selected as CSV
  - Generate random clients and refresh UI

---

### Dependency Injection & Startup

- All services (API & logic) registered in `MauiProgram.cs`
- Uses `HttpClient` to call external API endpoints
- UI uses MudBlazor for responsive, material-style layout
- Hosted API endpoints are centralized in the logic layer

---

### Core Functionality

All of these now use the Web API (in the refactor branch):
```
GetClientsAsync()
GetClientMeasurementsAsync()
AddClientAsync()
UpdateClientAsync()
DeleteClientAsync()
ClearClientMeasurementAsync()
GenerateRandomClientsAsync()
ExportClientsAsCsvAsync()
...among others
```

---

### Summary
This project has been upgraded from a local-only data model to a fully API-backed architecture. Users can manage clients and their measurements, and the app supports API-driven features like search, batch deletion, and CSV export. The architecture is modular, clean, and scalable


## 7. Capstone Features List

Choose **at least 3** features from the list below and describe how you have implemented them in your project:

- [?] **Unit Tests:** Create 3 or more unit tests for your application.
- [ ] **Regex Validation:** Implement a regular expression to validate or ensure a field is always stored/displayed in the correct format.
- [ ] **Dictionary/List Usage:** Create a dictionary or list, populate it with values, and retrieve at least one value for use in your program.
- [Y] **Data Writing:** Write information/data to a text file (e.g., log files, configuration files, CSV export).
- [ ] **SOLID Principles Comments:** Add comments in your code explaining how you use at least 2 SOLID principles.
- [ ] **Generic Class:** Create a generic class and use it within your application.
- [Y] **API Endpoint:** Make your application an API with an endpoint (e.g., allow users to add a product via a POST request).
- [Y] **CRUD API:** Implement a CRUD API in your application.
- [Y] **Asynchronous Functionality:** Make your application asynchronous.
- [Y] **Entity Join:** Use 2 or more related tables in your database and create a function that returns data from both (akin to a join).
  
----

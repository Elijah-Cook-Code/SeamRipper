# SeamRipper
SeamRipper

# ["At The Seams"]

## REQUIRES THE REPO ATTHESEAMS.DATA
  https://github.com/Elijah-Cook-Code/AtTheSeams.Data.git
  (this contains DB, DATA MODELS, LOGIC, EF CORE + MIGRATIONS)
  The required repo above needs to be a project in the sln that if referenced by ATTHESEAMS main repo
  (looking for the easiest way to get it in the soulution without breaking the program)

  this is maybe the 3rd time starting this project other repos on my github have been previous attempts as well!

## 1. Project Overview

- **Description:**
  
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
    Supports data validation to prevent incorrect entries.

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

- **Key Features:**  
  - **Feature 1:** Robust CRUD Database
  - **Feature 2:** Use information in DB to expedite drafting, by performing calculations on measurements from the DB
  - **Feature 3:** Create Garment object based on selected Client, by parsing the Clients data, taking the required measurements for the Garment(object) requested based on its requirements  
  - **Feature 2:** Entity Relationship (One-to-Many): Clients and measurements are linked via foreign key (ClientId), allowing efficient data retrieval.
  - **Feature 3:** Asynchronous Operations: All database interactions are async, ensuring smooth UI responsiveness.

--- 

## 5. Visual Appeal

- **Design & UX:**  
  -atm more concered with building the skeleton of the program then looking to focus more on this at a later date, I do have some ideas for it. I'd like it to present the different tables, client info, client measurements. The allow the user to interact with them as needed and maybe in the future have it create a table that will create garments based on the clients information and put that into a new table 

- **Inspiration:**
    ??


---

## Software Project Requirements

### Application Structure
 
Project Structure
    Components/Layout → Contains layout files (MainLayout.razor, NavMenu.razor).
    Components/Pages → Includes UI pages like Clients.razor, ClientMeasurements.razor.
    Data/Connections → Database context (ClientContext.cs).
    Data/Models → Defines database models (ClientInfo.cs, ClientMeasurements.cs).
    Data/Services → Business logic (ClientLogic.cs).
    MauiProgram.cs → Application startup, dependency injection, and database initialization.

### API Integration

- **API Usage:**  
    -looking for a good idea to incorperate to this one, possibly something relating to image storage for clients 
- **Integration Details:**  


### Database Interaction

Implementation Details

Database Setup & ORM (Entity Framework Core)
    The database is defined in ClientContext.cs​

Two models:
    -ClientInfo.cs (Holds client details)​
    -ClientMeasurements.cs (Stores measurement data, linked via ClientId)​
Relationship:
      One ClientInfo can have multiple ClientMeasurements (One-to-Many).

CRUD Operations (Client & Measurements)
    All CRUD operations are handled inside ClientLogic.cs​
Functions include:
        GetClientsAsync()
        GetAllMeasurementsAsync()
        AddClientAsync()
        UpdateClientAsync()
        DeleteClientAsync()
        AddClientMeasurementAsync()
        DeleteClientMeasurementAsync()

UI (Blazor & MudBlazor)
    The Clients.razor page allows users to view and manage clients.
    The ClientMeasurements.razor page enables input and editing of measurements.

Dependency Injection & Startup
    -Services are registered in MauiProgram.cs​
    -Database migration automatically runs at startup.
    -MudBlazor UI components are enabled for enhanced UI.

### 6.4 Functions/Methods
        
        GetClientsAsync()
        GetAllMeasurementsAsync()
        AddClientAsync()
        UpdateClientAsync()
        DeleteClientAsync()
        AddClientMeasurementAsync()
        DeleteClientMeasurementAsync()

## 7. Capstone Features List

Choose **at least 3** features from the list below and describe how you have implemented them in your project:

- [?] **Unit Tests:** Create 3 or more unit tests for your application.
- [ ] **Regex Validation:** Implement a regular expression to validate or ensure a field is always stored/displayed in the correct format.
- [ ] **Dictionary/List Usage:** Create a dictionary or list, populate it with values, and retrieve at least one value for use in your program.
- [?] **Data Writing:** Write information/data to a text file (e.g., log files, configuration files, CSV export).
- [ ] **SOLID Principles Comments:** Add comments in your code explaining how you use at least 2 SOLID principles.
- [ ] **Generic Class:** Create a generic class and use it within your application.
- [ ] **API Endpoint:** Make your application an API with an endpoint (e.g., allow users to add a product via a POST request).
- [Y] **CRUD API:** Implement a CRUD API in your application.
- [Y] **Asynchronous Functionality:** Make your application asynchronous.
- [Y] **Entity Join:** Use 2 or more related tables in your database and create a function that returns data from both (akin to a join).

---

## 8. Additional Notes and Mentor Feedback

- **Questions/Concerns:**  


- **Feedback Section:**  

---

## 9. Submission Instructions

- **Deadline:**  
  Remember to complete and submit the provided Google Form by **Midnight EST on Sunday 2/23/25**.
  
- **Final Check:**  
  Ensure every requirement is met before submission.


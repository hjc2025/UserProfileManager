# Modern WinForms User Profile Manager

This project is a demonstration of how to build a classic .NET Windows Forms application using modern software architecture and development practices. It serves as a blueprint for creating testable, maintainable, and scalable desktop applications.

The sample application allows a user to load a user profile, edit it, and save it in different formats (JSON or XML). While simple in function, it is built upon a robust architectural foundation.

## Core Technologies & Patterns

This project intentionally combines classic and modern .NET technologies to showcase a powerful, hybrid approach:

-   **Framework:** .NET 8 & Windows Forms
-   **Language:** C# 12
-   **Architecture:** **Model-View-Presenter (MVP)**
-   **Dependency Injection:** `Microsoft.Extensions.Hosting` (Generic Host)
-   **Configuration:** `Microsoft.Extensions.Configuration` (via `appsettings.json`)
-   **Design Patterns:**
    -   **Strategy Pattern:** For dynamically selecting the save format.
    -   **Factory Pattern:** To provide the correct saving strategy.
    -   **Repository Pattern:** To abstract data access.

## Why This Architecture?

Traditional WinForms development often leads to "Code-Behind" spaghetti code, where business logic, data access, and UI manipulation are tightly coupled in form event handlers. This makes the application difficult to test, maintain, and evolve.

This project's architecture solves these problems:

1.  **Testability:** The **Presenter** contains all application logic and has no reference to `System.Windows.Forms`. It can be fully unit-tested by mocking its dependencies (`IProfileView`, `IUserProfileRepository`, etc.).
2.  **Separation of Concerns (SoC):** Each component has a single, well-defined responsibility:
    -   **View (`ProfileForm`):** "Dumb" component. Its only job is to display data and forward user actions (clicks) to the Presenter via events.
    -   **Presenter (`ProfilePresenter`):** The "brains." It orchestrates all actions, processes data, and tells the View what to display.
    -   **Model (`UserProfile`):** A simple data container (POCO).
    -   **Services (`IUserProfileRepository`, etc.):** Handle cross-cutting concerns like data access and notifications.
3.  **Flexibility & Extensibility:**
    -   Need to add a CSV saving option? Just create a `CsvSavingStrategy` class and register it in `Program.cs`. No other code needs to change.
    -   Need to fetch data from a SQL database instead of memory? Create a `SqlUserProfileRepository`, change one line in `Program.cs`, and the application works without modification to the UI or Presenter.

## Project Structure

The solution is organized into a clear folder structure that reflects the architectural separation.

```
UserProfileManager/
│
├── appsettings.json            // Application configuration (e.g., save paths)
├── Program.cs                  // The "Composition Root": DI setup and application startup
│
├── Models/
│   └── UserProfile.cs          // The core data object (POCO)
│
├── Views/
│   ├── IProfileView.cs         // The View's contract/interface
│   └── ProfileForm.cs          // The WinForms Form implementing the View
│
├── Presenters/
│   └── ProfilePresenter.cs     // The application logic orchestrator
│
├── Services/
│   ├── INotificationService.cs // Abstraction for showing messages
│   ├── IUserProfileRepository.cs // Abstraction for data access
│   └── Implementations/
│       ├── MessageBoxNotificationService.cs
│       └── InMemoryUserProfileRepository.cs // "Fake" data source
│
└── Strategies/
    ├── ISavingStrategy.cs      // The Strategy Pattern interface
    ├── SavingStrategyFactory.cs// A factory to provide the correct strategy
    └── Implementations/
        ├── JsonSavingStrategy.cs
        └── XmlSavingStrategy.cs
```

## How It Works: The Flow of Control

The application's startup and interaction flow are managed by the Dependency Injection container.

#### Startup (`Program.cs`)

1.  A **Generic Host** is created, which sets up the DI container (`IServiceProvider`) and configuration.
2.  All services, strategies, the Presenter, and the View are registered with the container as singletons. Crucially, the `ProfileForm` is registered as the concrete implementation for the `IProfileView` interface.
3.  The DI container is asked to resolve `ProfilePresenter`. This is the key activation step:
    -   The container creates a singleton `ProfileForm` instance to satisfy the `IProfileView` dependency for the presenter.
    -   The `ProfilePresenter`'s constructor runs, receiving all its dependencies and subscribing its methods to the `ProfileForm`'s events.
4.  The DI container is then asked to resolve the `ProfileForm` (retrieving the same instance) and `Application.Run()` starts the UI.

#### User Interaction (Example: Clicking "Save")

1.  The user clicks the `btnSave` button on the `ProfileForm`.
2.  The `ProfileForm`'s `btnSave_Click` handler fires, which in turn invokes the `SaveClicked` event defined on the `IProfileView` interface.
3.  The `ProfilePresenter`, which subscribed to this event at startup, executes its `OnSaveClicked` method.
4.  The Presenter gets the latest data from the View (e.g., `_view.UserName`).
5.  It performs validation. If invalid, it calls `_view.ShowMessage(...)`.
6.  It asks the `SavingStrategyFactory` for the correct strategy based on the `_view.SelectedSaveFormat`.
7.  It executes the strategy: `strategy.Save(...)`.
8.  Upon success, it calls the `INotificationService` to show a confirmation message.

## How to Build and Run

1.  **Prerequisites:**
    -   Visual Studio 2022
    -   .NET 8 SDK
2.  **Clone the repository:**
    ```bash
    git clone <your-repo-url>
    ```
3.  **Open the Solution:** Open `UserProfileManager.sln` in Visual Studio.
4.  **Restore Dependencies:** The NuGet packages should restore automatically. If not, right-click the solution and select "Restore NuGet Packages".
5.  **Run the Application:** Press **F5** or click the "Start" button in Visual Studio.

### Modifying Configuration

The default save path is configured in `appsettings.json`.

```json
{
  "Settings": {
    "DefaultSavePath": "user_profile"
  }
}
```

This will save files as `user_profile.json` or `user_profile.xml` in the executable's directory (`bin/Debug/...`). You can change this value to a full path if desired. Ensure the `appsettings.json` file's properties are set to **"Copy to Output Directory: Copy if newer"**.
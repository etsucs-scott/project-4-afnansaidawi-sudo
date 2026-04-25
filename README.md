[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/qJo95Bxr)

# Music Practice Scheduler - CSCI 1260 Project 4

A simple Blazor WebAssembly application that helps musicians organize and track their music practice sessions. Built as a beginner-friendly project demonstrating OOP, data structures, file I/O, and exception handling.

## About This Project

This is **Project 4: Final Project** for CSCI 1260. The application allows users to:
- Manage a collection of songs they are learning
- Organize songs by genre
- Schedule daily practice sessions
- Track recently practiced songs
- Save and load practice data

## Tech Stack

- **Framework**: Blazor WebAssembly (ASP.NET Core)
- **.NET Version**: .NET 10 (or .NET 8+)
- **Language**: C#
- **Testing**: xUnit (15+ unit tests)
- **Data Serialization**: System.Text.Json (built-in)
- **Storage**: Browser localStorage (via JavaScript)

## Data Structures Used

The application demonstrates all required data structures:

1. **Dictionary<string, Song>** - Stores songs by name for fast lookup
2. **HashSet<string>** - Stores unique music genres (prevents duplicates automatically)
3. **Queue<PracticeSession>** - Manages upcoming practice sessions in FIFO order
4. **Stack<Song>** - Tracks recently practiced songs in LIFO order

## Project Structure

```
MusicPracticeScheduler.App/
├── Models/
│   ├── Song.cs                    # Represents a song
│   ├── PracticeSession.cs         # Represents a practice session
│   └── MusicScheduler.cs          # Core logic class (Dictionary, HashSet, Queue, Stack)
├── Services/
│   └── LocalStorageService.cs     # JSON serialization/deserialization
├── Components/
│   ├── Pages/
│   │   ├── Home.razor            # Main landing page
│   │   ├── Songs.razor           # Song management page
│   │   └── Schedule.razor        # Practice scheduling page
│   └── Layout/
└── wwwroot/                       # Static files

MusicPracticeScheduler.Tests/
├── UnitTest1.cs                   # Contains all unit tests (15 tests)
```

## Build and Run

### Prerequisites
- .NET SDK 8.0 or higher installed

### Build the Project
```bash
cd project-4-afnansaidawi-sudo
dotnet build
```

### Run the Application
```bash
dotnet run --project MusicPracticeScheduler.App
```

The application will start on `http://localhost:5001` (or the configured port). Open this URL in your web browser.

### Run Unit Tests
```bash
dotnet test MusicPracticeScheduler.Tests
```

All 15 unit tests should pass with no failures.

## Features

### Songs Management
- Add new songs with name, artist, genre, and duration
- View all songs in a table
- Remove songs from the collection
- View unique genres
- Mark songs as completed
- **Load 20 sample songs** for quick testing and demonstration

### Practice Scheduling
- Schedule practice sessions for any song
- View upcoming practice sessions (Queue order - FIFO)
- Complete practice sessions
- Add notes to practice sessions

### Recently Practiced
- View recently practiced songs (Stack order - LIFO)
- Track which songs were practiced most recently

### Sample Data
The application includes a "Load Sample Data" button that adds 20 popular songs across different genres:
- **Rock**: Imagine, Bohemian Rhapsody, Hotel California, Stairway to Heaven, Dream On
- **Pop**: Billie Jean, Thriller, Bad, Beat It, Smooth Criminal, Shape of You, Thinking Out Loud, Photograph, Castle on the Hill, Perfect
- **Classic**: Yesterday, Let It Be, Hey Jude, Twist and Shout, All You Need Is Love

This feature helps demonstrate the application functionality and provides sample data for testing.

## Unit Tests

The project includes 16 comprehensive unit tests covering:

1. Adding songs successfully
2. Validation for empty song names
3. Duplicate song name prevention
4. Removing songs
5. Retrieving all songs
6. Filtering songs by genre
7. Genre deduplication (HashSet functionality)
8. Scheduling practice sessions
9. Session validation
10. Completing sessions
11. Queue order (FIFO) verification
12. Stack order (LIFO) verification
13. Dictionary lookup
14. Exception handling
15. Total song count
16. Loading default sample songs

Run all tests:
```bash
dotnet test
```

## User Interface

The UI is simple and beginner-friendly:
- Basic HTML tables and forms
- No external CSS frameworks
- Simple input validation
- Error and success messages
- Navigation between pages

### Pages

1. **Home** - Welcome page with quick stats
2. **Songs** - Add/manage songs and view genres
3. **Schedule** - Schedule practice sessions and view upcoming/recent songs

## Exception Handling

The application includes error handling for:
- Empty or null song names
- Duplicate song names
- Invalid durations (must be > 0)
- Non-existent songs when scheduling
- JSON serialization/deserialization errors

All errors are caught and displayed as user-friendly messages.

## Data Persistence

Data is serialized to JSON using `System.Text.Json` and can be saved/loaded via browser localStorage. The `LocalStorageService` class handles:
- Converting scheduler data to JSON format
- Converting JSON back to scheduler objects
- Error handling during serialization

## Design Decisions

1. **No Dependency Injection** - Kept simple for beginner project
2. **In-Memory Data Structures** - All data stored in memory during runtime
3. **Simple UI** - No CSS frameworks or advanced styling
4. **JSON Serialization** - Using built-in System.Text.Json, no external libraries
5. **Direct Service Instantiation** - Services created directly in Razor components

## How to Use

1. **Start the app** - Run `dotnet run --project MusicPracticeScheduler.App`
2. **Add songs** - Go to Songs page, fill in song details, click Add Song
3. **Schedule sessions** - Go to Schedule page, select a song, add duration and notes, click Schedule
4. **Complete sessions** - Click "Complete" button on upcoming sessions
5. **View results** - See recently practiced songs and all your songs

## External Resources and Citations

- [Microsoft Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [System.Text.Json Documentation](https://docs.microsoft.com/en-us/dotnet/api/system.text.json)
- [xUnit Documentation](https://xunit.net/)
- [C# Collections Documentation](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic)

## Class Diagram

A UML diagram showing all classes and their relationships is included as `MusicPracticeScheduler-UML.png` in the repository.

Classes shown:
- `Song` - Properties: Name, Artist, Genre, Duration, IsCompleted
- `PracticeSession` - Properties: SongName, Date, Duration, Notes
- `MusicScheduler` - Main class with Dictionary, HashSet, Queue, Stack
- `LocalStorageService` - Handles JSON serialization

## Submission Information

This project is submitted for CSCI 1260 - Project 4: Final Project via GitHub Classroom.

Repository: https://github.com/etsucs-scott/project-4-afnansaidawi

All code is original student work, demonstrating understanding of:
- Object-Oriented Programming (OOP)
- Data Structures (Dictionary, HashSet, Queue, Stack)
- Exception Handling
- File I/O and Data Persistence
- Unit Testing
- Web UI with Blazor

## Code Comments

- Every class has a comment explaining its purpose
- Every method has a comment explaining what it does
- Complex logic includes inline comments
- All comments are in English


# MusicPracticeScheduler - UML Class Diagram

## Class Structure and Relationships

### Song Class
```
Class: Song
├── Properties:
│   ├── Name: string
│   ├── Artist: string
│   ├── Genre: string
│   ├── DurationMinutes: int
│   └── IsCompleted: bool
├── Constructors:
│   ├── Song()                                    // Empty constructor
│   └── Song(name, artist, genre, duration)      // Full constructor
```

**Responsibility**: Represents a song that a musician is learning.

---

### PracticeSession Class
```
Class: PracticeSession
├── Properties:
│   ├── SongName: string
│   ├── Date: DateTime
│   ├── DurationMinutes: int
│   └── Notes: string
├── Constructors:
│   ├── PracticeSession()                        // Empty constructor
│   └── PracticeSession(songName, duration, notes) // Full constructor
```

**Responsibility**: Represents a single practice session for a song.

---

### MusicScheduler Class (Core Logic)
```
Class: MusicScheduler
├── Private Fields:
│   ├── songs: Dictionary<string, Song>          // Fast song lookup by name
│   ├── genres: HashSet<string>                  // Unique genres (no duplicates)
│   ├── upcomingSessions: Queue<PracticeSession> // FIFO - next sessions to do
│   └── recentlyPracticed: Stack<Song>           // LIFO - recently done songs
├── Public Methods:
│   ├── AddSong(name, artist, genre, duration): Song
│   ├── RemoveSong(name): bool
│   ├── GetAllSongs(): List<Song>
│   ├── GetSongsByGenre(genre): List<Song>
│   ├── GetAllGenres(): List<string>
│   ├── ScheduleSession(songName, duration, notes): void
│   ├── GetUpcomingSessions(): List<PracticeSession>
│   ├── CompleteNextSession(): PracticeSession
│   ├── GetRecentlyPracticed(): List<Song>
│   ├── MarkSongComplete(songName): void
│   ├── SongExists(songName): bool
│   ├── GetTotalSongs(): int
│   └── Clear(): void
```

**Responsibility**: Manages all songs, genres, and practice sessions using appropriate data structures.

**Data Structures Used**:
- **Dictionary<string, Song>**: Provides O(1) lookup of songs by name
- **HashSet<string>**: Automatically prevents duplicate genres
- **Queue<PracticeSession>**: FIFO order - first scheduled session is first to complete
- **Stack<Song>**: LIFO order - most recently practiced song is on top

---

### LocalStorageService Class
```
Class: LocalStorageService
├── Inner Class: SchedulerData
│   ├── Songs: List<Song>
│   ├── Sessions: List<PracticeSession>
│   └── Genres: List<string>
├── Public Methods:
│   ├── SerializeScheduler(scheduler): string
│   └── DeserializeScheduler(json): MusicScheduler
├── Private Fields:
│   └── StorageKey: const string = "musicSchedulerData"
```

**Responsibility**: Converts scheduler data to/from JSON for persistence.

---

## Relationships

### Data Flow Diagram
```
User Interface (Razor Pages)
    │
    ├─→ Songs.razor       ←→ MusicScheduler (manage songs)
    │
    ├─→ Schedule.razor    ←→ MusicScheduler (manage sessions)
    │
    └─→ Home.razor        ←→ MusicScheduler (display stats)
    
    
Persistence Flow:
    MusicScheduler ←→ LocalStorageService ←→ Browser localStorage
```

### Class Dependencies

```
Razor Pages (Songs.razor, Schedule.razor)
    ↓ (creates and uses)
    MusicScheduler
    ├─ (contains)
    ├─→ Song (in Dictionary, Stack)
    ├─→ PracticeSession (in Queue)
    └─→ LocalStorageService
        └─→ JSON (System.Text.Json)
```

---

## Design Principles Used

1. **Single Responsibility**: Each class has one main job
   - Song: represents song data
   - PracticeSession: represents a practice session
   - MusicScheduler: manages songs and sessions
   - LocalStorageService: handles JSON serialization

2. **Encapsulation**: Data structures are private, accessed through public methods

3. **Exception Handling**: Invalid operations throw meaningful exceptions
   - Empty song names
   - Duplicate songs
   - Non-existent songs
   - Invalid durations

4. **Simple Design**: No complex patterns, easy to understand for beginners

---

## How Data Flows Through the System

### Adding a Song
```
User input (Songs.razor)
    ↓
AddSong() method in MusicScheduler
    ↓
Dictionary updated (fast lookup)
    ↓
HashSet updated (genre tracking)
    ↓
Display updated in UI
```

### Scheduling a Practice Session
```
User input (Schedule.razor)
    ↓
ScheduleSession() method in MusicScheduler
    ↓
Queue updated (FIFO order)
    ↓
Display updated in UI
```

### Completing a Practice Session
```
User clicks "Complete"
    ↓
CompleteNextSession() method in MusicScheduler
    ↓
Queue dequeued (removes first item)
    ↓
Song added to Stack (recently practiced)
    ↓
Display updated in UI
```

---

## Data Structure Verification

### Dictionary<string, Song>
- **Usage**: `songs[name] = newSong`
- **Benefit**: O(1) lookup time for songs
- **Test**: TestSongExists_Dictionary

### HashSet<string>
- **Usage**: `genres.Add(genre)` - automatically prevents duplicates
- **Benefit**: Fast genre uniqueness checking
- **Test**: TestGetAllGenres_NoDuplicates

### Queue<PracticeSession>
- **Usage**: `upcomingSessions.Enqueue()` and `Dequeue()`
- **Benefit**: FIFO order - fair scheduling (first session first)
- **Test**: TestUpcomingSessions_QueueOrder

### Stack<Song>
- **Usage**: `recentlyPracticed.Push()` and access via ToList()
- **Benefit**: LIFO order - recently practiced songs are easily accessible
- **Test**: TestRecentlyPracticed_StackOrder

---

## Total Lines of Code by Component

- Song.cs: ~30 lines
- PracticeSession.cs: ~30 lines
- MusicScheduler.cs: ~180 lines
- LocalStorageService.cs: ~70 lines
- Unit Tests: ~400 lines
- Razor Components: ~300 lines
- **Total: ~1010 lines of beginner-level code**

---

## Beginner Code Characteristics

✓ Simple class design (no abstract classes, interfaces, or generics)
✓ Clear method names that describe what they do
✓ Comments on every class and method
✓ Basic exception handling with try-catch
✓ Straightforward logic flow
✓ No advanced C# features
✓ No design patterns or architectural complexity
✓ Easy to follow and understand

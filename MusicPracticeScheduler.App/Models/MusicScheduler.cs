using System.Collections.Generic;
using System.Linq;

// MusicScheduler class - main class that manages songs and practice sessions
// Uses Dictionary, HashSet, Queue, and Stack for different operations
public class MusicScheduler
{
    // Dictionary to store songs by name - for fast lookup
    private Dictionary<string, Song> songs;
    
    // HashSet to store unique genres - prevents duplicates automatically
    private HashSet<string> genres;
    
    // Queue to store practice sessions in order (FIFO - first in first out)
    private Queue<PracticeSession> upcomingSessions;
    
    // Stack to store recently practiced songs (LIFO - last in first out)
    private Stack<Song> recentlyPracticed;

    // Constructor - initializes all data structures
    public MusicScheduler()
    {
        songs = new Dictionary<string, Song>();
        genres = new HashSet<string>();
        upcomingSessions = new Queue<PracticeSession>();
        recentlyPracticed = new Stack<Song>();
    }

    // Add a new song to the scheduler
    // Returns the song if successful, throws exception if song name is empty or already exists
    public Song AddSong(string name, string artist, string genre, int duration)
    {
        // Check if name is empty
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Song name cannot be empty");
        }

        // Check if song already exists
        if (songs.ContainsKey(name))
        {
            throw new Exception("Song already exists");
        }

        // Create new song
        Song newSong = new Song(name, artist, genre, duration);

        // Add to dictionary
        songs[name] = newSong;

        // Add genre to HashSet (automatically prevents duplicates)
        genres.Add(genre);

        return newSong;
    }

    // Remove a song by name
    // Returns true if removed, false if song not found
    public bool RemoveSong(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return songs.Remove(name);
    }

    // Get all songs as a list
    // Returns empty list if no songs
    public List<Song> GetAllSongs()
    {
        return songs.Values.ToList();
    }

    // Get all songs for a specific genre
    // Returns empty list if no songs found for that genre
    public List<Song> GetSongsByGenre(string genre)
    {
        if (string.IsNullOrWhiteSpace(genre))
        {
            return new List<Song>();
        }

        return songs.Values
            .Where(s => s.Genre == genre)
            .ToList();
    }

    // Get all genres as a list
    public List<string> GetAllGenres()
    {
        return genres.ToList();
    }

    // Schedule a new practice session
    // Adds to the queue for upcoming sessions
    public void ScheduleSession(string songName, int duration, string notes)
    {
        if (string.IsNullOrWhiteSpace(songName))
        {
            throw new Exception("Song name cannot be empty");
        }

        if (!songs.ContainsKey(songName))
        {
            throw new Exception("Song not found");
        }

        PracticeSession session = new PracticeSession(songName, duration, notes);
        upcomingSessions.Enqueue(session);
    }

    // Get all upcoming sessions as a list
    // Queue doesn't have a direct way to get all items, so we convert to list
    public List<PracticeSession> GetUpcomingSessions()
    {
        return upcomingSessions.ToList();
    }

    // Complete the next scheduled session
    // Removes from queue and adds song to recently practiced stack
    public PracticeSession CompleteNextSession()
    {
        if (upcomingSessions.Count == 0)
        {
            throw new Exception("No upcoming sessions");
        }

        PracticeSession session = upcomingSessions.Dequeue();

        // Add song to recently practiced stack
        if (songs.ContainsKey(session.SongName))
        {
            recentlyPracticed.Push(songs[session.SongName]);
        }

        return session;
    }

    // Get recently practiced songs as a list
    // Stack doesn't have a direct way to get all items, so we convert to list
    public List<Song> GetRecentlyPracticed()
    {
        return recentlyPracticed.ToList();
    }

    // Mark a song as completed
    public void MarkSongComplete(string songName)
    {
        if (string.IsNullOrWhiteSpace(songName))
        {
            throw new Exception("Song name cannot be empty");
        }

        if (!songs.ContainsKey(songName))
        {
            throw new Exception("Song not found");
        }

        songs[songName].IsCompleted = true;
    }

    // Check if a song exists
    public bool SongExists(string songName)
    {
        return songs.ContainsKey(songName);
    }

    // Get total number of songs
    public int GetTotalSongs()
    {
        return songs.Count;
    }

    // Clear all data (useful for testing)
    public void Clear()
    {
        songs.Clear();
        genres.Clear();
        upcomingSessions.Clear();
        recentlyPracticed.Clear();
    }

    // Load default songs for demonstration
    // This method adds 20 sample songs to get started
    public void LoadDefaultSongs()
    {
        // Clear existing data first
        Clear();

        // Add 20 sample songs with different genres
        AddSong("Imagine", "John Lennon", "Rock", 3);
        AddSong("Bohemian Rhapsody", "Queen", "Rock", 6);
        AddSong("Hotel California", "Eagles", "Rock", 7);
        AddSong("Stairway to Heaven", "Led Zeppelin", "Rock", 8);
        AddSong("Dream On", "Aerosmith", "Rock", 5);

        AddSong("Billie Jean", "Michael Jackson", "Pop", 5);
        AddSong("Thriller", "Michael Jackson", "Pop", 6);
        AddSong("Bad", "Michael Jackson", "Pop", 5);
        AddSong("Beat It", "Michael Jackson", "Pop", 5);
        AddSong("Smooth Criminal", "Michael Jackson", "Pop", 5);

        AddSong("Shape of You", "Ed Sheeran", "Pop", 4);
        AddSong("Thinking Out Loud", "Ed Sheeran", "Pop", 5);
        AddSong("Photograph", "Ed Sheeran", "Pop", 5);
        AddSong("Castle on the Hill", "Ed Sheeran", "Pop", 4);
        AddSong("Perfect", "Ed Sheeran", "Pop", 4);

        AddSong("Yesterday", "The Beatles", "Classic", 3);
        AddSong("Let It Be", "The Beatles", "Classic", 4);
        AddSong("Hey Jude", "The Beatles", "Classic", 7);
        AddSong("Twist and Shout", "The Beatles", "Classic", 3);
        AddSong("All You Need Is Love", "The Beatles", "Classic", 4);
    }
}


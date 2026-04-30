using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages the scheduling and organization of music practice sessions.
/// Uses different data structures (Dictionary, HashSet, Queue, Stack) to efficiently manage songs and sessions.
/// </summary>
public class MusicScheduler
{
    /// <summary>Dictionary to store songs by name for fast lookup.</summary>
    private Dictionary<string, Song> songs;
    
    /// <summary>HashSet to store unique genres, automatically preventing duplicates.</summary>
    private HashSet<string> genres;
    
    /// <summary>Queue to store practice sessions in FIFO order (first in, first out).</summary>
    private Queue<PracticeSession> upcomingSessions;
    
    /// <summary>Stack to store recently practiced songs in LIFO order (last in, first out).</summary>
    private Stack<Song> recentlyPracticed;

    /// <summary>
    /// Initializes a new instance of the <see cref="MusicScheduler"/> class.
    /// Initializes all internal data structures for managing songs and sessions.
    /// </summary>
    public MusicScheduler()
    {
        songs = new Dictionary<string, Song>();
        genres = new HashSet<string>();
        upcomingSessions = new Queue<PracticeSession>();
        recentlyPracticed = new Stack<Song>();
    }

    /// <summary>
    /// Adds a new song to the scheduler.
    /// </summary>
    /// <param name="name">The name of the song to add. Cannot be null or empty.</param>
    /// <param name="artist">The artist or composer of the song.</param>
    /// <param name="genre">The music genre of the song.</param>
    /// <param name="duration">The duration of the song in minutes.</param>
    /// <returns>The newly created Song object.</returns>
    /// <exception cref="Exception">Thrown when song name is empty or song already exists.</exception>
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

    /// <summary>
    /// Removes a song from the scheduler by its name.
    /// </summary>
    /// <param name="name">The name of the song to remove.</param>
    /// <returns>True if the song was removed; false if the song was not found.</returns>
    public bool RemoveSong(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return songs.Remove(name);
    }

    /// <summary>
    /// Edits an existing song's information.
    /// </summary>
    /// <param name="originalName">The current name of the song to edit.</param>
    /// <param name="newName">The new name for the song.</param>
    /// <param name="artist">The updated artist or composer name.</param>
    /// <param name="genre">The updated music genre.</param>
    /// <param name="duration">The updated duration in minutes.</param>
    /// <returns>The updated Song object.</returns>
    /// <exception cref="Exception">Thrown when the song is not found or new name is empty or already exists.</exception>
    public Song EditSong(string originalName, string newName, string artist, string genre, int duration)
    {
        // Check if original song exists
        if (!songs.ContainsKey(originalName))
        {
            throw new Exception("Song not found");
        }

        // Check if new name is empty
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new Exception("Song name cannot be empty");
        }

        // If name is changing and new name already exists (and is different)
        if (newName != originalName && songs.ContainsKey(newName))
        {
            throw new Exception("Song with this name already exists");
        }

        // Get the existing song
        Song song = songs[originalName];

        // Remove old entry if name is changing
        if (newName != originalName)
        {
            songs.Remove(originalName);
        }

        // Update song properties
        song.Name = newName;
        song.Artist = artist;
        song.Genre = genre;
        song.DurationMinutes = duration;

        // Add back with new name if it changed
        if (newName != originalName)
        {
            songs[newName] = song;
        }

        // Update genre set
        genres.Add(genre);

        return song;
    }

    /// <summary>
    /// Retrieves a song by its name.
    /// </summary>
    /// <param name="name">The name of the song to retrieve.</param>
    /// <returns>The Song object if found.</returns>
    public Song GetSongByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new Exception("Song name cannot be empty");
        }

        if (!songs.ContainsKey(name))
        {
            throw new Exception("Song not found");
        }

        return songs[name];
    }

    /// <summary>
    /// Retrieves all songs in the scheduler.
    /// </summary>
    /// <returns>A list containing all Song objects. Returns an empty list if no songs exist.</returns>
    public List<Song> GetAllSongs()
    {
        return songs.Values.ToList();
    }

    /// <summary>
    /// Retrieves all songs of a specific genre.
    /// </summary>
    /// <param name="genre">The genre to filter songs by.</param>
    /// <returns>A list of songs matching the specified genre. Returns an empty list if none found.</returns>
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

    /// <summary>
    /// Retrieves all unique genres in the scheduler.
    /// </summary>
    /// <returns>A list of all unique genre names.</returns>
    public List<string> GetAllGenres()
    {
        return genres.ToList();
    }

    /// <summary>
    /// Schedules a new practice session for a song.
    /// </summary>
    /// <param name="songName">The name of the song to practice. Must exist in the scheduler.</param>
    /// <param name="duration">The duration of the practice session in minutes.</param>
    /// <param name="notes">Optional notes or comments about the practice session.</param>
    /// <exception cref="Exception">Thrown when song name is empty or song is not found.</exception>
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

    /// <summary>
    /// Retrieves all upcoming practice sessions.
    /// </summary>
    /// <returns>A list of all upcoming PracticeSession objects in queue order.</returns>
    public List<PracticeSession> GetUpcomingSessions()
    {
        return upcomingSessions.ToList();
    }

    /// <summary>
    /// Completes the next scheduled practice session and records the song as recently practiced.
    /// </summary>
    /// <returns>The completed PracticeSession object.</returns>
    /// <exception cref="Exception">Thrown when there are no upcoming sessions to complete.</exception>
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

    /// <summary>
    /// Retrieves recently practiced songs in reverse chronological order.
    /// </summary>
    /// <returns>A list of recently practiced Song objects, with most recent first.</returns>
    public List<Song> GetRecentlyPracticed()
    {
        return recentlyPracticed.ToList();
    }

    /// <summary>
    /// Marks a song as completed or mastered.
    /// </summary>
    /// <param name="songName">The name of the song to mark as completed.</param>
    /// <exception cref="Exception">Thrown when song name is empty or song is not found.</exception>
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

    /// <summary>
    /// Checks if a song exists in the scheduler.
    /// </summary>
    /// <param name="songName">The name of the song to check.</param>
    /// <returns>True if the song exists; false otherwise.</returns>
    public bool SongExists(string songName)
    {
        return songs.ContainsKey(songName);
    }

    /// <summary>
    /// Gets the total number of songs in the scheduler.
    /// </summary>
    /// <returns>The count of songs currently stored.</returns>
    public int GetTotalSongs()
    {
        return songs.Count;
    }

    /// <summary>
    /// Clears all data from the scheduler.
    /// Removes all songs, genres, sessions, and practice history.
    /// </summary>
    public void Clear()
    {
        songs.Clear();
        genres.Clear();
        upcomingSessions.Clear();
        recentlyPracticed.Clear();
    }

    /// <summary>
    /// Loads a collection of 20 default sample songs into the scheduler.
    /// Clears any existing data first and populates with songs from various artists and genres.
    /// Useful for demonstration and testing purposes.
    /// </summary>
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


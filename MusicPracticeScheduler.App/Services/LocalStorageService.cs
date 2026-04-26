using System.Text.Json;

/// <summary>
/// Provides serialization and deserialization functionality for the music scheduler data.
/// Converts between MusicScheduler objects and JSON format for storage purposes.
/// </summary>
public class LocalStorageService
{
    /// <summary>The storage key used to identify scheduler data in browser localStorage.</summary>
    private const string StorageKey = "musicSchedulerData";

    /// <summary>
    /// Data container that holds all scheduler information in a serializable format.
    /// Contains songs, practice sessions, and genres.
    /// </summary>
    public class SchedulerData
    {
        /// <summary>Gets or sets the list of songs managed by the scheduler.</summary>
        public List<Song> Songs { get; set; } = new List<Song>();

        /// <summary>Gets or sets the list of upcoming practice sessions.</summary>
        public List<PracticeSession> Sessions { get; set; } = new List<PracticeSession>();

        /// <summary>Gets or sets the list of unique genres in the scheduler.</summary>
        public List<string> Genres { get; set; } = new List<string>();
    }

    /// <summary>
    /// Converts a MusicScheduler object to its JSON string representation.
    /// </summary>
    /// <param name="scheduler">The MusicScheduler object to serialize.</param>
    /// <returns>A JSON string containing all scheduler data.</returns>
    /// <exception cref="Exception">Thrown if serialization encounters an error.</exception>
    public string SerializeScheduler(MusicScheduler scheduler)
    {
        try
        {
            SchedulerData data = new SchedulerData
            {
                Songs = scheduler.GetAllSongs(),
                Sessions = scheduler.GetUpcomingSessions(),
                Genres = scheduler.GetAllGenres()
            };

            // Convert to JSON string
            string json = JsonSerializer.Serialize(data);
            return json;
        }
        catch (Exception ex)
        {
            throw new Exception("Error serializing scheduler: " + ex.Message);
        }
    }

    /// <summary>
    /// Converts a JSON string back into a populated MusicScheduler object.
    /// Reconstructs all songs and their properties from the serialized data.
    /// </summary>
    /// <param name="json">The JSON string containing scheduler data.</param>
    /// <returns>A new MusicScheduler object populated with data from the JSON string.</returns>
    /// <exception cref="Exception">Thrown if deserialization encounters an error.</exception>
    public MusicScheduler DeserializeScheduler(string json)
    {
        try
        {
            MusicScheduler scheduler = new MusicScheduler();

            if (string.IsNullOrWhiteSpace(json))
            {
                return scheduler;
            }

            SchedulerData? data = JsonSerializer.Deserialize<SchedulerData>(json);

            if (data != null && data.Songs != null)
            {
                // Re-add all songs to the scheduler
                foreach (Song song in data.Songs)
                {
                    try
                    {
                        scheduler.AddSong(song.Name, song.Artist, song.Genre, song.DurationMinutes);
                        
                        // If song was marked complete, mark it again
                        if (song.IsCompleted)
                        {
                            scheduler.MarkSongComplete(song.Name);
                        }
                    }
                    catch
                    {
                        // Skip songs that couldn't be added
                    }
                }
            }

            return scheduler;
        }
        catch (Exception ex)
        {
            throw new Exception("Error deserializing scheduler: " + ex.Message);
        }
    }
}

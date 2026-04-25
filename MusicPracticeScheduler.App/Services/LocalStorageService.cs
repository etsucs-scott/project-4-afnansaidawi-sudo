using System.Text.Json;

// LocalStorageService - handles saving and loading data as JSON
// This is a simple service for a beginner project
public class LocalStorageService
{
    // Key where data is stored in browser localStorage
    private const string StorageKey = "musicSchedulerData";

    // A simple data container to store all scheduler data
    public class SchedulerData
    {
        public List<Song> Songs { get; set; } = new List<Song>();
        public List<PracticeSession> Sessions { get; set; } = new List<PracticeSession>();
        public List<string> Genres { get; set; } = new List<string>();
    }

    // Convert scheduler data to JSON string
    // This method takes all the data and converts it to JSON format
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

    // Convert JSON string back to scheduler
    // This method takes JSON and reconstructs the scheduler data
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

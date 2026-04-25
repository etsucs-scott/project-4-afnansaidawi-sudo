// Song class - represents a song the user is learning
public class Song
{
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Genre { get; set; }
    public int DurationMinutes { get; set; }
    public bool IsCompleted { get; set; }

    // Empty constructor for JSON deserialization
    public Song()
    {
        Name = "";
        Artist = "";
        Genre = "";
        DurationMinutes = 0;
        IsCompleted = false;
    }

    // Constructor with all fields
    public Song(string name, string artist, string genre, int duration)
    {
        Name = name;
        Artist = artist;
        Genre = genre;
        DurationMinutes = duration;
        IsCompleted = false;
    }
}

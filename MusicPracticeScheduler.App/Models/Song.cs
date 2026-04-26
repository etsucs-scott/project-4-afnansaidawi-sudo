/// <summary>
/// Represents a song that the user is learning or practicing.
/// </summary>
public class Song
{
    /// <summary>Gets or sets the name of the song.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the artist or composer of the song.</summary>
    public string Artist { get; set; }

    /// <summary>Gets or sets the music genre of the song.</summary>
    public string Genre { get; set; }

    /// <summary>Gets or sets the duration of the song in minutes.</summary>
    public int DurationMinutes { get; set; }

    /// <summary>Gets or sets a value indicating whether the song has been completed or mastered.</summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Song"/> class with default values.
    /// Used for JSON deserialization.
    /// </summary>
    public Song()
    {
        Name = "";
        Artist = "";
        Genre = "";
        DurationMinutes = 0;
        IsCompleted = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Song"/> class with all fields.
    /// </summary>
    /// <param name="name">The name of the song.</param>
    /// <param name="artist">The artist or composer of the song.</param>
    /// <param name="genre">The music genre of the song.</param>
    /// <param name="duration">The duration of the song in minutes.</param>
    public Song(string name, string artist, string genre, int duration)
    {
        Name = name;
        Artist = artist;
        Genre = genre;
        DurationMinutes = duration;
        IsCompleted = false;
    }
}

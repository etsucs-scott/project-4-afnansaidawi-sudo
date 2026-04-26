/// <summary>
/// Represents a single music practice session scheduled for a specific song.
/// Tracks when a user practices a song, how long they practiced, and any notes about the session.
/// </summary>
public class PracticeSession
{
    /// <summary>Gets or sets the name of the song being practiced in this session.</summary>
    public string SongName { get; set; }

    /// <summary>Gets or sets the date and time when the practice session was scheduled.</summary>
    public DateTime Date { get; set; }

    /// <summary>Gets or sets the duration of the practice session in minutes.</summary>
    public int DurationMinutes { get; set; }

    /// <summary>Gets or sets optional notes or comments about the practice session.</summary>
    public string Notes { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PracticeSession"/> class with default values.
    /// Used for JSON deserialization.
    /// </summary>
    public PracticeSession()
    {
        SongName = "";
        Date = DateTime.Now;
        DurationMinutes = 0;
        Notes = "";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PracticeSession"/> class with specified values.
    /// </summary>
    /// <param name="songName">The name of the song to practice.</param>
    /// <param name="duration">The duration of the practice session in minutes.</param>
    /// <param name="notes">Optional notes about the practice session.</param>
    public PracticeSession(string songName, int duration, string notes)
    {
        SongName = songName;
        Date = DateTime.Now;
        DurationMinutes = duration;
        Notes = notes;
    }
}

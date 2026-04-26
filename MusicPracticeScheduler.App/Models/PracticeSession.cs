/// <summary>
/// Represents a single music practice session for a specific song.
/// </summary>
public class PracticeSession
{
    /// <summary>Gets or sets the name of the song being practiced.</summary>
    public string SongName { get; set; }

    /// <summary>Gets or sets the date and time when the session was scheduled.</summary>
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
    /// Initializes a new instance of the <see cref="PracticeSession"/> class with all fields.
    /// </summary>
    /// <param name="songName">The name of the song to practice.</param>
    /// <param name="duration">The duration of the practice session in minutes.</param>
    /// <param name="notes">Optional notes or comments about the session.</param>
    public PracticeSession(string songName, int duration, string notes)
    {
        SongName = songName;
        Date = DateTime.Now;
        DurationMinutes = duration;
        Notes = notes;
    }
}

// PracticeSession class - represents one practice session
public class PracticeSession
{
    public string SongName { get; set; }
    public DateTime Date { get; set; }
    public int DurationMinutes { get; set; }
    public string Notes { get; set; }

    // Empty constructor for JSON deserialization
    public PracticeSession()
    {
        SongName = "";
        Date = DateTime.Now;
        DurationMinutes = 0;
        Notes = "";
    }

    // Constructor with all fields
    public PracticeSession(string songName, int duration, string notes)
    {
        SongName = songName;
        Date = DateTime.Now;
        DurationMinutes = duration;
        Notes = notes;
    }
}

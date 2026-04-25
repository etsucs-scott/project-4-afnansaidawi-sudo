namespace MusicPracticeScheduler.Tests;

// Unit tests for MusicScheduler class
// Tests verify that the scheduler correctly manages songs and practice sessions
public class MusicSchedulerTests
{
    // Test 1: Adding a song successfully
    [Fact]
    public void TestAddSong_Success()
    {
        // Arrange - Create a scheduler
        MusicScheduler scheduler = new MusicScheduler();

        // Act - Add a song
        Song song = scheduler.AddSong("Imagine", "John Lennon", "Rock", 3);

        // Assert - Check that song was added
        Assert.NotNull(song);
        Assert.Equal("Imagine", song.Name);
        Assert.Single(scheduler.GetAllSongs());
    }

    // Test 2: Cannot add song with empty name
    [Fact]
    public void TestAddSong_EmptyName_ThrowsException()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();

        // Act & Assert - Should throw exception
        Assert.Throws<Exception>(() => scheduler.AddSong("", "Artist", "Genre", 5));
    }

    // Test 3: Cannot add duplicate song name
    [Fact]
    public void TestAddSong_DuplicateName_ThrowsException()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Imagine", "John Lennon", "Rock", 3);

        // Act & Assert - Should throw exception for duplicate
        Assert.Throws<Exception>(() => scheduler.AddSong("Imagine", "Another Artist", "Pop", 4));
    }

    // Test 4: Remove song successfully
    [Fact]
    public void TestRemoveSong_Success()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Imagine", "John Lennon", "Rock", 3);

        // Act - Remove the song
        bool removed = scheduler.RemoveSong("Imagine");

        // Assert - Check that song was removed
        Assert.True(removed);
        Assert.Empty(scheduler.GetAllSongs());
    }

    // Test 5: Remove non-existent song returns false
    [Fact]
    public void TestRemoveSong_NotFound_ReturnsFalse()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();

        // Act - Try to remove song that doesn't exist
        bool removed = scheduler.RemoveSong("NonExistent");

        // Assert - Should return false
        Assert.False(removed);
    }

    // Test 6: Get all songs returns list
    [Fact]
    public void TestGetAllSongs_ReturnsList()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Song1", "Artist1", "Rock", 3);
        scheduler.AddSong("Song2", "Artist2", "Pop", 4);

        // Act - Get all songs
        List<Song> allSongs = scheduler.GetAllSongs();

        // Assert - Check that we got both songs
        Assert.Equal(2, allSongs.Count);
    }

    // Test 7: Get songs by genre filters correctly
    [Fact]
    public void TestGetSongsByGenre_FiltersCorrectly()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Song1", "Artist1", "Rock", 3);
        scheduler.AddSong("Song2", "Artist2", "Rock", 4);
        scheduler.AddSong("Song3", "Artist3", "Pop", 5);

        // Act - Get only Rock songs
        List<Song> rockSongs = scheduler.GetSongsByGenre("Rock");

        // Assert - Should have 2 Rock songs
        Assert.Equal(2, rockSongs.Count);
    }

    // Test 8: Get all genres uses HashSet (no duplicates)
    [Fact]
    public void TestGetAllGenres_NoDuplicates()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Song1", "Artist1", "Rock", 3);
        scheduler.AddSong("Song2", "Artist2", "Rock", 4);

        // Act - Get all genres
        List<string> genres = scheduler.GetAllGenres();

        // Assert - Should have only 1 genre (Rock appears twice but HashSet prevents duplicates)
        Assert.Single(genres);
        Assert.Contains("Rock", genres);
    }

    // Test 9: Schedule session adds to Queue
    [Fact]
    public void TestScheduleSession_Success()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Imagine", "John Lennon", "Rock", 3);

        // Act - Schedule a practice session
        scheduler.ScheduleSession("Imagine", 30, "Practice scales");

        // Assert - Check that session was added
        List<PracticeSession> sessions = scheduler.GetUpcomingSessions();
        Assert.Single(sessions);
    }

    // Test 10: Schedule session with non-existent song throws exception
    [Fact]
    public void TestScheduleSession_SongNotFound_ThrowsException()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();

        // Act & Assert - Should throw exception
        Assert.Throws<Exception>(() => scheduler.ScheduleSession("NonExistent", 30, "Notes"));
    }

    // Test 11: Complete session removes from Queue and adds to Stack
    [Fact]
    public void TestCompleteNextSession_Success()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Imagine", "John Lennon", "Rock", 3);
        scheduler.ScheduleSession("Imagine", 30, "Practice");

        // Act - Complete the session
        PracticeSession completed = scheduler.CompleteNextSession();

        // Assert - Check that session removed from queue and song added to stack
        Assert.Equal("Imagine", completed.SongName);
        Assert.Empty(scheduler.GetUpcomingSessions());
        Assert.Single(scheduler.GetRecentlyPracticed());
    }

    // Test 12: Mark song complete updates flag
    [Fact]
    public void TestMarkSongComplete_UpdatesFlag()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        Song song = scheduler.AddSong("Imagine", "John Lennon", "Rock", 3);

        // Act - Mark song as complete
        scheduler.MarkSongComplete("Imagine");

        // Assert - Check that song is marked complete
        List<Song> allSongs = scheduler.GetAllSongs();
        Assert.True(allSongs[0].IsCompleted);
    }

    // Test 13: Test Dictionary lookup by song name
    [Fact]
    public void TestSongExists_Dictionary()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Imagine", "John Lennon", "Rock", 3);

        // Act - Check if song exists
        bool exists = scheduler.SongExists("Imagine");

        // Assert - Should exist
        Assert.True(exists);
    }

    // Test 14: Recently practiced uses Stack (LIFO order)
    [Fact]
    public void TestRecentlyPracticed_StackOrder()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Song1", "Artist1", "Rock", 3);
        scheduler.AddSong("Song2", "Artist2", "Rock", 3);

        // Act - Schedule and complete sessions in order
        scheduler.ScheduleSession("Song1", 30, "note");
        scheduler.ScheduleSession("Song2", 30, "note");
        scheduler.CompleteNextSession(); // Song1 completed first
        scheduler.CompleteNextSession(); // Song2 completed second

        // Assert - Stack should return Song2 first (LIFO)
        List<Song> recent = scheduler.GetRecentlyPracticed();
        Assert.Equal("Song2", recent[0].Name);
        Assert.Equal("Song1", recent[1].Name);
    }

    // Test 15: Upcoming sessions uses Queue (FIFO order)
    [Fact]
    public void TestUpcomingSessions_QueueOrder()
    {
        // Arrange
        MusicScheduler scheduler = new MusicScheduler();
        scheduler.AddSong("Song1", "Artist1", "Rock", 3);
        scheduler.AddSong("Song2", "Artist2", "Rock", 3);

        // Act - Schedule two sessions
        scheduler.ScheduleSession("Song1", 30, "note1");
        scheduler.ScheduleSession("Song2", 30, "note2");

        // Assert - Queue should return Song1 first (FIFO)
        List<PracticeSession> sessions = scheduler.GetUpcomingSessions();
        Assert.Equal("Song1", sessions[0].SongName);
        Assert.Equal("Song2", sessions[1].SongName);
    }
}


// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
	public class StageTests
    {
		[Test]
	    public void StageConstructor()
	    {
			Stage stage = new Stage();
			Assert.NotNull(stage.Performers);
		}
		[Test]
		public void PerformersThrowExceptions()
		{
			Stage stage = new Stage();
			Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(null));

			Performer performer = new Performer("Diana", "Yovcheva", 7);

			Assert.Throws<ArgumentException>(() => stage.AddPerformer(performer));
		}
		[Test]
		public void PerformersWorks()
		{
			Stage stage = new Stage();
			Performer performer = new Performer("Diana", "Yovcheva", 18);

			Assert.AreEqual(18, performer.Age);

			stage.AddPerformer(performer);

			Assert.AreEqual(1, stage.Performers.Count);
			Assert.True(stage.Performers.Any(x=>x == performer));	
		}
		[Test]
		public void ValidateAddSong()
		{
			Stage stage = new Stage();
			Assert.Throws<ArgumentNullException>(() => stage.AddSong(null));

			Song song = new Song("someSong", new TimeSpan( 0, 0, 2));

			Assert.Throws<ArgumentException>(() => stage.AddSong(song));

			Song song2 = new Song("someSong", new TimeSpan(0, 3, 25));
			stage.AddSong(song2);
		}
        [Test]
		public void ValidateAddSongToPerformer()
        {
			Stage stage = new Stage();

			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(null, "somePerformers"));
			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer("someSong", null));

			Performer performer = new Performer("Diana", "Yovcheva", 19);
			stage.AddPerformer(performer);
			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("nothingName", performer.FullName));

			Song song2 = new Song("someSong", new TimeSpan(0, 3, 25));
			stage.AddSong(song2);
			string result = stage.AddSongToPerformer(song2.Name, performer.FullName);
			Assert.True(performer.SongList.Contains(song2));
			Assert.AreEqual("someSong (03:25) will be performed by Diana Yovcheva", result);	
		}
		[Test]
		public void ValidatePlay()
		{
			Stage stage = new Stage();
			Performer performer = new Performer("Diana", "Yovcheva", 19);
			Performer performer1 = new Performer("Sashko", "Yovchev", 18);
			Performer performer2 = new Performer("Maria", "Ivanova", 35);

			Song song1 = new Song("someSong1", new TimeSpan(0, 3, 25));
			Song song2 = new Song("someSong2", new TimeSpan(0, 2, 25));
			Song song3 = new Song("someSong3", new TimeSpan(0, 3, 04));
			Song song4 = new Song("someSong4", new TimeSpan(0, 1, 25));

			stage.AddPerformer(performer);
			stage.AddPerformer(performer1);
			stage.AddPerformer(performer2);

			stage.AddSong(song1);
			stage.AddSong(song2);
			stage.AddSong(song3);
			stage.AddSong(song4);

			stage.AddSongToPerformer(song1.Name, performer.FullName);
			stage.AddSongToPerformer(song2.Name, performer.FullName);	
			stage.AddSongToPerformer(song3.Name, performer1.FullName);	
			stage.AddSongToPerformer(song4.Name, performer2.FullName);

			string result = stage.Play();

			Assert.AreEqual($"{stage.Performers.Count} performers played {4} songs", result);
		}
    }
}
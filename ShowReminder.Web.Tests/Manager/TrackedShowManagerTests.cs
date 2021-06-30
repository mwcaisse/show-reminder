using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using ShowReminder.Data;
using ShowReminder.Data.Entity;
using ShowReminder.Web.Manager;
using Xunit;

namespace ShowReminder.Web.Tests.Manager
{
    public class TrackedShowManagerTests
    {
        private TrackedShowManager subject;

        private Mock<DataContext> mockDatabaseContext;

        private Mock<DbSet<TrackedShow>> mockDbSet;

        private Mock<ShowManager> mockShowManager;

        private List<TrackedShow> testTrackedShows;

        public TrackedShowManagerTests()
        {
            mockDatabaseContext = new Mock<DataContext>(MockBehavior.Default, new object[]
            {
                new DbContextOptions<DataContext>()
            });
            
            mockDatabaseContext.Setup(x => x.SaveChanges()).Verifiable(); // Make SaveChanges do nothing

            testTrackedShows = new List<TrackedShow>()
            {
                new TrackedShow()
                {
                    Id = 1,
                    Name = "Archer",
                    FirstAiredDate = null,
                    AirDay = "Wednesday",
                    AirTime = "22:00"
                },
                new TrackedShow()
                {
                    Id = 3,
                    Name = "Mr Robot",
                    FirstAiredDate = null,
                    AirDay = "Sunday",
                    AirTime = "21:00"
                }
            };

            mockDbSet = MockUtils.CreateDbSetMock(testTrackedShows);
            mockDatabaseContext.Setup(x => x.Shows).Returns(mockDbSet.Object);

            mockShowManager = new Mock<ShowManager>(MockBehavior.Default, null);
            
            subject = new TrackedShowManager(mockDatabaseContext.Object, mockShowManager.Object);
        }

        public class GetTests : TrackedShowManagerTests
        {
            [Fact]
            public void TestReturnsCorrectTrackedShow()
            {
                var showOne = subject.Get(1);

                Assert.NotNull(showOne);
                Assert.Equal(1, showOne.Id);
                Assert.Equal("Archer", showOne.Name);
                Assert.Equal("Wednesday", showOne.AirDay);
            }

            [Fact]
            public void TestReturnsNullForInexistentShow()
            {
                Assert.Null(subject.Get(0));
                Assert.Null(subject.Get(5));
                Assert.Null(subject.Get(-100));
            }
        }
        
        public class GetShowsAiredYesterdayTests : TrackedShowManagerTests
        {
            [Fact]
            public void TestOnlyReturnsShowsLastAiredYesterday()
            {
                testTrackedShows.Clear();
                
                testTrackedShows.AddRange(new List<TrackedShow>()
                {
                    new TrackedShow()
                    {
                        Id = 1,
                        LastEpisodeId = 1,
                        LastEpisode = new TrackedEpisode()
                        {
                            Id = 1,
                            OverallNumber = 10,
                            SeasonNumber = 6,
                            EpisodeNumber = 5,
                            AirDate = DateTime.Now.AddMinutes(-23 * 60), // 1 day ago
                            Name = "Tethics?!",
                        }
                    },
                    new TrackedShow()
                    {
                        Id = 2,
                        LastEpisodeId = 2,
                        LastEpisode = new TrackedEpisode()
                        {
                            Id = 2,
                            OverallNumber = 10,
                            SeasonNumber = 6,
                            EpisodeNumber = 5,
                            AirDate = DateTime.Now.AddMinutes(-120), // 2 hours ago
                            Name = "Spaaaaace?!",
                        }
                    },
                    new TrackedShow()
                    {
                        Id = 3,
                        LastEpisodeId = 3,
                        LastEpisode = new TrackedEpisode()
                        {
                            Id = 3,
                            OverallNumber = 10,
                            SeasonNumber = 6,
                            EpisodeNumber = 5,
                            AirDate = DateTime.Now.AddDays(-10), // 10 days ago
                            Name = "Spaaaaace?!",
                        }
                    }
                });
                
                //we'd determine that a show aired yesterday using LastEpisode

                var yesterdayShows = subject.GetShowsAiredAfter(DateTime.Now.AddDays(-1));
                Assert.Equal(2, yesterdayShows.Count);
            }
        }
       
    }
}

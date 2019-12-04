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
        }
    }
}
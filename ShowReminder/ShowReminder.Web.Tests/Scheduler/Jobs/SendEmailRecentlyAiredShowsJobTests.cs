using ShowReminder.Data.Entity;
using ShowReminder.Web.Scheduler.Jobs;
using Xunit;

namespace ShowReminder.Web.Tests.Scheduler.Jobs
{
    public class ConstructTorrentSearchUrlTests
    {

        private TrackedShow CreateShow(string name, int season, int episode)
        {
            return new TrackedShow()
            {
                Name = name,
                LastEpisode = new TrackedEpisode()
                {
                    SeasonNumber = season,
                    EpisodeNumber = episode
                }
            };
        }
        
        [Fact]
        public void TestConstructsProperUrl()
        {
            var show = CreateShow("Archer", 10, 12);
            var link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(show);
            
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Archer+1080p+s10e12&order=seeders&by=DESC", link);
        }
        
        [Fact]
        public void TestConvertsSpacesToPlus()
        {
            var link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Mr Robot", 10, 12));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Mr+Robot+1080p+s10e12&order=seeders&by=DESC", link);
            
            link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Hawaii Five 0", 10, 12));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Hawaii+Five+0+1080p+s10e12&order=seeders&by=DESC", link);
        }
        
        [Fact]
        public void TestPadsEpisodeWithZero()
        {
            var link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Archer", 10, 1));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Archer+1080p+s10e01&order=seeders&by=DESC", link);
            
            link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Archer", 10, 5));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Archer+1080p+s10e05&order=seeders&by=DESC", link);
            
            link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Archer", 10, 15));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Archer+1080p+s10e15&order=seeders&by=DESC", link);
        }

        [Fact]
        public void TestsPadsSeasonWithZero()
        {    
            var link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Archer", 13, 12));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Archer+1080p+s13e12&order=seeders&by=DESC", link);
            
            link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Archer", 1, 12));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Archer+1080p+s01e12&order=seeders&by=DESC", link);
            
            link = SendEmailRecentlyAiredShowsJob.ConstructTorrentSearchUrl(CreateShow("Archer", 5, 12));
            Assert.Equal("https://rarbg.to/torrents.php?category=18;41;49&search=Archer+1080p+s05e12&order=seeders&by=DESC", link);
        }
    }
}
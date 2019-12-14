namespace ShowReminder.Web
{
    public class ApplicationConfiguration
    {
        public string SendGridApiKey { get; set; }

        public string FromEmailAddress { get; set; }
        public string FromEmailAddressName { get; set; }

        public string ToEmailAddress { get; set; }
        public string ToEmailAddressName { get; set; }

    }
}
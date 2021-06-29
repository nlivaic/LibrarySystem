namespace LibrarySystem.Infrastructure
{
    public class HttpClientName
    {
        public string Name { get; set; }

        private HttpClientName(string name)
        {
            Name = name;
        }

        public readonly static HttpClientName MicroBlinkClient = new("MicroBlinkClient");

        public static implicit operator string(HttpClientName c) => c.Name;
    }
}

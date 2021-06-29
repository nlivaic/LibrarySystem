using System.Text.Json.Serialization;

namespace LibrarySystem.Infrastructure.Scanner
{
    public class MrzData
    {
        [JsonPropertyName("rawMrzString")]
        public string RawMrzString { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("mrzData")]
        public MrzData MrzData { get; set; }
    }

    public class MrzIdResponse
    {
        [JsonPropertyName("result")]
        public Result Result { get; set; }
    }
}

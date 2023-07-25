using Newtonsoft.Json;

namespace Charisma.Test.Helper
{
    public class ErrorResponse
    {
        public string type { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string traceId { get; set; }
    }
}
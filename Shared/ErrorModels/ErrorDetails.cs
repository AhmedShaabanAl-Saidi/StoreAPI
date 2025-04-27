namespace Shared.ErrorModels
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}

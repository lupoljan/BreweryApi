namespace BreweryApi.Utilities
{
    public class ErrorResponse
    {
        public string Code { get; set; } = ErrorCodes.UnexpectedError;
        public string Message { get; set; } = "An error occurred.";
        public string? Detail { get; set; }
    }
}

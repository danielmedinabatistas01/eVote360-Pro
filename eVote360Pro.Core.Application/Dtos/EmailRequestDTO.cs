namespace eVote360Pro.Core.Application.DTOs.Email
{
    public class EmailRequestDTO
    {
        public string? To { get; set; }

        public List<string>? ToRange { get; set; } = new();

        public string Subject { get; set; } = string.Empty;

        public string HtmlBody { get; set; } = string.Empty;
    }
}
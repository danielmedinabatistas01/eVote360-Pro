namespace eVote360Pro.Core.Application.DTOs.Email
{
    public class EmailRequestDTO
    {
        public string? To { get; set; }
        public required string Subject { get; set; } = string.Empty;
        public required string HtmlBody { get; set; } = string.Empty;
        public List<string>? ToRange { get; set; }
    }
}
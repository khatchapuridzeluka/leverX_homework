namespace leverX.Dtos.DTOs.Users
{
    public record LoginResponseDto
    {
        public string Token { get; init; }
        public DateTime ExpiresAt { get; init; }
        public UserDto User { get; init; }
    }
}

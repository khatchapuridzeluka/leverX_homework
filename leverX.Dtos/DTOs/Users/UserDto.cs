namespace leverX.Dtos.DTOs.Users
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public string Username { get; set; }   

        public string Role { get; set; }
    }
}

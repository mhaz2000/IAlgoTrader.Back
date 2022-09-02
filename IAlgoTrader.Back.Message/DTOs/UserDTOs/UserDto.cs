namespace IAlgoTrader.Back.Message.DTOs.UserDTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid LogoId { get; set; }
    }
}

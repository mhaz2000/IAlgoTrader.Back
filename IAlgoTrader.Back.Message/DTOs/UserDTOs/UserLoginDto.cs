namespace IAlgoTrader.Back.Message.DTOs.UserDTOs
{
    public class UserLoginDto
    {
        public string TokenType { get; set; }
        public string AuthToken { get; set; }
        public string RefreshToken { get; set; }
        public int expires_in { get; set; }
        public bool IsAdmin { get; set; }
    }
}

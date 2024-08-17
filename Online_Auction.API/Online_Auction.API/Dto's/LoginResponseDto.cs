namespace Online_Auction.API.Dto_s
{
    public class LoginResponseDto
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public int Expires_In { get; set; }
    }
}

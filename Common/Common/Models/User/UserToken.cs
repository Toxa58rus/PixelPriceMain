namespace Common.Models.User
{
    public class UserToken
    {
	    public UserToken(string token, string userId)
	    {
		    Token = token;
		    UserId = userId;
	    }
        public string Token { get; private set; }
        public string UserId { get; private set; }
    }
}

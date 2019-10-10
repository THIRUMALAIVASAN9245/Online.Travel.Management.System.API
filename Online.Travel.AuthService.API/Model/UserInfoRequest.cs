namespace Online.Travel.AuthService.API.Model
{
    public class UserInfoRequest
    {
        public string Operation { get; set; }

        public string FilterByStatus { get; set; }

        public int Id { get; set; }
    }
}

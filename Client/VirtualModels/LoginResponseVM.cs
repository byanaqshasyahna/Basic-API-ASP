namespace Client.VirtualModels
{
    public class LoginResponseVM
    {
        public int status { get; set; }
        public string TokenJWT { get; set; }
        public string Message { get; set; }
    }
}

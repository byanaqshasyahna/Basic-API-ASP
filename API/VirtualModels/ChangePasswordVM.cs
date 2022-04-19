namespace API.VirtualModels
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int OTP { get; set; }
    }
}

namespace ServerAPI.EmailCommunication
{
    public interface IEmail
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}

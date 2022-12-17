namespace Chat.Models
{
    public class Message
    {
        public Message(string username, string content)
        {
            Username = username;
            Content = content;
        }

        public Message()
        {
        }

        public string Username { get; set; }
        public string Content { get; set; }
    }
}
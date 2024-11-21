namespace Wizzarts.Web.ViewModels.Chat
{
    public class MessageViewModel
    {
        public string User { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public int ChatId { get; set; }
    }
}

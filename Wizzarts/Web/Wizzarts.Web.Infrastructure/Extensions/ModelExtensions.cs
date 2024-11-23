namespace Wizzarts.Web.Infrastructure.Extensions
{
    using Wizzarts.Web.ViewModels.Art;
    using Wizzarts.Web.ViewModels.Article;
    using Wizzarts.Web.ViewModels.Deck;
    using Wizzarts.Web.ViewModels.Event;
    using Wizzarts.Web.ViewModels.Expansion;
    using Wizzarts.Web.ViewModels.PlayCard;
    using Wizzarts.Web.ViewModels.WizzartsMember;

    public static class ModelExtensions
    {
        public static string GetInformation(this ISingleArtViewModel art)
        {
            return art.Title.Replace(" ", "-");
        }

        public static string GetCardName(this ISingleCardViewModel card)
        {
            return card.Name.Replace(" ", "-");
        }

        public static string GetArticleTitle(this ISingleArticleViewModel article)
        {
            return article.Title.Replace(" ", "-");
        }

        public static string GetEventTitle(this ISingleEventViewModel currentEvent)
        {
            return currentEvent.Title.Replace(" ", "-");
        }

        public static string GetExpansionTitle(this ISingleExpansionViewModel currentExpansion)
        {
            return currentExpansion.Title.Replace(" ", "-");
        }

        public static string GetEventComponentTitle(this ISingleEventComponentViewModel currentMilestone)
        {
            return currentMilestone.Title.Replace(" ", "-");
        }

        public static string GetAvatarName(this ISingleAvatarViewModel currentAvatar)
        {
            return currentAvatar.Name.Replace(" ", "-");
        }

        public static string GetWizzartsMemberName(this ISingleMemberViewModel currentMember)
        {
            return currentMember.Username.Replace(" ", "-");
        }

        public static string GetWizzartsMemberNickName(this ISingleMemberViewModel currentMember)
        {
            return currentMember.Nickname.Replace(" ", "-");

        }

        public static string GetDeckName(this ISingleDeckViewModel currentDeck)
        {
            return currentDeck.Name.Replace(" ", "-");
        }
    }
}

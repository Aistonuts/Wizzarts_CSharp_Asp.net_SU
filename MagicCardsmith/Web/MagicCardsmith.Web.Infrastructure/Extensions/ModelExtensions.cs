using MagicCardsmith.Web.ViewModels.Event;

namespace MagicCardsmith.Web.Infrastructure.Extensions
{
    using MagicCardsmith.Web.ViewModels.Art;
    using MagicCardsmith.Web.ViewModels.Article;
    using MagicCardsmith.Web.ViewModels.Card;
    using MagicCardsmith.Web.ViewModels.Event;
    using MagicCardsmith.Web.ViewModels.Expansion;
    using MagicCardsmith.Web.ViewModels.Premium;

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

        public static string GetMilestoreTitle(this ISingleMilestoneViewModel currentMilestone)
        {
            return currentMilestone.Title.Replace(" ", "-");
        }

        public static string GetAvatarName(this ISingleAvatarViewModel currentAvatar)
        {
            return currentAvatar.Name.Replace(" ", "-");
        }
    }
}

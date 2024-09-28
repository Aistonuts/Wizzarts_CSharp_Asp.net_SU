namespace Wizzarts.Common
{
    public static class MembershipConstants
    {
        public const int MemberToArtistRequiredArts = 3;

        public const int ArtistToContentCreatorRequiredArts = 10;

        public const int MemberToContentCreatorRequiredArts = MemberToArtistRequiredArts + ArtistToContentCreatorRequiredArts;

        public const int RequiredNumberArticles = 2;

        public const int RequiredNumberEventCards = 2;

        public const int RequiredNumberEvents = 1;
    }
}

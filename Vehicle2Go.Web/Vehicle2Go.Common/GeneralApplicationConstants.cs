namespace Vehicle2Go.Common
{
    public static class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2023;

        public const int DefaultPage = 1;
        public const int EntitiesPerPage = 3;

        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string AdminEmail = "agent@abv.bg";

        public const string OnlineUsersCookieName = "IsOnline";
        public const int LastActivityBeforeOfflineMinutes = 10;

        public const string UsersCacheKey = "UsersCache";
        public const int UsersCacheDurationMinutes = 5;
        public const string RentsCacheKey = "RentsCache";
        public const int RentsCacheDurationMinutes = 10;
    }
}
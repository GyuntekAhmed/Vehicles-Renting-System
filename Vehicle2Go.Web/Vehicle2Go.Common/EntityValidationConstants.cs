﻿namespace Vehicle2Go.Common
{
    public static class EntityValidationConstants
    {
        public static class CategoryConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class VehicleConstants
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 50;

            public const int ModelMinLength = 1;
            public const int ModelMaxLength = 50;

            public const int ImageUrlMinLength = 3;
            public const int ImageUrlMaxLength = 2048;

            public const int RegistrationMinLength = 2;
            public const int RegistrationMaxLength = 20;

            public const int AddressMinLength = 2;
            public const int AddressMaxLength = 150;

            public const string PricePerDayMinValue = "0";
            public const string PricePerDayMaxValue = "9999999";

            public const int ColorMinLength = 2;
            public const int ColorMaxLength = 50;
        }

        public static class AgentConstants
        {
            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 20;

            public const int AddressMinLength = 2;
            public const int AddressMaxLength = 150;
        }

        public static class User
        {
            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 50;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 50;

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }
    }
}

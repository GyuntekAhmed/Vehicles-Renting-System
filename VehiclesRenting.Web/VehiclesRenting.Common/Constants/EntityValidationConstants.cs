namespace VehiclesRenting.Common.Constants
{
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }

        public static class Car
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 50;

            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 50;

            public const int ImageUrlMinLength = 3;
            public const int ImageUrlMaxLength = 2048;

            public const int RegistrationMinLength = 2;
            public const int RegistrationMaxLength = 20;

            public const int CurrentAddressMinLength = 2;
            public const int CurrentAddressMaxLength = 150;

            public const string PricePerDayMinValue = "1";

            public const int ColorMinLength = 2;
            public const int ColorMaxLength = 50;
        }

        public static class Motorcycle
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 50;

            public const int ModelMinLength = 2;
            public const int ModelMaxLength = 50;

            public const int ImageUrlMinLength = 3;
            public const int ImageUrlMaxLength = 2048;

            public const int RegistrationMinLength = 2;
            public const int RegistrationMaxLength = 20;

            public const int CurrentAddressMinLength = 2;
            public const int CurrentAddressMaxLength = 150;

            public const string PricePerDay = "1";

            public const int ColorMinLength = 2;
            public const int ColorMaxLength = 50;
        }

        public static class Scooter
        {
            public const int BrandMinLength = 2;
            public const int BrandMaxLength = 50;

            public const int ImageUrlMinLength = 3;
            public const int ImageUrlMaxLength = 2048;

            public const string PricePerDay = "1";

            public const int CurrentAddressMinLength = 2;
            public const int CurrentAddressMaxLength = 150;
        }

        public static class Agent
        {
            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 20;
        }
    }
}

namespace NetFlow.Common.GlobalConstants
{
    public class UserConstants
    {
        //Register User Constants
        public const int MIN_USERNAME_LENGTH = 3;
        public const int MAX_USERNAME_LENGTH = 10;

        public const int MIN_BIRTHDATE_YEAR = 1900;
        public const int MAX_BIRTHDATE_YEAR = 2019;

        public const string REGEX_USERNAME = "[a-zA-z0-9-.*/_]+";

        public const string USERNAME_LENGHT_ERROR_MESSAGE = "The {0} must be at least {1} characters long!";

        public const string REGISTER_USER_FIRST_NAME = "First Name";
        public const string REGISTER_USER_LAST_NAME = "First Name";

        public const string REGISTER_USER_BIRTHDAY = "Birth Date";

        public const string EMAIL = "Email";
        public const string PASSWORD = "Password";
        public const string CONFIRM_PASSWORD = "Confirm Password";

        public const string PASSWORD_ERROR_MESSAGE = "The {0} must be at least {2} and at max {1} characters long.";

        public const string PASSWORD_DO_NOT_MATCH = "The {0} must be at least {2} and at max {1} characters long.";
        public const string COMPARE_PASSWORD = "Password";



        //Login User Constants
        public const string ACCOUNT_USERNAME = "Username";
        public const string ACCOUNT_PASSWORD = "Password";

    }
}
    
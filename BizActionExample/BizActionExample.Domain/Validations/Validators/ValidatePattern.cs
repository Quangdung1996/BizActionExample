namespace BizActionExample.Domain.Validations
{
    public static class ValidatePattern
    {
        public static string MobilePhonePattern = "^1[0-9]{10}$";

        public static string IdCardPattern = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";
    }
}
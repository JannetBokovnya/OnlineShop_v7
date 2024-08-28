namespace OnlineShop.Library.Constants
{
    public static class IdConstants
    {
        public const string ApiScope = "OnlineShop.Api"; //взаимодействие услуги между собой
        public const string WebScope = "OnlineShop.Web"; //будут заходить пользователи через web  интерфейс

        //константы тип грантов, т.е способ каким образом мы получаем токен
        public const string GrantType_ClientCredentials = "client_credentials";
        public const string GrantType_Password = "password";
    }
}
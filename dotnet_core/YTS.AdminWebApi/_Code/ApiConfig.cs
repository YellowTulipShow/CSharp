namespace YTS.WebApi
{
    public static class ApiConfig
    {
        public const string APIRoute = "/api/[controller]/[action]";
        public const string CorsName = "YTSAllowSpecificOrigins";
        public const string SwaggerEndpointName = "AdminWebApi v1";
        public const string SwaggerEndpointUrl = "/swagger/v1/swagger.json";
        public const string APPSettingName_SwaggerInfo = "SwaggerInfo";
        public const string APPSettingName_JWTTokenManagement = "TokenManagement";
    }
}

namespace AdrexamAPI.Extensions
{
    public static class StringExtensions
    {
        public static string? GetRealUrl(this string str)
        {
            var config = ServiceProviderAccessor.ServiceProvider.GetService<IConfiguration>();
            return string.IsNullOrWhiteSpace(str) ? null : $"{config["HostUrl"]}/{str.TrimStart('/')}";
        }
    }
}
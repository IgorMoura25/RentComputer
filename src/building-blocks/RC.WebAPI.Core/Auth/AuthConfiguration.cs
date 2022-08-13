using Microsoft.AspNetCore.Builder;

namespace RC.WebAPI.Core.Auth
{
    public static class AuthConfiguration
    {
        public static void UseAuthConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}

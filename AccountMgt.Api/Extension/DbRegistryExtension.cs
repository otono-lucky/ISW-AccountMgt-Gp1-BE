using AccountMgt.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountMgt.Api.Extension
{
    public static class DbRegistryExtension
    {
        private static string GetRenderConnectionString()
        {
            // Get the Database URL from the ENV variables in Render
            string connectionUrl = $"postgres://isw_acctmgt_gp1_user:N909NWrqniL3xGLKQmD3DrUEUn85VcYE@dpg-cotavmvsc6pc73cgncp0-a/isw_acctmgt_gp1";

            // parse the connection string
            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

            var x = $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port=5432;" +
                   $"Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
            return x;
        }

        public static void AddDbContextAndConfigurations(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            var ConnectionString = GetRenderConnectionString();
            services.AddDbContextPool<AppDbContext>(options =>
            {
                string connStr;

                if (env.IsDevelopment())
                {
                    connStr = GetRenderConnectionString();
                    // options.UseNpgsql(connStr);
                    connStr = config.GetConnectionString("LocalConnection");
                    options.UseNpgsql(connStr);
                }
                else
                {
                    connStr = config.GetConnectionString("DefaultConnection");
                    options.UseNpgsql(connStr);
                }
            });
        }
    }
}

using LinguaNex.EntityFrameworkCore;
namespace LinguaNex.DataSeeders
{
    public static class DataSeederExtensions
    {
        public static async Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
        {
            var dbcontext = app.ApplicationServices.GetService<LinguaNexDbContext>();
            var dataSeeders = app.ApplicationServices.GetServices<IDataSeeder>();
            await dbcontext.Database.EnsureCreatedAsync();

            foreach (var dataSeeder in dataSeeders)
            {
                await dataSeeder.Seed();
            }
            return app;
        }
    }
}

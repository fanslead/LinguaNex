namespace LinguaNex.DataSeeders
{
    public static class DataSeederExtensions
    {
        public static async Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
        {
            var dataSeeders = app.ApplicationServices.GetServices<IDataSeeder>();

            foreach (var dataSeeder in dataSeeders)
            {
                await dataSeeder.Seed();
            }
            return app;
        }
    }
}

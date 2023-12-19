using Wheel.DependencyInjection;

namespace LinguaNex.DataSeeders
{
    public interface IDataSeeder : ITransientDependency
    {
        Task Seed(CancellationToken cancellationToken = default);
    }
}

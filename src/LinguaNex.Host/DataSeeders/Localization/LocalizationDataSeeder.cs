using LinguaNex.Domain;
using LinguaNex.Entities;

namespace LinguaNex.DataSeeders.Localization
{
    public class LocalizationDataSeeder(IBasicRepository<Projects, string> projectsRepository, IBasicRepository<Culture, string> cultureRepository)
        : IDataSeeder
    {
        public async Task Seed(CancellationToken cancellationToken = default)
        {
            if (!(await projectsRepository.AnyAsync(cancellationToken)))
            {
                await projectsRepository.InsertAsync(new Projects() { Id = "C96755D0-C22C-4DAD-9620-AF64C4C3D9D7", Name = "LinguaNex", Enalbe = true }, true);
            }
            if (!(await cultureRepository.AnyAsync(cancellationToken)))
            {
                await cultureRepository.InsertAsync(new Culture() { Id = "1186495029787492344", Name = "zh-Hans", ProjectId = "C96755D0-C22C-4DAD-9620-AF64C4C3D9D7" }, true);
                await cultureRepository.InsertAsync(new Culture() { Id = "1186495029787492345", Name = "en", ProjectId = "C96755D0-C22C-4DAD-9620-AF64C4C3D9D7" }, true);
            }
        }
    }
}

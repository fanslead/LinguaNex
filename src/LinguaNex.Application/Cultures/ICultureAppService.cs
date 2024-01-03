using LinguaNex.Cultures.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel.Core.Dto;
using Wheel.DependencyInjection;

namespace LinguaNex.Cultures
{
    public interface ICultureAppService : ITransientDependency
    {
        Task<R<CultureDto>> CreateAsync(CreateCultureDto dto);

        Task<Page<CultureDto>> PageListAsync(CulturePageRequest request);

        Task<R> DeleteAsync(long id);
    }
}

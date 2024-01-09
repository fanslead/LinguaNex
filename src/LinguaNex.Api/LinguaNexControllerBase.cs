using AutoMapper;
using LinguaNex.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Wheel.Core.Dto;
using Wheel.EventBus.Distributed;
using Wheel.EventBus.Local;
using Wheel.Utilities;

namespace Wheel.Controllers
{
    public abstract class LinguaNexControllerBase : ControllerBase
    {
        public IServiceProvider ServiceProvider { get; set; }
        public SnowflakeIdGenerator SnowflakeIdGenerator => LazyGetService<SnowflakeIdGenerator>();
        public GuidGenerator GuidGenerator => LazyGetService<GuidGenerator>();
        public IUnitOfWork UnitOfWork => LazyGetService<IUnitOfWork>();
        public IMapper Mapper => LazyGetService<IMapper>();
        public IMemoryCache MemoryCache => LazyGetService<IMemoryCache>();
        public IDistributedCache DistributedCache => LazyGetService<IDistributedCache>();
        public ILocalEventBus LocalEventBus => LazyGetService<ILocalEventBus>();
        public IDistributedEventBus DistributedEventBus => LazyGetService<IDistributedEventBus>();
        public IStringLocalizerFactory LocalizerFactory => LazyGetService<IStringLocalizerFactory>();

        public ILoggerFactory LoggerFactory => LazyGetService<ILoggerFactory>();

        public ILogger Logger => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance;

        private IStringLocalizer _stringLocalizer = null;

        public IStringLocalizer L
        {
            get
            {
                if (_stringLocalizer == null)
                    _stringLocalizer = LocalizerFactory.Create(null);
                return _stringLocalizer;
            }
        }

        public T LazyGetService<T>() where T : notnull
        {
            return new Lazy<T>(ServiceProvider.GetRequiredService<T>).Value;
        }

        protected R Success()
        {
            return new R();
        }
        protected R<T> Success<T>(T data)
        {
            return new R<T>(data);
        }
        protected Page<T> Page<T>(List<T> data, long total)
        {
            return new Page<T>(data, total);
        }
    }
}

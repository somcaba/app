using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Yacaba.Core.Validators {
    public abstract class IsExistingValidator<TEntity, TProperty> : AsyncPropertyValidator<TEntity, TProperty> where TEntity : class {

        protected abstract DbSet<TEntity> Set { get; set; }

        public override async Task<Boolean> IsValidAsync(ValidationContext<TEntity> context, TProperty value, CancellationToken cancellation) {
            TEntity? existing = await Set.FindAsync([value], cancellationToken: cancellation).ConfigureAwait(false);
            return existing != null;
        }

    }

    public class IsExistingValidatorOf<TEntity, TProperty, TDbContext> : IsExistingValidator<TEntity, TProperty> where TEntity : class where TDbContext : DbContext {
        private readonly IServiceProvider _serviceProvider;

        public IsExistingValidatorOf(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }
        protected override DbSet<TEntity> Set { get; set; } = default!;

        public override Task<Boolean> IsValidAsync(ValidationContext<TEntity> context, TProperty value, CancellationToken cancellation) {
            using (IServiceScope scope = _serviceProvider.CreateScope()) {
                TDbContext dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
                Set = dbContext.Set<TEntity>();
                return base.IsValidAsync(context, value, cancellation);
            }
        }

        public override String Name => $"IsExisting{typeof(TEntity).Name}";
    }

}

namespace Raindrop.Identity
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain.ReadModel.Models;
    using Domain.ReadModel.Repositories;

    using Microsoft.AspNetCore.Identity;

    using Utility;

    public class UserAccountStore :
        IUserPasswordStore<UserAccount>,
        IUserEmailStore<UserAccount>
    {
        private readonly IRepository<UserReadModel> _userRepository;

        public UserAccountStore(IRepository<UserReadModel> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<IdentityResult> CreateAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.Run(() =>
            {
                _userRepository.Add(new UserReadModel(Guid.NewGuid(), user.Username, user.PasswordHash));

                return IdentityResult.Success;
            });

        public Task<IdentityResult> DeleteAsync(UserAccount user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<UserAccount> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccount> FindByIdAsync(string userId, CancellationToken cancellationToken) =>
            _userRepository
                .Where(x => x.Id.ToString().Equals(userId, StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault()
                .Map(UserAccountFromReadModel)
                .Map(Task.FromResult);

        public Task<UserAccount> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) =>
            _userRepository
                .Where(x => NormalizeUserName(x.Username).Equals(normalizedUserName, StringComparison.Ordinal))
                .SingleOrDefault()
                .Map(UserAccountFromReadModel)
                .Map(Task.FromResult);

        public Task<string> GetEmailAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.FromResult(string.Empty);

        public Task<bool> GetEmailConfirmedAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.FromResult(true);

        public Task<string> GetNormalizedEmailAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.FromResult(string.Empty);

        public Task<string> GetNormalizedUserNameAsync(UserAccount user, CancellationToken cancellationToken) =>
            user.Username
            .Map(NormalizeUserName)
            .Map(Task.FromResult);

        public Task<string> GetPasswordHashAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.FromResult(user.PasswordHash);

        public Task<string> GetUserIdAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.FromResult(user.Id.ToString());

        public Task<string> GetUserNameAsync(UserAccount user, CancellationToken cancellationToken) =>
            user.Username
            .Map(Task.FromResult);

        public Task<bool> HasPasswordAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.FromResult(user.PasswordHash != null);

        public Task SetEmailAsync(UserAccount user, string email, CancellationToken cancellationToken) =>
            Task.FromResult(0);

        public Task SetEmailConfirmedAsync(UserAccount user, bool confirmed, CancellationToken cancellationToken) =>
            Task.FromResult(0);

        public Task SetNormalizedEmailAsync(UserAccount user, string normalizedEmail, CancellationToken cancellationToken) =>
            Task.FromResult(0);

        public Task SetNormalizedUserNameAsync(UserAccount user, string normalizedName, CancellationToken cancellationToken) =>
            Task.Run(() => user.Username = normalizedName);

        public Task SetPasswordHashAsync(UserAccount user, string passwordHash, CancellationToken cancellationToken) =>
            Task.Run(() => user.PasswordHash = passwordHash);

        public Task SetUserNameAsync(UserAccount user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(UserAccount user, CancellationToken cancellationToken) =>
            Task.Run(() =>
                user
                .Map(UserReadModelFromAccount)
                .Tee(_userRepository.Update)
                .Map(x => IdentityResult.Success));

        private string NormalizeUserName(string username) =>
            username.ToUpperInvariant();

        private static UserAccount UserAccountFromReadModel(UserReadModel readModel) =>
            readModel
            ?.Map(x => new UserAccount(x.Username, x.PasswordHash));

        private static UserReadModel UserReadModelFromAccount(UserAccount account) =>
            account
            ?.Map(x => new UserReadModel(x.Id, x.Username, x.PasswordHash));
    }
}

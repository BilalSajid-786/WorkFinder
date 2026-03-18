using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;

namespace WorkFinder.RepositoryContracts
{
    /// <summary>
    /// Interface for Subscription Repository
    /// </summary>
    public interface ISubscriptionRepository
    {
        Task<UserSubscription> SaveUserSubscriptionInfo(UserSubscription userSubscription);

        Task UpdateUserSubscriptionInfo(Guid userId,string customerId, string subscriptionStatus, string invoiceStatus,
            string accessStatus,DateTime updatedAt);

        Task<string?> GetSubscriptionIdByUserId(Guid userId);

        Task UpdateSubscriptionById(string subscriptionId, bool cancelAtPeriodEnd);
    }
}

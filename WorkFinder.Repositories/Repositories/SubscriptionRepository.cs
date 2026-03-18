using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFinder.Entities.Entities;
using WorkFinder.Repositories.DbContext;
using WorkFinder.RepositoryContracts;
using static WorkFinder.Entities.Entities.SystemSeeding.SystemPermissions;

namespace WorkFinder.Repositories.Repositories
{
    /// <summary>
    /// Subscription Repository Implementation
    /// </summary>
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly DapperDbContext _dapperDbContext;
        public SubscriptionRepository(DapperDbContext dapperDbContext)
        {
            _dapperDbContext = dapperDbContext;
        }

        /// <summary>
        /// Get Subscription By Id. This will be used to get the subscription info for the user when they login to the application and check their subscription status.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string?> GetSubscriptionIdByUserId(Guid userId)
        {
            using var connection = _dapperDbContext.CreateConnection();

            var sql = "[GetSubscriptionIdByUserId]";

            //procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);


            return await connection.ExecuteScalarAsync<string>(sql, parameters, 
                commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Save User Subscription Info in the db
        /// </summary>
        /// <param name="userSubscription"></param>
        /// <returns></returns>
        public async Task<UserSubscription> SaveUserSubscriptionInfo(UserSubscription userSubscription)
        {
            using var connection = _dapperDbContext.CreateConnection();

            //procedure name
            var sql = "[InsertUserSubscriptionInfo]";

            //procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userSubscription.UserId);
            parameters.Add("@StripeCustomerId", userSubscription.StripeCustomerId);
            parameters.Add("@StripeSubscriptionId", userSubscription.StripeSubscriptionId);
            parameters.Add("@StripePaymentMethodId", userSubscription.StripePaymentMethodId);
            parameters.Add("@SubscriptionStatus", userSubscription.SubscriptionStatus);
            parameters.Add("@InvoiceStatus", userSubscription.InvoiceStatus);
            parameters.Add("@AccessStatus", userSubscription.AccessStatus);
            parameters.Add("@TrialStart", userSubscription.TrialStart);
            parameters.Add("@TrialEnd", userSubscription.TrialEnd);
            parameters.Add("@CurrentPeriodStart", userSubscription.CurrentPeriodStart);
            parameters.Add("@CurrentPeriodEnd", userSubscription.CurrentPeriodEnd);
            parameters.Add("@Amount", userSubscription.Amount);
            parameters.Add("@Currency", userSubscription.Currency);
            parameters.Add("@PromoCode", userSubscription.PromoCode);
            parameters.Add("@CreatedAt", userSubscription.CreatedAt);
            parameters.Add("@UpdatedAt", userSubscription.UpdatedAt);

            await connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
            return userSubscription;
        }

        /// <summary>
        /// Update subscription by Id
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        public async Task UpdateSubscriptionById(string subscriptionId, bool cancelAtPeriodEnd)
        {
            using var connection = _dapperDbContext.CreateConnection();

            var sql = "[UpdateSubscriptionById]";

            var parameters = new DynamicParameters();
            parameters.Add("@SubscriptionId", subscriptionId);
            parameters.Add("@CancelAtPeriodEnd", cancelAtPeriodEnd);

            await connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        /// <summary>
        /// Update User Subscription Info in the db based on the subscription status received from the webhook. This will be used to update the subscription status in the db when the payment is successful or failed.
        /// </summary>
        /// <param name="subscriptionStatus"></param>
        /// <returns></returns>
        public async Task UpdateUserSubscriptionInfo(Guid userId, string customerId, string subscriptionStatus, string invoiceStatus,
            string accessStatus, DateTime updatedAt)
        {
            try
            {
                using var connection = _dapperDbContext.CreateConnection();

                var sql = "[UpdateUserSubscriptionInfo]";

                //procedure parameters
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                parameters.Add("@CustomerId", customerId);
                parameters.Add("@SubscriptionStatus", subscriptionStatus);
                parameters.Add("@InvoiceStatus", invoiceStatus);
                parameters.Add("@AccessStatus", accessStatus);
                parameters.Add("@UpdatedAt", updatedAt);


                await connection.ExecuteAsync(sql, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}

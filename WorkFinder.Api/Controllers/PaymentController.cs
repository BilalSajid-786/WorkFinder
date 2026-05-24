using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Stripe;
using WorkFinder.ServiceContracts;
using WorkFinder.ServiceContracts.DTOs.Response;
using WorkFinder.ServiceContracts.DTOs.Subscription;

namespace WorkFinder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ResponseDto _responseDto;

        public PaymentController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
            _responseDto = new ResponseDto();
        }

        /// <summary>
        /// Create Subscription with promo code trial or immediate charge
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult<ResponseDto>> CreateSubscription(CreateSubscriptionRequestDto request)
        {
            try
            {
               var subscriptionInfo = await _subscriptionService.CreateSubscriptionAsync(request);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Subscription created successfully.";
                _responseDto.Result = subscriptionInfo;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        /// <summary>
        /// Create Subscription with promo code trial or immediate charge
        /// </summary>
        [HttpPost("checkout")]
        public async Task<ActionResult<ResponseDto>> CreateCheckOutSession(CreateSubscriptionRequestDto request)
        {
            try
            {
                var subscriptionInfo = await _subscriptionService.CreateCheckoutSubscriptionAsync(request);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "CheckoutSession created successfully.";
                _responseDto.Result = subscriptionInfo;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }

        [HttpPost("cancel")]
        public async Task<ActionResult<ResponseDto>> CancelSubscription(CancelSubscriptionRequestDto cancelSubscriptionRequestDto)
        {
            try
            {
                var subscription = new SubscriptionService();
                var updatedSubscription = await subscription.UpdateAsync(
                cancelSubscriptionRequestDto.SubscriptionId,
                new SubscriptionUpdateOptions
                {
                    CancelAtPeriodEnd = true
                });

                var subscriptionDetails = await subscription.GetAsync(cancelSubscriptionRequestDto.SubscriptionId);
                await _subscriptionService.UpdateSubscriptionAsync(subscriptionDetails.Id,true);
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Subscription cancelled successfully.";
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}
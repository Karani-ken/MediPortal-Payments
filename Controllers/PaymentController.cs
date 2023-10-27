using MediPortal_Payments.Models.Dtos;
using MediPortal_Payments.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MediPortal_Payments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ResponseDto _response = new ResponseDto();

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
            //_response = new ResponseDto();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddPayment(PaymentRequestDto paymentRequest)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var userId = Guid.Parse(userIdClaim.Value);
                var response = await _paymentService.CreatePayment(userId,paymentRequest);

                if(response != null)
                {
                    _response.IsSuccess = true;
                    _response.obj = response;
                }
                else
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.InnerException.Message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [HttpPost("StripePayment")]
        public async Task<ActionResult<ResponseDto>> StripePayment(StripeRequestDto stripeRequestDto)
        {
            try
            {
                var response = await _paymentService.StripePayment(stripeRequestDto);
                if (response != null)
                {
                    _response.IsSuccess = true;
                    _response.obj = response;
                }
                else
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.InnerException.Message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        [HttpPost("ValidatePayment")]
        public async Task<ActionResult<ResponseDto>> ValidPayment(Guid PaymentId)
        {
            try
            {
               var response = await _paymentService.ValidatePayment(PaymentId);
                _response.obj = response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.InnerException.Message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
         [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetPayments()
        {
            try
            {
               var response = await _paymentService.GetPayments();
                _response.obj = response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.InnerException.Message;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}

using BankApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Interfaces
{
    public interface IPaymentService
    {
        PaymentRequestResponseDto CreatePaymentTransaction(PaymentRequestDto request);
        Task<PaymentResultDto> AuthorizePayment(PayWithCardDto dto);
        Task<TransBankPaymentResponseDto> Redirect(TransBankPaymentRequestDto dto);
        Task<TransBankPaymentResponseDto> ProcessExternalPayment(TransBankPaymentRequestDto request);
    }
}

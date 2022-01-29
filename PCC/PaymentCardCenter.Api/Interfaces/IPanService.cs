using PaymentCardCenter.Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCenter.Api.Interfaces
{
    public interface IPanService
    {
        Task<TransBankPaymentResponseDto> RedirectPaymentToIssuer(TransBankPaymentRequestDto request);
    }
}

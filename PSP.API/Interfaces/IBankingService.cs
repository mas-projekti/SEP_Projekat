using PSP.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Interfaces
{
    public interface IBankingService
    {
        Task<BankPaymentRequestResponseDto> InitiateBankPayment(Guid transactionID);
    }
}

using Cashbox.Application.DTOs.CashRegisterMovement;
using Cashbox.Application.DTOs.CashRegisterSession;
using Cashbox.Application.DTOs.CashRegisterSessionOrder;
using SharedKernel.Util;

namespace Cashbox.Application.UseCases
{
    public interface ICashRegisterService
    {
        // Session
        Task<Result<IReadOnlyList<CashRegisterSessionDto>>> GetAllAsync();
        Task<Result<CashRegisterSessionDto>> GetSessionByIdAsync(int sessionId);
        Task<Result<IReadOnlyList<CashRegisterSessionDto>>> GetSessionsByDateRangeAsync(DateTime from, DateTime to);
        Task<Result<int>> OpenSessionAsync(OpenCashRegisterSessionDto request);
        Task<Result> CloseSessionAsync(CloseCashRegisterSessionDto request);

        // Movements
        Task<Result<IReadOnlyList<CashRegisterMovementDto>>> GetMovementsBySessionIdAsync(int sessionId);
        Task<Result<int>> AddMovementAsync(CreateCashRegisterMovementDto request);        
        
        // Orders
        Task<Result<IReadOnlyList<CashRegisterSessionOrderDto>>> GetOrdersBySessionIdAsync(int sessionId);
    }
}

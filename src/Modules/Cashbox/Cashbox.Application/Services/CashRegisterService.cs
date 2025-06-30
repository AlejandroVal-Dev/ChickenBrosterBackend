using Cashbox.Application.DTOs.CashRegisterMovement;
using Cashbox.Application.DTOs.CashRegisterSession;
using Cashbox.Application.DTOs.CashRegisterSessionOrder;
using Cashbox.Application.UseCases;
using Cashbox.Application.Util;
using Cashbox.Domain.Entities;
using Cashbox.Domain.Enums;
using SharedKernel.Util;

namespace Cashbox.Application.Services
{
    public class CashRegisterService : ICashRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CashRegisterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<CashRegisterSessionDto>>> GetAllAsync()
        {
            var sessions = await _unitOfWork.Sessions.GetAllAsync();
            var result = sessions.Select(session => new CashRegisterSessionDto
            {
                Id = session.Id,
                OpenedAt = session.OpenedAt,
                ClosedAt = session.ClosedAt,
                OpenedByUserId = session.OpenedByUserId,
                ClosedByUserId = session.ClosedByUserId,
                InitialAmount = session.InitialAmount,
                ExpectedAmount = session.ExpectedAmount,
                CountedAmount = session.CountedAmount,
                Difference = session.Difference,
                Status = session.Status.ToString()
            }).ToList();

            return Result<IReadOnlyList<CashRegisterSessionDto>>.Success(result);
        }

        public async Task<Result<CashRegisterSessionDto>> GetSessionByIdAsync(int sessionId)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(sessionId);
            if (session == null)
                return Result<CashRegisterSessionDto>.Failure("Session not found.");

            var dto = new CashRegisterSessionDto
            {
                Id = session.Id,
                OpenedAt = session.OpenedAt,
                ClosedAt = session.ClosedAt,
                OpenedByUserId = session.OpenedByUserId,
                ClosedByUserId = session.ClosedByUserId,
                InitialAmount = session.InitialAmount,
                ExpectedAmount = session.ExpectedAmount,
                CountedAmount = session.CountedAmount,
                Difference = session.Difference,
                Status = session.Status.ToString()
            };

            return Result<CashRegisterSessionDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<CashRegisterSessionDto>>> GetSessionsByDateRangeAsync(DateTime from, DateTime to)
        {
            var sessions = await _unitOfWork.Sessions.GetByDateRangeAsync(from, to);
            var result = sessions.Select(session => new CashRegisterSessionDto
            {
                Id = session.Id,
                OpenedAt = session.OpenedAt,
                ClosedAt = session.ClosedAt,
                OpenedByUserId = session.OpenedByUserId,
                ClosedByUserId = session.ClosedByUserId,
                InitialAmount = session.InitialAmount,
                ExpectedAmount = session.ExpectedAmount,
                CountedAmount = session.CountedAmount,
                Difference = session.Difference,
                Status = session.Status.ToString()
            }).ToList();

            return Result<IReadOnlyList<CashRegisterSessionDto>>.Success(result);
        }

        public async Task<Result<int>> OpenSessionAsync(OpenCashRegisterSessionDto request)
        {
            var existingSession = await _unitOfWork.Sessions.GetOpenSessionByUserAsync(request.OpenedByUserId);
            if (existingSession is null)
                return Result<int>.Failure("User has a cashbox session active.");

            var session = new CashRegisterSession(request.OpenedByUserId, request.InitialAmount);

            await _unitOfWork.Sessions.AddAsync(session);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(session.Id);
        }

        public async Task<Result> CloseSessionAsync(CloseCashRegisterSessionDto request)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(request.SessionId);
            if (session is null)
                return Result.Failure("Cashbox session not found.");

            if (session.Status != CashRegisterSessionStatus.Open)
                return Result.Failure("Cashbox session is not open.");

            session.Close(request.ClosedByUserId, request.CountedAmount);

            if (request.OrderIds != null && request.OrderIds.Any())
            {
                var sessionOrders = request.OrderIds.Select(orderId =>
                    new CashRegisterSessionOrder(session.Id, orderId)).ToList();

                await _unitOfWork.Orders.AddRangeAsync(sessionOrders);
                await _unitOfWork.CommitAsync();
            }

            await _unitOfWork.Sessions.UpdateAsync(session);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result<IReadOnlyList<CashRegisterMovementDto>>> GetMovementsBySessionIdAsync(int sessionId)
        {
            var movements = await _unitOfWork.Movements.GetBySessionIdAsync(sessionId);
            var result = movements.Select(m => new CashRegisterMovementDto
            {
                Id = m.Id,
                Type = m.Type,
                Amount = m.Amount,
                Description = m.Description,
                MadeByUserId = m.MadeByUserId,
                CreatedAt = m.CreatedAt
            }).ToList();

            return Result<IReadOnlyList<CashRegisterMovementDto>>.Success(result);
        }

        public async Task<Result<int>> AddMovementAsync(CreateCashRegisterMovementDto request)
        {
            var session = await _unitOfWork.Sessions.GetByIdAsync(request.SessionId);
            if (session is null)
                return Result<int>.Failure("Cashbox session not found.");

            if (session.Status != CashRegisterSessionStatus.Open)
                return Result<int>.Failure("Cashbox session is not open.");

            var movement = new CashRegisterMovement(
                request.SessionId,
                request.Type,
                request.Amount,
                request.Description,
                request.MadeByUserId);

            await _unitOfWork.Movements.AddAsync(movement);
            await _unitOfWork.CommitAsync();

            session.AddMovementAmount(request.Amount, request.Type == CashRegisterMovementType.Income);
            await _unitOfWork.Sessions.UpdateAsync(session);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(movement.Id);
        }

        public async Task<Result<IReadOnlyList<CashRegisterSessionOrderDto>>> GetOrdersBySessionIdAsync(int sessionId)
        {
            var orders = await _unitOfWork.Orders.GetBySessionIdAsync(sessionId);
            var result = orders.Select(o => new CashRegisterSessionOrderDto
            {
                Id = o.Id,
                SessionId = o.SessionId,
                OrderId = o.OrderId
            }).ToList();

            return Result<IReadOnlyList<CashRegisterSessionOrderDto>>.Success(result);
        }
    }
}

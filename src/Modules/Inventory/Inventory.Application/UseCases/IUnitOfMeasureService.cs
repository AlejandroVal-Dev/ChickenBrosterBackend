using Inventory.Application.DTOs.UnitOfMeasure;
using SharedKernel.Util;

namespace Inventory.Application.UseCases
{
    public interface IUnitOfMeasureService
    {
        Task<Result<IReadOnlyList<UnitOfMeasureDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<UnitOfMeasureDto>>> GetActivesAsync();
        Task<Result<UnitOfMeasureDto>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<UnitOfMeasureDto>>> SearchByNameAsync(string name);
        Task<Result<int>> CreateAsync(CreateUnitOfMeasureDto request);
        Task<Result> UpdateAsync(UpdateUnitOfMeasureDto request);
        Task<Result> DeleteAsync(int id);
        Task<Result> RestoreAsync(int id);
    }
}

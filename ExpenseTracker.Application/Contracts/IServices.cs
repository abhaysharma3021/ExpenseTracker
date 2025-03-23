using ExpenseTracker.Shared;

namespace ExpenseTracker.Application.Contracts;

public interface IService<TReadDto, TCreateDto, TUpdateDto>
{
    Task<Result<TReadDto?>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<TReadDto>>> GetAllAsync();
    Task<Result<TReadDto>> CreateAsync(TCreateDto createDto);
    Task<Result> UpdateAsync(Guid id, TUpdateDto updateDto);
    Task<Result> DeleteAsync(Guid id);
}
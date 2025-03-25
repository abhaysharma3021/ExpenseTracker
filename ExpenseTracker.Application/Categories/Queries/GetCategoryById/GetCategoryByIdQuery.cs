using AutoMapper;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.Category;
using MediatR;

namespace ExpenseTracker.Application.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDto>>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            if(category == null)
            {
                return Result<CategoryDto>.Failure($"Category with ID: {request.Id} not found.");
            }
            var mappedDto = _mapper.Map<CategoryDto>(category);
            return Result<CategoryDto>.Success(mappedDto);
        }
        catch (Exception ex)
        {
            return Result<CategoryDto>.Failure("Something went wrong");
        }
    }
}
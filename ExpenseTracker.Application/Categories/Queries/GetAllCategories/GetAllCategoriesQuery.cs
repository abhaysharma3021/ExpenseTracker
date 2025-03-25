using AutoMapper;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using ExpenseTracker.Shared.DTOs.Category;
using MediatR;

namespace ExpenseTracker.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<Result<IEnumerable<CategoryDto>>>;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<CategoryDto>>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            if(categories == null)
            {
                return Result<IEnumerable<CategoryDto>>.Failure("Categories not found");
            }

            var mappedDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Result<IEnumerable<CategoryDto>>.Success(mappedDto);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CategoryDto>>.Failure("Something went wrong");
        }
    }
}
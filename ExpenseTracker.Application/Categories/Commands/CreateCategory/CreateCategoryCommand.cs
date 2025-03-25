using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using MediatR;

namespace ExpenseTracker.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name) : IRequest<Result>;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await _categoryRepository.FindAsync(c => c.Name.Trim() == request.Name.Trim());
            if(exists != null)
            {
                return Result.Failure($"Category with name: {request.Name} already exists");
            }

            var entity = new Category(request.Name);
            var response = await _categoryRepository.AddAsync(entity);

            return response;
        }
        catch(Exception ex)
        {
            return Result.Failure("Something went wrong");
        }
    }
}
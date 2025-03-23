using AutoMapper;
using ExpenseTracker.Application.Contracts;
using ExpenseTracker.Application.Errors;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Shared;
using Microsoft.Extensions.Logging;
using System;

namespace ExpenseTracker.Application.Services;

public class Service<T, TRead, TCreate, TUpdate> : IService<TRead, TCreate, TUpdate> where T : class
{
    protected readonly IRepository<T> _repository;
    protected readonly IMapper _mapper;
    protected readonly ILogger<Service<T, TRead, TCreate, TUpdate>> _logger;

    public Service(
        IRepository<T> repository,
        IMapper mapper,
        ILogger<Service<T, TRead, TCreate, TUpdate>> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public virtual async Task<Result<TRead?>> GetByIdAsync(Guid id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} with ID {id} not found.");

            var mappedEntity = _mapper.Map<TRead>(entity);
            return Result<TRead?>.Success(mappedEntity);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return Result<TRead?>.Failure($"{ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving {typeof(T).Name} with ID {id}.");
            return Result<TRead?>.Failure($"An error occured while fetching {typeof(T).Name}.");
        }
    }

    public virtual async Task<Result<IEnumerable<TRead>>> GetAllAsync()
    {
        try
        {
            var entities = await _repository.GetAllAsync();
            var mappedEntity = _mapper.Map<IEnumerable<TRead>>(entities);
            return Result<IEnumerable<TRead>>.Success(mappedEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving all {typeof(T).Name}s.");
            return Result<IEnumerable<TRead>>.Failure($"An error occured while fetching the list of {typeof(T).Name}.");
        }
    }

    public virtual async Task<Result<TRead>> CreateAsync(TCreate createDto)
    {
        try
        {
            // Check if entity exists
            if (await EntityExists(createDto))
            {
                return Result<TRead>.Failure($"{typeof(T).Name} already exists.");
            }

            // Map DTO to entity
            var entity = _mapper.Map<T>(createDto);
            await _repository.AddAsync(entity);

            // Map entity back to read DTO
            var mappedEntity = _mapper.Map<TRead>(entity);

            return Result<TRead>.Success(mappedEntity);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while creating a {typeof(T).Name}.");
            return Result<TRead>.Failure($"An error occured while creating {typeof(T).Name}.");
        }
    }

    public virtual async Task<Result> UpdateAsync(Guid id, TUpdate updateDto)
    {
        try
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                throw new NotFoundException($"{typeof(T).Name} with ID {id} not found.");

            _mapper.Map(updateDto, existingEntity);
            return await _repository.UpdateAsync(existingEntity);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return Result.Failure($"{ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating {typeof(T).Name} with ID {id}.");
            return Result.Failure($"An error occured while updating {typeof(T).Name}.");
        }
    }

    public virtual async Task<Result> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} with ID {id} was not found.");

            return await _repository.DeleteAsync(id);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning(ex.Message);
            return Result.Failure($"{ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting {typeof(T).Name} with ID {id}.");
            return Result.Failure($"An error occured while deleting {typeof(T).Name}.");
        }
    }

    // Check if Entity Exists
    protected virtual async Task<bool> EntityExists(TCreate createDto)
    {
        // Default implementation - override in derived classes
        return false;
    }
}

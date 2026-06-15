using AutoMapper;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class GenericService<TDto, TEntity>
        : IGenericService<TDto>
        where TDto : class
        where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(
            IGenericRepository<TEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<List<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllList();

            return _mapper.Map<List<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null)
                return null;

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(
            int id,
            TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            await _repository.UpdateAsync(id, entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
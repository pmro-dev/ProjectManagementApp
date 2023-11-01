namespace Project_Main.Services.DTO.Builders
{
    public interface IEntityFactory<TModel, TDto>
    {
        public TModel CreateModel(TDto dto);
        public TDto CreateDto(TModel model);
    }
}

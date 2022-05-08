namespace Application.Services.Categories.Queries.GetCategories
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }
        public ParentCategoryDto Parent { get; set; }
    }
}

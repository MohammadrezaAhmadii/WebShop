namespace Application.Services.Categories.Commands.AddNewCategory
{
    public class AddNewCategoryDto
    {
        public long? ParentId{ get; set; }
        public string Name{ get; set; }
    }
}

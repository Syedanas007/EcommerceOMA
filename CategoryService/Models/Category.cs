namespace CategoryService.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public int Orders { get; set; }
    }
}

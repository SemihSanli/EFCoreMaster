namespace EFStoreFlow.Entities
{
    public class ToDo
    {
        public  int TodoId { get; set; }
        public string TodoDescription { get; set; }
        public bool TodoStatus { get; set; }
        public string? Priority { get; set; }
    }
}

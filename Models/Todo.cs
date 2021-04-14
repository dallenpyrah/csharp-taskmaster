namespace taskmaster.Models
{
    public class Todo
    {
        public int Id { get; set;}
        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }
    }
}
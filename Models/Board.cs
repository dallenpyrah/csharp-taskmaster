namespace taskmaster.Models
{
    public class Board
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Open { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }
    }

    public class BoardMemberViewModel : Board
    {
        public int BoardMemberId { get; set; }
    }
}
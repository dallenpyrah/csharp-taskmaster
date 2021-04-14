namespace taskmaster.Models
{
    public class BoardMember
    {
        public int Id { get; set; }

        public string MemberId { get; set; }

        public int BoardId { get; set; }

        public string CreatorId { get; set; }
    }
}
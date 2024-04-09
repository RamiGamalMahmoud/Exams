using Postgrest.Attributes;
using Postgrest.Models;
using System;

namespace Exams.Models
{
    [Table("questions")]
    public class Question : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }
        
        [Column("text")]
        public string Text { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}

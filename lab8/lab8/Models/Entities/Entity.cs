using SQLite;

namespace lab8.Models.Entities
{
    public class Entity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}

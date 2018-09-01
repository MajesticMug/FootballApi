using System.ComponentModel.DataAnnotations;

namespace Sports.Football.Data.Model.Base
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
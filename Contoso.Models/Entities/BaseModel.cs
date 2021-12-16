using System.ComponentModel.DataAnnotations.Schema;

namespace Contoso.Models.Entities
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}

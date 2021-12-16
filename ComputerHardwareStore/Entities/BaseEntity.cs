using System.ComponentModel.DataAnnotations;

namespace ComputerHardwareStore.Entities
{
    public class BaseEntity
    {
        [Key] public int Id { get; set; }
    }
}

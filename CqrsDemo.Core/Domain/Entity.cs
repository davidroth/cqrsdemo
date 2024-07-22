using Fusonic.Extensions.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CqrsDemo.Core.Domain
{
    public abstract class Entity : IEntity<int>
    {
        protected Entity()
        { }

        protected Entity(int id) => Id = id;

        [Key]
        public int Id { get; set; }

        public bool HasId() => Id != 0;

        [NotMapped]
        public ICollection<INotification> Events { get; } = new List<INotification>();
    }
}
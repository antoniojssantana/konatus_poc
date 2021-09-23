using System;
using konatus.business.Enums;

namespace konatus.business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public StatusDefault Status { get; set; }
    }
}
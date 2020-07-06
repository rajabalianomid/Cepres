using System;

namespace Cepres.Common.Domain
{
    public abstract partial class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
        public DateTime CreateUtc { get; set; }
    }
}

using Challenge.Domain.Entities.Base;
using System;

namespace Challenge.Domain.Entities;

public class Transfer : BaseEntity
{
    public Guid PayerId { get; set; }

    public Guid PayeeId { get; set; }

    public decimal Value { get; set; }
}

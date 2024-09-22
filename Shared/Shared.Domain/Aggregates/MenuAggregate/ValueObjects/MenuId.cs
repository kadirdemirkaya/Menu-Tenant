using Shared.Domain.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Aggregates.MenuAggregate.ValueObjects
{
    public sealed class MenuId : ValueObject
    {
        public Guid Id { get; }


        public MenuId(Guid id)
        {
            Id = id;
        }

        public static MenuId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static MenuId Create(Guid Id)
        {
            return new MenuId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}

using Shared.Domain.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Aggregates.UserAggregate.ValueObjects
{
    public sealed class ConnectionPoolId : ValueObject
    {
        public Guid Id { get; }


        public ConnectionPoolId(Guid id)
        {
            Id = id;
        }

        public static ConnectionPoolId CreateUnique()
        {
            return new(Guid.NewGuid());
        }

        public static ConnectionPoolId Create(Guid Id)
        {
            return new ConnectionPoolId(Id);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Shared.Domain.Aggregates.MenuAggregate.ValueObjects
{
    [Owned]
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public Address()
        {
            
        }

        public Address(string street,string city,string county)
        {
            Street = street;
            City = city;
            Country = county;
        }

        public static Address Create(string street, string city, string county)
            => new(street, city, county);
    }
}

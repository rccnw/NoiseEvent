using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    // This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
    // Using non-generic integer types for simplicity and to ease caching logic
    public class BaseEntity
    {
        // Note: the presence of 'Id' property will cause EF to generate a Primary Key
        public int Id { get; set; }
    }
}

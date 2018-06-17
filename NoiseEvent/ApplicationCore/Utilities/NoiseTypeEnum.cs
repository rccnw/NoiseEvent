using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;

namespace ApplicationCore.Utilities
{
    // see 'Smarter Enumerations' (Strongly Typed Enum Class)
    // http://www.weeklydevtips.com/014
    // https://github.com/rccnw/SmartEnum
    //
    // var allOptions = TestEnum.List;
    // var myEnum = TestEnum.FromString("One");
    // var myEnum = TestEnum.FromValue(1);
    // Console.WriteLine(TestEnum.One); // One (1)

    public class NoiseTypeEnum : SmartEnum<NoiseTypeEnum, string>
    {
        public static NoiseTypeEnum Unknown     = new NoiseTypeEnum(nameof(Unknown), "Unknown");
        public static NoiseTypeEnum Vehicle     = new NoiseTypeEnum(nameof(Vehicle), "Vehicle");
        public static NoiseTypeEnum Motorcycle  = new NoiseTypeEnum(nameof(Motorcycle), "Motorcycle");
        public static NoiseTypeEnum Siren       = new NoiseTypeEnum(nameof(Siren), "Siren");
        public static NoiseTypeEnum Dog         = new NoiseTypeEnum(nameof(Dog), "Dog");
        public static NoiseTypeEnum Animal      = new NoiseTypeEnum(nameof(Animal), "Animal");
        public static NoiseTypeEnum Person      = new NoiseTypeEnum(nameof(Person), "Person");
        public static NoiseTypeEnum Aircraft    = new NoiseTypeEnum(nameof(Aircraft), "Aircraft");
        public static NoiseTypeEnum Other       = new NoiseTypeEnum(nameof(Other), "Other");


        protected NoiseTypeEnum(string name, string value) : base(name, value)
        {
        }
    }
}

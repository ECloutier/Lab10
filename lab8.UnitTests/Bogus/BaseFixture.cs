using System;
using System.Collections.Generic;
using System.Text;

namespace lab8.UnitTests.Bogus
{
    public class BaseFixture
    {
        protected Fixture Fixture;
        public BaseFixture()
        {
            Fixture = new Fixture();
        }
    }
}
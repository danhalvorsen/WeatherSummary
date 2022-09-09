using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class FooTests
    {

        public interface IABC {

            float DoWork();
        }

        public class ABC : IABC
        {
            private readonly IFoo foo;

            public ABC(IFoo foo)
            {
                this.foo = foo;
            }

            public float DoWork()
            {
                return this.foo.Calc();
            }
        }

        [Test]
        public void SetUp()
        {

            var fooMock = new Mock<IFoo>();
            fooMock.Setup(m => m.Calc()).Returns(4);

            var sut = new ABC(fooMock.Object);

            sut.DoWork().Should().Be(4);
        }


        public interface IFoo
        {
            float Calc();
        }

        public class Foo : IFoo
        {
            virtual public float Calc()
            {
                return 1 + 1;
            }
        }

        public class Bla : Foo
        {
            public override float Calc()
            {
                return base.Calc();
            }
        }
    }
}

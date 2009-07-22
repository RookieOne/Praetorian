using NUnit.Framework;

namespace Foundation_Test.Contexts
{
    public abstract class ContextSpecification
    {
        [SetUp]
        protected void Setup()
        {
            Given();
            When();
        }

        protected virtual void Given()
        {
        }

        protected virtual void When()
        {
        }
    }
}
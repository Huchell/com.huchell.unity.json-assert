using NUnit.Framework;

namespace JsonAssert.Constraints.Tests
{
    [TestFixture]
    public sealed class JsonArrayConstraintTests
    {
        [Test]
        public void Succeeds_WhenActual_IsEmptyJsonArray()
        {
            var actual = "[]";
            Assert.That(actual, new JsonArrayConstraint());
        }

        [Test]
        public void Succeeds_WhenActual_IsJsonWithValue()
        {
            var actual = "[12]";
            Assert.That(actual, new JsonArrayConstraint());
        }

        [Test]
        public void Fails_WhenActual_IsAJsonObject()
        {
            var actual = "{}";
            Assert.That(actual, Is.Not.Matches(new JsonArrayConstraint()));
        }

        [Test]
        public void Fails_WhenActual_IsAnEmptyString()
        {
            var actual = "";
            Assert.That(actual, Is.Not.Matches(new JsonArrayConstraint()));
        }

        [Test]
        public void Fails_WhenActual_IsNull()
        {
            string actual = null;
            Assert.That(actual, Is.Not.Matches(new JsonArrayConstraint()));
        }

        [Test]
        public void Fails_WhenActual_IsNotAString()
        {
            var actual = 1;
            Assert.That(actual, Is.Not.Matches(new JsonArrayConstraint()));
        }
    }
}

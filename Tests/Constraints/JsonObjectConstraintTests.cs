using NUnit.Framework;

namespace JsonAssert.Constraints.Tests
{
    [TestFixture]
    public sealed class JsonObjectConstraintTests
    {
        [Test]
        public void Succeeds_WhenActual_IsAnEmptyJsonObject()
        {
            var actual = "{}";
            Assert.That(actual, new JsonObjectConstraint());
        }

        [Test]
        public void Succeeds_WhenActual_IsAJsonObjectWithProperty()
        {
            var actual = @"{ ""name"": ""huchell"" }";
            Assert.That(actual, new JsonObjectConstraint());
        }

        [Test]
        public void Fails_WhenActual_IsAJsonArray()
        {
            var actual = "[]";
            Assert.That(actual, Is.Not.Matches(new JsonObjectConstraint()));
        }

        [Test]
        public void Fails_WhenActual_IsAnEmptyString()
        {
            var actual = "";
            Assert.That(actual, Is.Not.Matches(new JsonObjectConstraint()));
        }

        [Test]
        public void Fails_WhenActual_IsNull()
        {
            string actual = null;
            Assert.That(actual, Is.Not.Matches(new JsonObjectConstraint()));
        }

        [Test]
        public void Fails_WhenActual_IsNotAString()
        {
            var actual = 1;
            Assert.That(actual, Is.Not.Matches(new JsonObjectConstraint()));
        }
    }
}

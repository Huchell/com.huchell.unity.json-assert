using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints.Tests
{
    [TestFixture]
    public sealed class JsonPropertyConstraintTests
    {
        private JsonPropertyConstraint validPropertyConstraint;

        [SetUp]
        public void SetUp()
        {
            this.validPropertyConstraint = new JsonPropertyConstraint("valid", new EqualConstraint("value"));
        }

        [Test]
        public void Succeeds_WhenActual_IsJsonObjectWithProperty()
        {
            var actual = @"{ ""valid"": ""value"" }";
            var result = this.validPropertyConstraint.ApplyTo(actual);
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public void Fails_WhenActual_IsJsonObjectWithoutProperty()
        {
            var actual = @"{ ""invalid"": ""value"" }";
            var result = this.validPropertyConstraint.ApplyTo(actual);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Fails_WhenPropertyValue_DoesNotMatch()
        {
            var actual = @"{ ""valid"":""nope"" }";
            var result = this.validPropertyConstraint.ApplyTo(actual);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Fails_WhenActual_IsJsonArray()
        {
            var actual = "[]";
            var result = this.validPropertyConstraint.ApplyTo(actual);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Fails_WhenActual_IsEmptyString()
        {
            var actual = "";
            var result = this.validPropertyConstraint.ApplyTo(actual);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Fails_WhenActual_IsNull()
        {
            string actual = null;
            var result = this.validPropertyConstraint.ApplyTo(actual);
            Assert.That(result.IsSuccess, Is.False);
        }

        [Test]
        public void Fails_WhenActual_IsNotAString()
        {
            var actual = 1;
            var result = this.validPropertyConstraint.ApplyTo(actual);
            Assert.That(result.IsSuccess, Is.False);
        }
    }
}

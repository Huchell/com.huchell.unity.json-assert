using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints.Tests
{
    [TestFixture]
    public sealed class JsonPathConstraintTests
    {
        [Test]
        public void Succeeds_WhenActual_IsJsonObjectWithPath()
        {
            var constraint = new JsonPathConstraint("valid", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("valid");

            var actual = @"{ ""valid"": ""value"" }";
            Assert.That(actual, constraint);
            Assert.That(actual, existsContraint);
        }

        [Test]
        public void Succeeds_WhenPath_HasPropertyDepth()
        {
            var constraint = new JsonPathConstraint("valid.property", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("valid.property");

            var actual = @"{ ""valid"": { ""property"": ""value"" } }";
            Assert.That(actual, constraint);
            Assert.That(actual, existsContraint);
        }

        [Test]
        public void Succeeds_WhenPath_UsesArray()
        {
            var constraint = new JsonPathConstraint("valid[0]", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("valid[0]");

            var actual = @"{ ""valid"": [ ""value"" ] }";
            Assert.That(actual, constraint);
            Assert.That(actual, existsContraint);
        }

        [Test]
        public void Succeeds_WhenActual_IsArray()
        {
            var constraint = new JsonPathConstraint("[0]", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("[0]");

            var actual = @"[ ""value"" ]";
            Assert.That(actual, constraint);
            Assert.That(actual, existsContraint);
        }

        [Test]
        public void Fails_WhenPath_IsNotValid()
        {
            var constraint = new JsonPathConstraint("[0.valid", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("[0.valid");

            var actual = @"[ ""valid"": ""value"" ]";
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsContraint));
        }

        [Test]
        public void Fails_WhenPath_DoesNotMatchActual()
        {
            var constraint = new JsonPathConstraint("invalid", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("invalid");

            var actual = @"{ ""valid"": ""value"" }";
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsContraint));
        }

        [Test]
        public void Fails_WhenActual_IsNotValidJson()
        {
            var constraint = new JsonPathConstraint("valid", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("valid");

            var actual = @"{ ""valid"": ""value"" ";
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsContraint));
        }

        [Test]
        public void Fails_WhenActual_IsNotAString()
        {
            var constraint = new JsonPathConstraint("valid", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("valid");

            var actual = 2;
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsContraint));
        }

        [Test]
        public void Fails_WhenActual_IsNull()
        {
            var constraint = new JsonPathConstraint("valid", new EqualConstraint("value"));
            var existsContraint = new JsonPathExistsConstraint("valid");

            string actual = null;
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsContraint));
        }
    }
}

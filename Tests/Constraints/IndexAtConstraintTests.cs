using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints.Tests
{
    [TestFixture]
    public sealed class IndexAtConstraintTests
    {
        [Test]
        public void Succeeds_WhenActual_IsListAtLeastOneElement()
        {
            var constraint = new IndexAtConstraint(0, new EqualConstraint("value"));
            var existsConstraint = new IndexAtExistsConstraint(0);

            var actual = new[] { "value" };
            Assert.That(actual, constraint);
            Assert.That(actual, existsConstraint);
        }

        [Test]
        public void Succeeds_WhenIndex_Exists()
        {
            var constraint = new IndexAtConstraint(1, new EqualConstraint("value1"));
            var existsConstraint = new IndexAtExistsConstraint(1);

            var actual = new[] { "value0", "value1" };
            Assert.That(actual, constraint);
            Assert.That(actual, existsConstraint);
        }

        [Test]
        public void Succeeds_WhenActual_IsJsonArray()
        {
            var constraint = new IndexAtConstraint(0, new EqualConstraint("value"));
            var existsConstraint = new IndexAtExistsConstraint(0);

            var actual = @"[ ""value"" ]";
            Assert.That(actual, constraint);
            Assert.That(actual, existsConstraint);
        }

        [Test]
        public void Fails_WhenActual_IsNonList()
        {
            var constraint = new IndexAtConstraint(0, new EqualConstraint("value"));
            var existsConstraint = new IndexAtExistsConstraint(0);

            var actual = 2;
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsConstraint));
        }

        [Test]
        public void Fails_WhenIndex_DoesNotExist()
        {
            var constraint = new IndexAtConstraint(1, new EqualConstraint("value0"));
            var existsConstraint = new IndexAtExistsConstraint(1);

            var actual = new[] { "value0" };
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsConstraint));
        }

        [Test]
        public void Fails_WhenActual_IsNull()
        {
            var constraint = new IndexAtConstraint(1, new EqualConstraint("value0"));
            var existsConstraint = new IndexAtExistsConstraint(1);

            IList actual = null;
            Assert.That(actual, Is.Not.Append(constraint));
            Assert.That(actual, Is.Not.Append(existsConstraint));
        }
    }
}

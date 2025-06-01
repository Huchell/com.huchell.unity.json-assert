using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonObjectConstraint : Constraint
    {
        public override string Description => "a json object {}";

        public override ConstraintResult ApplyTo(object actual)
        {
            if (actual is not string str)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Failure);
            }

            if (string.IsNullOrEmpty(str))
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Failure);
            }

            try
            {
                JObject.Parse(str);
            }
            catch (JsonReaderException)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Failure);
            }
            return new ConstraintResult(this, actual, ConstraintStatus.Success);
        }
    }
}

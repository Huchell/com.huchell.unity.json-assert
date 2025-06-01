using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonArrayConstraint : Constraint
    {
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
                var arr = JArray.Parse(str);
                return new ConstraintResult(this, arr, ConstraintStatus.Success);
            }
            catch (JsonReaderException)
            {
                return new ConstraintResult(this, actual, ConstraintStatus.Failure);
            }
        }

        public ConstraintExpression IndexAt(int index) => new ConstraintExpression().Append(new IndexAtOperator(index));
    }
}

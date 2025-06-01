using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class IndexAtExistsConstraint : Constraint
    {
        private readonly int index;

        public IndexAtExistsConstraint(int index)
            : base()
        {
            this.index = index;
        }

        public override ConstraintResult ApplyTo(object actual)
        {
            if (actual is string str)
            {
                try
                {
                    var arr = JArray.Parse(str);
                    return new IndexAtConstraintResult(this, actual, isSuccess: arr.Count > index);
                }
                catch (JsonReaderException)
                {
                }
            }

            if (actual is not IList list)
            {
                return new IndexAtConstraintResult(this, actual, isSuccess: false);
            }

            return new IndexAtConstraintResult(this, actual, isSuccess: list.Count > index);
        }
    }
}

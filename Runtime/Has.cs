using JsonAssert.Constraints;
using NUnit.Framework.Constraints;

namespace JsonAssert
{
    public class Has : NUnit.Framework.Has
    {
        public static ResolvableConstraintExpression JsonProperty(string name)
        {
            return new ConstraintExpression().Append(new JsonPropertyOperator(name));
        }

        public static ResolvableConstraintExpression JsonPath(string path)
        {
            return new ConstraintExpression().Append(new JsonPathOperator(path));
        }
    }
}

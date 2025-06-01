using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonPathExistsConstraint : Constraint
    {
        private readonly string path;

        public JsonPathExistsConstraint(string path)
        {
            this.path = path;
        }

        public override ConstraintResult ApplyTo(object actual)
        {
            if (actual is not string jsonStr)
            {
                return new JsonPathConstraintResult(this, actual, isSuccess: false);
            }

            var token = this.SafeParse(jsonStr);
            if (token is null)
            {
                return new JsonPathConstraintResult(this, actual, isSuccess: false);
            }

            var result = token.SelectToken(this.path);
            if (result is null)
            {
                return new JsonPathConstraintResult(this, actual, isSuccess: false);
            }

            return new JsonPathConstraintResult(this, actual, isSuccess: true);
        }

        private JToken SafeParse(string json)
        {
            try
            {
                return JToken.Parse(json);
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }
    }
}

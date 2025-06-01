using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace JsonAssert.Constraints
{
    public sealed class JsonPathConstraint : PrefixConstraint
    {
        private readonly string path;

        public JsonPathConstraint(string path, IResolveConstraint baseConstraint)
            : base(baseConstraint)
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

            var value = GetValueFromToken(result);
            var baseResult = this.BaseConstraint.ApplyTo(value);
            return new JsonPathConstraintResult(this, baseResult);
        }

        private object GetValueFromToken(JToken token)
        {
            return token.Type switch
            {
                JTokenType.Boolean => token.ToObject<bool>(),
                JTokenType.Integer => token.ToObject<int>(),
                JTokenType.Float => token.ToObject<float>(),
                JTokenType.String => token.ToObject<string>(),
                _ => (object)token,
            };
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

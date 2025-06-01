using JsonAssert.Constraints;

namespace JsonAssert.Unity
{
    public class Is : NUnit.Framework.Is
    {
        public static JsonObjectConstraint JsonObject => new();
        public static JsonArrayConstraint JsonArray => new();
    }
}

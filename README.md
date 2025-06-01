# JsonAssert

Unity package to make it easier to write tests for Json results.

## Usage

The package has been written for NUnits constraint-based Assert model so can be used with `Assert.That` and other constraints.

```cs
// Assert that json is an json object
Assert.That(@"{ ""property"": ""value"" }", Is.JsonObject);
// Assert that a json property exists
Assert.That(@"{ ""property"": ""value"" }", Has.JsonProperty("property"));
// Assert that the json property `property` has the value `value`
Assert.That(@"{ ""property"": ""value"" }", Has.JsonProperty("property").EqualTo("value"));

// Assert that json is an json array
Assert.That(@"[ ""value"" ]", Is.JsonArray);
// Assert that a value exists at index 0
Assert.That(@"[ ""value"" ]", Has.IndexAt(0));
// Assert that the value at index 0 has the value `value`
Assert.That(@"[ ""value"" ]", Has.IndexAt(0).EqualTo("value"));
```

## JsonPath

JsonAssert has support for using a JsonPath to write more advanced queries for more complex json objects.

```cs
string json = @"{ ""property0"": [ { ""property1"": ""value"" } ] }"
Assert.That(json, Has.JsonPath("property0[0].property1").EqualTo("value");
// This is equivalent to using the above JsonPath constraint with the other constraints
Assert.That(json, Has.JsonProperty("property0").IndexAt(0).JsonProperty("property1").EqualTo("value");
```


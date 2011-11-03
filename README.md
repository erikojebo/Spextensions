Spextensions
============
Spextensions is a set of extensions and utilities for RhinoMocks and NUnit.

The code is licensed under the MIT license, as described in the LICENSE file.

Spextensions.RhinoMocks
----------------------

Spextensions.RhinoMocks includes several extensions to the RhinoMocks API to simplify common usage patterns of RhinoMocks. Spextensions.RhinoMocks has no ties to NUnit. You can use it with what ever testing framework you are currently using.

### Installation

The easiest way to add Spextensions.RhinoMocks to your application is to use NuGet. You can add the package by searching for "Spextensions.RhinoMocks" in the NuGet GUI or by using the package manager console:

    PM> Install-Package Spextensions.RhinoMocks

### Features

#### Return sequences

You can set up a mocked/stubbed method to return new values from a given sequence for each time it is called:

```csharp
var sequence = new Sequence<int>(1, 2, 3);

_stub.Stub(x => x.Function()).ReturnSequence(sequence);

Assert.AreEqual(1, _stub.Function());
Assert.AreEqual(2, _stub.Function());
Assert.AreEqual(3, _stub.Function());
```

#### Assertions

You can perform arbitrary boolean assertions when expectations are matched:

```csharp
_mock.Expect(x => x.Method())
    .AssertThat(() => condition);
```

The test will fail if the condition lambda returns false when the mocked/stubbed method is called.

#### Lazy/overridable return values

You can set up a mocked/stubbed method to return a given value, which can later be changed so that is actually returns another value. This is very useful when most of the test require a certain return value, but one or two tests need another return value. Now you can set up the mock/stub in the SetUp of the test fixture and then just change the return value in the tests that do not want the default behaviour.

```csharp
[TestFixture]
public class LazyReturnValueSpecs
{
    private ISomeInterface _stub;
    private LazyReturnValue<int> _returnValue;

    [SetUp]
    public void SetUp()
    {
        _stub = MockRepository.GenerateStub<ISomeInterface>();

        _returnValue = LazyReturnValue.Create(1);
        _stub.Stub(x => x.Function()).LazyReturnValue(_returnValue);

    }

    [Test]
    public void Lazily_returning_constant_value_returns_the_constant_value()
    {
        var actualReturnValue = _stub.Function();

        Assert.AreEqual(1, actualReturnValue);
    }

    [Test]
    public void Changing_lazily_returned_value_returns_the_new_value()
    {
        _returnValue.Value = 2;

        var actualReturnValue = _stub.Function();

        Assert.AreEqual(2, actualReturnValue);
    }
}
```

#### Testing order of method calls

Using StrictMocks is simply a pain. If you want to test that methods are called in the correct order, but without having to deal with StrictMocks you can use Signals.

```csharp
_mock1 = MockRepository.GenerateMock<ISomeInterface>();
_mock2 = MockRepository.GenerateMock<ISomeOtherInterface>();
_signal1 = new Signal("signal description 1");

_mock1.Expect(x => x.Method())
    .Signal(_signal1);

_mock2.Expect(x => x.OtherMethod())
    .AssertSignal(_signal1);

// If the calls are not made in the expected order the signal will not be signaled
// when OtherMethod is called, and the test will fail
_mock2.OtherMethod();
_mock1.Method();
```

Multiple signals can be used simultaneously if you want to check the order of multiple method calls.

Signals are inspired by jMocks States.

#### Capturing arguments

You can capture the actual values from a call to a mocked/stubbed method:

```csharp
// Capture argument by type:
int argumentValue = -1;

_mock1.Stub(x => x.FunctionWithOneParameter(1))
    .CaptureFirstMatchingArgument(new ArgumentConstraint<int>(), x => argumentValue = x)
    .Return(0);

_mock1.FunctionWithOneParameter(1);

Assert.AreEqual(1, argumentValue);


// Capture argument by type and match index:
string argumentValue = null;

_mock1.Stub(x => x.FunctionWithTwoParametersOfSameType(null, null))
    .IgnoreArguments()
    .CaptureFirstMatchingArgument(new ArgumentConstraint<string>(matchIndex: 1), x => argumentValue = x)
    .Return(0);

_mock1.FunctionWithTwoParametersOfSameType("string 1", "string 2");

Assert.AreEqual("string 2", argumentValue);
```

You can extract a parameter by type or by type and index.

#### Debugging

It can be frustrating when your stubs/mocks do not get called the way you expect them to. If a mocked method is not called with the correct parameters, you can use debuggins extensions to find out with which parameters the method is actually called.

```csharp
_mock1.Expect(x => x.FunctionWithThreeParameters(0, false, null))
    .IgnoreArgumentsAndWriteInvocationInfoToConsole()
    .Return(0);

_mock1.FunctionWithThreeParameters(1, true, "string");
_mock1.FunctionWithThreeParameters(2, false, "other string");

/* Console output:
Stubbed/expected method 'FunctionWithThreeParameters' was called with the following parameters: 1, True, string
Stubbed/expected method 'FunctionWithThreeParameters' was called with the following parameters: 2, False, other string
*/
```

As the method name implies attaching the debugging output to the mocked method actually adds an IgnoreArguments constraint, so make sure to remove the debugger call when you have found the problem, to avoid leaving a false positive test behind.

Spextensions.NUnit
----------------------

Spextensions.NUnit primarily includes fluid, RSpec style, assertion syntax and semantic aliases for attributes:

```csharp
[Specification]
public class ListSpecs
{
    [Fact]
    private void Clearing_a_list_removes_all_items()
    {
       var list = new List<int> { 1, 2, 3 };
       list.Clear();
       list.Count.ShouldEqual(0);
    }
}
```
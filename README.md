Spextensions
============
Spextensions is a set of extensions and utilities for RhinoMocks and NUnit.

The code is licensed under the MIT license, as described in the LICENSE file.

Spextensions.RhinoMocks
----------------------

Spextensions.RhinoMocks includes several extensions to the RhinoMocks API to simplify common usage patterns of RhinoMocks.

** Return sequences **

** Assertions **

** Method call ordering  **

** Capturing arguments **

** Debugging **



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
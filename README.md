# WildCardExercice.net

* 2 implementations of the wildcard filter in C# with unit tests (TDD)
* Operators:
	- '?' match one character
	- '*' match zero or more characters
	- '+' match one or more characters

* The first implementation uses recursion, the second one uses dynamic programming.

	- The dynamic programming implementation does not support operator '+'.
	- I have to figure it out

```cs
var match = new RecursiveWildCard().IsMatch("abcdef", "a?c*f"); // true
var match = new DynamicProgrammingWildCard().IsMatch("abcdef", "a+def"); // true
var match = new DynamicProgrammingWildCard().IsMatch("abcdef", "a+bcdef"); // false
```

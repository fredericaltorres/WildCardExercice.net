# WildCardExercice.net

* An implementation of the wildcard filter in C# with unit tests (TDD)
* Operators:
	- '?' match one character
	- '*' match zero or more characters
	- '+' match one or more characters

```cs
var match = new WildCard().IsMatch("abcdef", "a?c*f");
```

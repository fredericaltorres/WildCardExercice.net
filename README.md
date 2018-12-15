# WildCardExercice.net

## Overview

* 2 implementations of the wildcard filter in C# with unit tests (TDD)
* Operators:
	- '?' match one character
	- '*' match zero or more characters
	- '+' match one or more characters

* The first implementation uses recursion, the second one uses dynamic programming.

	- The dynamic programming implementation does not support operator '+'.

```cs
var match = new RecursiveWildCard().IsMatch("abcdef", "a?c*f"); // true
var match = new RecursiveWildCard().IsMatch("abcdef", "a+bcdef"); // false

var match = new DynamicProgrammingWildCard().IsMatch("abcdef", "a?c*f"); // true
```


## Build

* Jenkins pipeline using Powershell to build and run unit tests.



```

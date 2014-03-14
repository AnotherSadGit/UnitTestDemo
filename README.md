UnitTestDemo
============

A Visual Studio C# solution that demonstrates how to create unit tests, and how to use dependency injection to isolate the methods being tested.

The initial commit in this repository is the demonstration application that will be tested, a simple bank application that is based on microsoft's [Walkthrough: Creating and Running Unit Tests for Managed Code](http://msdn.microsoft.com/en-us/library/ms182532.aspx).  Note that the application is based on the Microsoft example but is not identical to it.  

Subsequent commits will show the steps to add a basic unit test for one of the application's methods.  Further commits in other branches will show the steps to modify the application to use dependency injection to isolate the method under test from its dependencies (the methods in other classes that it calls).

The Microsoft Patterns and Practices [Unity 3 dependency injection container](http://msdn.microsoft.com/en-us/library/dn170416.aspx) performs the dependency injection during normal operation of the application.  The [Moq Mocking Library](https://github.com/Moq/moq4/wiki/Quickstart) is used to generate mock dependency objects during unit testing.
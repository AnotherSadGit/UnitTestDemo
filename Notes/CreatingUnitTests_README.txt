Creating Unit Tests for the Bank Sample Project
===============================================
Simon Elms, 15 Mar 2014

1) Solution Explorer > Solution > Add Project > Visual C# > Test > Unit Test Project

2) Unit Test Project > Add Reference > Bank Project

3) How do you tell Visual Studio which methods represent unit tests?

	Unit Test Project must reference:
	- Microsoft.VisualStudio.QualityTools.UnitTestFramework
	
	File containing test class needs a using statement:
	- using Microsoft.VisualStudio.TestTools.UnitTesting;
	
	- The test class and test method are decorated with attributes: 
		- [TestClass]
		- [TestMethod]

3) House-keeping - Naming conventions:

	A WORD OF CAUTION: RELIGIOUS WARS ARE BEING CONTINUALLY FOUGHT OVER THE BEST NAMING CONVENTIONS FOR UNIT TESTS.  EVERYONE HAS THEIR OWN IDEA OF WHAT'S BEST.
	
	That having been said, here are some suggestions:
	
	There's no need to include "Test" in the names of the test class or the test method.  It's obvious.
	
	In the unit test project create a FOLDER for each CLASS being tested.  Group all tests relating to that class in the folder.
		eg create a "BankAccount" folder for all tests of the BankAccount class.
	
	Within the folder, create a different TEST CLASS for each UNIT being tested.
	- What is the unit being tested?  A public method, NOT a class. 
		eg Within the "BankAccount" folder create a test class for testing the BankAccount.Debit method.  All unit tests of the Debit method will be included in this test class.  Create another test class for testing the BankAccount.Credit method, and another for testing BankAccount.Transfer.
		
	- It's helpful to include the names of both the class under test and the method under test in the test class name, to make it easier to identify what is being tested when viewing the tests in the Visual Studio Test Explorer.
		eg call the test class "BankAccount_Debit" rather than just "Debit".
		
	Name the unit test methods for the expected result given specific conditions.
		eg 
		- "Should_ThrowException_If_AmountInvalid"
		- "Should_DecrementBalance_Given_ValidAmount"
		
		Reason: If the unit tests are grouped by class in the Visual Studio Test Explorer then the class and tests will read like an English language specification of the application under test.
			eg The Test Explorer would show:
				BankAccount_Debit
					Should_ThrowException_If_AmountInvalid
					Should_DecrementBalance_Given_ValidAmount
	
4) Triple-A pattern for designing a unit test:

	Arrange
	- Set up initial test data and test conditions.
	
	Act
	- Execute method under test.
	
	Assert
	- Check the actual result matches the expected result.  Throw an exception if it doesn't.
	
	- Initially set the expected result incorrectly, to ensure the test will fail if the expected and actual results do not match.
	
5) Running unit tests:

	Build the test project (1st time only.  After that just run the tests, and Visual Studio will compile as it starts the run).
	
	Run via Test Explorer:
		Test Menu > Windows > Test Explorer
		
	Group By > Class
	- Test class and test methods will read like an English language specification of the application under test.
	
	Test will fail if:
	- It throws an exception, any exception.
	
	Otherwise it will pass.
	
6) Investigating why a unit test failed:

	Run the test.
	
	In the Visual Studio Test Explorer: Click on the failed test.  The reason why the test failed will be displayed in the summary pane.
	
	Note the actual and expected values in the summary pane.

7) Test Initialisation and Clean Up:

	[TestInitialize], [TestCleanup] attributes decorate methods that will run automatically before and after each unit test method.
	- Can be used for common setup and cleanup tasks, so setup and cleanup code is not repeated in each test method.
	
	Similarly:
	- [ClassInitialize], [ClassCleanup]: Called when the test class is loaded or unloaded.
	- [AssemblyInitialize], [AssemblyCleanup]: Called when the test assembly is loaded or unloaded.
Mocking Objects for Unit Testing using the Moq Mocking Library
==============================================================
Simon Elms, 18 Mar 2014

Useful Quick Start guide to Moq: https://github.com/Moq/moq4/wiki/Quickstart

1) Installing Moq to the Unit Test Project:

	Solution Explorer > Unit Test Project > Manage NuGet Packages... > Search Online: Moq 
	
	Select Moq: An enjoyable mocking library
	
	Install

2) In the Unit Test Project Add a Reference to the BankTransferService Project:

	NOTE: Now when we create a BankAccount object to test we have to pass an ITransferService object 
		into the BankAccount constructor.  We'll need a reference to the BankTransferService project, 
		where the ITransferService interface is defined.
	
	Add the following reference to the unit test project:
		BankTransferService project
				
3) In the Unit Test Method, Create a Mock Object:

	NOTE: Mock the object that the method under test will call.  DO NOT mock the object to be tested.
		- ie Mock a TransferService object, not a BankAccount object.

	Add using statements to the BankAccount_Transfer file:
		using Moq;
		
		using BankTransferService;
	
	In BankAccount_Transfer.Should_DecrementBalance_Given_ValidAmount unit test method:
	
		- Add 
			Mock<ITransferService> transferServiceMock = new Mock<ITransferService>();
			
4) Set the Return Value of the Mock Object SendFunds Method, which will be called during the Test:
	
	- The BankAccount.Transfer method, which we're unit testing, will call the ITransferService.SendFunds(...) method. 
		So set the appropriate return value for the SendFunds method of the mock object which will be 
		passed into the BankAccount constructor.
		
	In the BankAccount_Transfer.Should_DecrementBalance_Given_ValidAmount unit test method:
	
		- Add
			transferServiceMock.Setup(transferService => 
                transferService.SendFunds(It.IsAny<double>(), It.IsAny<string>())).Returns(true);
			
			- This set up indicates that the SendFunds method of the mock object will always return true, regardless 
				of the values of the arguments passed to the method.  The only condition is that two arguments 
				must be passed to the SendFunds method: A double and a string.
				
5) In the Unit Test Method, Pass the Mock Object into the Constructor of the Object Under Test:

	- ie Pass the mock ITransferService object into the constructor of the BankAccount object we're testing.
	
	In the BankAccount_Transfer.Should_DecrementBalance_Given_ValidAmount unit test method:
	
		- Change the constructor of the BankAccount object to pass the mock object into it:
            BankAccount account = new BankAccount(transferServiceMock.Object)
                {
                    CustomerName = "Mr. John Smith",
					Balance = 12.00
                };

			- NOTE: We must pass the Object property of the mock object, not the mock object itself.
			
6) In the Unit Test Method, Verify the Number of Times the Mock Object Method was Called During the Test:
	
	In the BankAccount_Transfer.Should_DecrementBalance_Given_ValidAmount unit test method:
	
		- add to the assert code: 
            transferServiceMock.Verify(transferService => 
                transferService.SendFunds(It.IsAny<double>(), It.IsAny<string>()), Times.Once());
				
			-This verifies the TransferService.SendFunds method was called exactly once by the method under test.
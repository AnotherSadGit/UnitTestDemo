Dependency Injection using the Unity Application Block
======================================================
Simon Elms, 12 Mar 2014

Good reference: Developer's Guide to Dependency Injection Using Unity (on MSDN), http://msdn.microsoft.com/en-us/library/dn223671.aspx

1) Demonstrate BankAccount.Transfer test:

	Passes.

2) Demonstrate Why Including a Dependency is a Bad Idea in Unit Testing:

	Modify TransferService.SendFunds to return false:

		- Test now fails since it is dependent on TransferService, even though the method under test is working perfectly.
	
3) Extract Interfaces from BankAccount (class under test) and TransferService (class being called by method under test):
	
	In BankAccount and TransferService classes:
		VS 2013 EDIT Menu > Refactor > Extract Interface
		
			- Extracts the interface and modifies the original class to implement the interface.
			
				eg 
				
				public class TransferService
				{ ... }
				
				will be modified to:
				
				public class TransferService : ITransferService
				{ ... }				
			
			- NOTE: Must manually make the interfaces public
			
				eg The interface as generated:
				
				interface ITransferService
				{ ... }
				
				Must be changed to:
				
				public interface ITransferService
				{ ... }

4) Modify Class Under Test to use Interfaces and Dependency Injection:

	In BankAccount class:
	
		a) Add a data member to the store dependency object;		
		b) Add a new constructor that takes a dependency object as a parameter;
		
			eg
			
			private ITransferService _transferService;
			public BankAccount(ITransferService transferService)
			{
				_transferService = transferService;
			}
		
		NOTE the data types of the data member and the constructor parameter.  They are both ITransferService, NOT TransferService.
			So any object that implements the ITransferService interface can be injected into the constructor; it isn't restricted 
			to just taking instances of the TransferService class.
		
		c) Replace all the original hard-coded instances of the dependency class with the dependency data member.
		
			eg 
			
			public void Transfer(double amount, string destinationAccountName)
			{
				:
				:
				TransferService transferService = new TransferService();

				bool result = transferService.SendFunds(details);
				:
				:
			}
		
			Must be changed to:
			
			public void Transfer(double amount, string destinationAccountName)
			{
				:
				:
				bool result = _transferService.SendFunds(details);
				:
				:
			}

5) Installing Unity: 

	Solution Explorer > Bank Project > Manage NuGet Packages... > Search Online: Unity
	
	Select Unity (The Unity Application Block)
	
	Install
	
6) Configuring Unity:

	a) Add to Bank config file:
	
  <configSections>
    <section name="unity"
      type="Microsoft.Practices.Unity.Configuration.UnityConfigurationSection, Microsoft.Practices.Unity.Configuration"/>
  </configSections>
  
  <unity configSource="unity.config"/>
  
		- sets up Unity section as a separate config file, to keep it tidy.
		
	b) Add new unity.config:
	
<?xml version="1.0" encoding="utf-8"?>
<unity xmlns="http://schemas.microsoft.com/practices/2010/unity">
  <container>
    <register type="Bank.IBankAccount, Bank" mapTo="Bank.BankAccount, Bank" />
    <register type="BankTransferService.ITransferService, BankTransferService" mapTo="BankTransferService.TransferService, BankTransferService" />
  </container>
</unity>

		NOTE: In unity.config file properties, set the Copy to Output Directory property to either "Copy always" or "Copy if newer" (default is "Do not copy").
				This ensures the unity.config file will be copied to the execution folder of the application.

7) Create Unity Dependency Injection Container on Application Startup and Resolve References with it:

	NOTE: Application startup: 
	
		- In global.asax Application_Start for web sites/services or;
	
		- In Main method of Windows application (as in this case).
		
	Add the following references to the Bank project:
		
		- Microsoft.Practices.Unity
		
		- Microsoft.Practices.Unity.Configuration
		
		- System.Configuration 

	Add using statements to the Program (startup) file:

		using System.Configuration;
		using Microsoft.Practices.Unity;
		using Microsoft.Practices.Unity.Configuration;

		using BankTransferService;
	
	In the Main method remove the line that explicitly creates a BankAccount object:
	
            BankAccount ba = new BankAccount();
	
	Add Unity dependency injection container to the Main method and use it to create a BankAccount object:
	
			IUnityContainer diContainer = new UnityContainer();
            diContainer.LoadConfiguration();

            // Unity will recursively resolves all object references: 
            //  IBankAccount will be resolved to a BankAccount object, 
            //	When it tries to create a BankAccount object, Unity will see the BankAccount 
            //      constructor takes an ITransferService argument,
            //  ITransferService will be resolved to a TransferService object.
            IBankAccount ba = diContainer.Resolve<IBankAccount>();
			
		NOTE: Unlike the previous code we replaced, we never explicitly tell Unity to create a BankAccount object.  
				Instead we tell it we want an object that implements the IBankAccount interface.  Unity uses its 
				config file to determine what type of object to create.  If we wanted to use a different type of 
				object we would modify Unity's config file; we would not need to recompile the application.
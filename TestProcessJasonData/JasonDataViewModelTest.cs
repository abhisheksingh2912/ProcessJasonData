using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessJasonData.ViewModel;
using System.Threading;
using System.Windows;
using Moq;
using System.Net;

namespace TestProcessJasonData
{
    [TestClass]
    public class MockAppTests
    {
        private static Application application = new Application() { ShutdownMode = ShutdownMode.OnExplicitShutdown };
    }

    [TestClass]
    public class JasonDataViewModelTest: MockAppTests
    {
        JasonDataViewModel testViewModelObj = new JasonDataViewModel();

        [TestInitialize]
        public void TestInitialize()
        {
            SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
        }
        
        [TestMethod]
        public void TestLoadJasonData()
        {            
            // Assert initially there were no items in jason datacollection
            Assert.AreEqual(0, testViewModelObj.JasonDataCollection.Count);
            
            //Prepare
            Mock<IConfigurationManager> configurationManagerMock = new Mock<IConfigurationManager>();
            configurationManagerMock.Setup(i => i.GetAppSetting("JasonDataPath")).Returns("http://jsonplaceholder.typicode.com/posts");
            testViewModelObj.ConfigManager = configurationManagerMock.Object;
            
            // Action 
            testViewModelObj.LoadCommand.Execute(null);
            // Wait for 10 secs as the load runs on parallel tasks.
            Thread.Sleep(5000);
            
            // Assert
            Assert.IsTrue(testViewModelObj.JasonDataCollection.Count > 0);
        }

        [TestMethod]
        public void TestCopyPlainTextToClipboard()
        {
            //Prepare
            testViewModelObj.SelectedItem = new ProcessJasonData.Model.JasonData { ID=1001, Title="TestTittle", UserID=101, Body = "This is test body" };

            // Action
            testViewModelObj.CopyPlainTextCommand.Execute(null); 
            
            // Assert                      
            Assert.AreEqual(testViewModelObj.SelectedItem.Body, Clipboard.GetText());
        }
    }
}

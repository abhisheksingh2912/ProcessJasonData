using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessJasonData.ViewModel;
using System.Threading;
using System.Windows;
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

            testViewModelObj.LoadCommand.Execute(null);
            // Wait for 10 secs as the load runs on parallel tasks.
            Thread.Sleep(5000);
            Assert.IsTrue(testViewModelObj.JasonDataCollection.Count > 0);
        }

        [TestMethod]
        public void TestCopyPlainTextToClipboard()
        {            
            testViewModelObj.SelectedItem = new ProcessJasonData.Model.JasonData { ID=1001, Title="TestTittle", UserID=101, Body = "This is test body" };
            testViewModelObj.CopyPlainTextCommand.Execute(null); 
                                  
            Assert.AreEqual(testViewModelObj.SelectedItem.Body, Clipboard.GetText());
        }
    }
}

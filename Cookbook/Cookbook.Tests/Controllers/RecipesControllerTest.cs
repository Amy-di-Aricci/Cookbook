using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cookbook;
using Cookbook.Controllers;
using System.Web.Mvc;

namespace Cookbook.Tests.Controllers
{
    [TestClass]
    public class RecipesControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            RecipesController controller = new RecipesController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        
    }
}

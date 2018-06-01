using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cookbook;
using Cookbook.Controllers;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Linq;
using Cookbook.Models;
using Moq;

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
        
        [TestMethod]
        public void Details()
        {
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Recipe>())
            .Returns(new FakeDbSet<Recipe>
            {
                new Recipe { RecipeId = 1,
                    Name = "Zupa pomidorowa",
                    Description = "asfagaghahahadh",
                    Difficulty = DifficultyEnum.easy,
                    PublishDate = DateTime.Now }
            });

            var controller = new RecipesController(mock.Object);

            var result = controller.Details(1) as ViewResult;

            Assert.AreEqual("Details", result.ViewName);
            
        }

        [TestMethod]
        public void Create()
        {
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Recipe>()).Returns(new FakeDbSet<Recipe>());

            var obj = mock.Object;
            var newObj = new Recipe
            {
                RecipeId = 1,
                Name = "Zupa pomidorowa",
                Description = "asfagaghahahadh",
                Difficulty = DifficultyEnum.easy,
                PublishDate = DateTime.Now
            };

            var controller = new RecipesController(mock.Object);
            var result = controller.Create(newObj);

            Assert.IsNotNull(obj.Set<Recipe>().FirstOrDefault(p => p.RecipeId == newObj.RecipeId));
        }

        [TestMethod]
        public void Edit()
        {
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Recipe>())
            .Returns(new FakeDbSet<Recipe>
            {
                new Recipe { RecipeId = 1,
                    Name = "Zupa pomidorowa",
                    Description = "asfagaghahahadh",
                    Difficulty = DifficultyEnum.easy,
                    PublishDate = DateTime.Now }
            });

            var obj = mock.Object;
            var newObj = new Recipe
            {
                RecipeId = 1,
                Name = "Zupa ogórkowa",
                Description = "asfagaghahahadh",
                Difficulty = DifficultyEnum.easy,
                PublishDate = DateTime.Now
            };

            var controller = new RecipesController(mock.Object);
            var result = controller.Edit(newObj);
            var editedObj = obj.Set<Recipe>().FirstOrDefault(p => p.RecipeId == newObj.RecipeId);

            Assert.AreEqual(newObj.Name, editedObj.Name);
        }

        [TestMethod]
        public void Delete()
        {
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Recipe>())
            .Returns(new FakeDbSet<Recipe>
            {
                new Recipe { RecipeId = 1,
                    Name = "Zupa pomidorowa",
                    Description = "asfagaghahahadh",
                    Difficulty = DifficultyEnum.easy,
                    PublishDate = DateTime.Now }
            });

            var obj = mock.Object;
            var controller = new RecipesController(mock.Object);
            var result = controller.DeleteConfirmed(1);

            Assert.IsNull(obj.Set<Recipe>().FirstOrDefault(p => p.RecipeId == 1));
        }
    }

    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}

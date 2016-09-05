using CompanyStore.Data;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Test.Services
{
    [TestFixture]
    public class CategoryTestService
    {
        #region Variables
        ICategoryService _categoryService;
        IEntityBaseRepository<Category> _categoryRepository;
        IUnitOfWork _unitOfWork;
        List<Category> _randomCategories;
        #endregion
        #region Setup
        [SetUp]
        public void Setup()
        {
            _randomCategories = SetupCategories();
            _categoryRepository = SetupCategoryRepository();
            _unitOfWork = new Mock<IUnitOfWork>().Object;
            _categoryService = new CategoryService(_categoryRepository, _unitOfWork);
        }
        public List<Category> SetupCategories()
        {
            int counter = new int();
            List<Category> categories = MockDataInitializer.GenerateCategories();
            foreach (Category c in categories)
                c.ID = ++counter;
            return categories;
        }
        public IEntityBaseRepository<Category> SetupCategoryRepository()
        {
            // Init repo
            var repo = new Mock<IEntityBaseRepository<Category>>();
            // Setup behavior
            repo.Setup(r => r.GetAll())
                .Returns(_randomCategories.AsQueryable());
            return repo.Object;
        }
        #endregion
        #region Test
        [Test]
        public void ServiceShouldReturnAllCategories()
        {
            var categories = _categoryService.GetCategories();

            Assert.That(categories, Is.EqualTo(_randomCategories));
        }
        #endregion
    }
}

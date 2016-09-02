using CompanyStore.Data;
using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStore.Test.Services
{
    [TestFixture]
    public class RentalTestService
    {
        #region Variables
        private List<Rental> _randomRentals;
        private IRentalService _rentalService;
        private IUnitOfWork _unitOfWork;
        private IEntityBaseRepository<Rental> _rentalRepository;
        #endregion
        #region Setup
        [SetUp]
        public void Setup()
        {

        }
        //public List<Rental> SetupRentals()
        //{
        //    int counter = new int();
            
        //}
        public void SetupRentalRepository()
        {

        }
        #endregion
        #region Test
        
        #endregion
    }
}

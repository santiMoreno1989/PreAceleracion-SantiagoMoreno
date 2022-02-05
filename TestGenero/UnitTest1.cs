using ApiPreAceleracionAlkemy.Controllers;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestGenero
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IUnitOfWork _uow;
        public UnitTest1(IUnitOfWork uow)
        {
            _uow = uow;
        }
        [TestMethod]
        public void GetGenero_Retorna_OkResult_con_todos_los_registros()
        {
           
        }
    }
}

using ApiPreAceleracionAlkemy.Controllers;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;

namespace TestDisney
{
    [TestClass]
    public class Test
    {
        GenerosController _controller;
        IGeneroRepository _genero;
        
        [TestMethod]
        public void GetReturnsGeneros()
        {
            //ARRANGE
            var controller = new GenerosController();
            //ACT
            var result = _controller.Get(10);
            //ASSERT
            
        }
    }
}

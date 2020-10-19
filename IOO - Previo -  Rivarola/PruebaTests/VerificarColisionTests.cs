using Game.Abstracciones;
using Game.Entidades;
using Game.SingletonManagers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebaColisiones.Tests
{
    [TestClass]
    public class VerificarColisionTests
    {
        #region SetUP

        private IVector2D r1, r2;

        #endregion

        #region CasosOk

        [TestMethod]
        public void VerificarColisiones_Colisiona_RetornaTrue()
        {
            //Arrange
            r1 = new Personaje(10, 10);
            r2 = new Personaje(8, 8);

            //Act
            bool resultado = ColisionManager.ColisionaEnemigoPersonaje(r1, r2);

            //Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void VerificarColisiones_Colisiona2_RetornaTrue()
        {
            //Arrange
            r1 = new Personaje(7, 7);
            r2 = new Personaje(5, 5);

            //Act
            bool resultado = ColisionManager.ColisionaEnemigoPersonaje(r1, r2);

            //Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void VerificarColisiones_Colisiona3_RetornaTrue()
        {
            //Arrange
            r1 = new Personaje(5, 5);
            r2 = new Personaje(7, 7);

            //Act
            bool resultado = ColisionManager.ColisionaEnemigoPersonaje(r1, r2);

            //Assert
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void VerificarColisiones_CasoColisionaPorCompleto_RetornaTrue()
        {
            //Arrange
            r1 = new Personaje(300, 300);
            r2 = new Personaje(300, 300);

            //Act
            bool resultado = ColisionManager.ColisionaEnemigoPersonaje(r1, r2);

            //Assert
            Assert.IsTrue(resultado);
        }

        #endregion

        #region CasosConError

        [TestMethod]
        public void VerificarColisiones_NoColisiona_RetornaFalse()
        {
            //Arrange
            r1 = new Personaje(1, 1);
            r2 = new Personaje(200, 200);

            //Act
            bool resultado = ColisionManager.ColisionaEnemigoPersonaje(r1, r2);

            //Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void VerificarColisiones_NoColisiona2_RetornaFalse()
        {
            //Arrange
            r1 = new Personaje(5, 5);
            r2 = new Personaje(1, 1);

            //Act
            bool resultado = ColisionManager.ColisionaEnemigoPersonaje(r1, r2);

            //Assert
            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void VerificarColisiones_NoColisiona3_RetornaFalse()
        {
            //Arrange
            r1 = new Personaje(100, 100);
            r2 = new Personaje(1, 1);

            //Act
            bool resultado = ColisionManager.ColisionaEnemigoPersonaje(r1, r2);

            //Assert
            Assert.IsFalse(resultado);
        }

        #endregion

    }
}
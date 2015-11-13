using System;
using NankinjoKey.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NankinjoKey.Models;
using System.Linq;

namespace NankinjoKey.Tests.Classes
{
    [TestClass]
    public class KeyCodeGeneratorTests
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [TestMethod]
        public void GenerateTests()
        {
            // arrange
            int length = 5;
            var keyGenerator = new KeyCodeGenerator();

            // act
            var keyCode = keyGenerator.Generate(length);

            // assert
            //Assert.IsNull(keyCode, "null error");
            Assert.AreEqual(keyCode.Length, length, "length error");
            Assert.IsFalse(db.KeyInfoes.Any(s => s.KeyCode == keyCode),"keyCode重複");
        }
    }
}

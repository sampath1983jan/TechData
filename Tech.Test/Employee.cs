using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
namespace Tech.Test
{
    [TestClass]
    public class Employee
    {
        [TestCase("Sampath","Kumar")]
        [TestCase("sss", "dd")]
        public void Create(string firstname,string lastname)
        {
            Emp e = new Emp(firstname, lastname);
            NUnit.Framework.Assert.AreEqual(firstname + " " + lastname, e.fullname);

        }
        [Test]
        public void Update(string firstname, string lastname) {
            Emp e = new Emp(firstname, lastname);
            NUnit.Framework.Assert.AreEqual(firstname + " " + lastname, e.fullname);
        }
    }
}


public class Emp {
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string fullname { get; set; }
    public Emp(string fn,string ln) {
        this.firstname = fn;
        this.lastname = ln;
        this.fullname = this.firstname + " " + this.lastname;
    }
}
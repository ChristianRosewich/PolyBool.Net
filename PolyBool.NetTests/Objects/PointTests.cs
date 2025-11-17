using PolyBool.Net.Interfaces;
using PolyBool.Net.Logic;
using System;
using System.Security.Cryptography.X509Certificates;

namespace PolyBool.Net.Objects.Tests
{
    [TestClass()]
    public class PointTests : PointTests<decimal>
    {
        protected override IPoint<decimal> newIPoint(double x, double y) 
            => Point<decimal>.New((decimal)x, (decimal)y);

        protected override Point newPoint(double x, double y) 
            => new Point((decimal)x, (decimal)y);
    }


    public class PointTests<T> where T : struct, IConvertible
    {
        private readonly Point Zero = new Point(0, 0);
        private readonly Point OneOne = new Point(1, 1);
        private int ZeroHash => Zero.GetHashCode();

        protected virtual Point newPoint(double x, double y)=> throw new NotImplementedException();
        protected virtual IPoint<T> newIPoint(double x, double y)=> throw new NotImplementedException();

        [DataTestMethod()]
        [DataRow(0,0,1,1,1,1)]
        [DataRow(1, 1, 0, 0, 1, 1)]
        [DataRow(1, 3, 2, 5, 3, 8)]
        public void AddTest(double X,double Y, double X1, double Y1, double ExpX, double ExpY)
        {
            IPoint<T> p = newIPoint(X, Y);
            Point p1 = newPoint(X1, Y1);
            Assert.IsInstanceOfType(p.Add(p1),typeof(IPoint<T>));
            Assert.AreEqual(ExpX,p.X.ToDouble(null));
            Assert.AreEqual(ExpY, p.Y.ToDouble(null));
        }

        [DataTestMethod()]
        [DataRow(0, 0, 1, 1, -1, -1)]
        [DataRow(1, 1, 0, 0, 1, 1)]
        [DataRow(1, 3, 2, 5, -1, -2)]
        public void SubtractTest(double X, double Y, double X1, double Y1, double ExpX, double ExpY)
        {
            IPoint<T> p = newIPoint(X, Y);
            Point p1 = newPoint(X1, Y1);
            Assert.IsInstanceOfType(p.Subtract(p1), typeof(IPoint<T>));
            Assert.AreEqual(ExpX, p.X.ToDouble(null));
            Assert.AreEqual(ExpY, p.Y.ToDouble(null));
        }

        [DataTestMethod()]
        [DataRow(0, 0, 1, 1, false)]
        [DataRow(1, 1, 0, 0, false)]
        [DataRow(1, 1, 1, 1, true)]
        [DataRow(1, 2, 1, 2, true)]
        [DataRow(1, 2, 2, 1, false)]
        public void EqualsTest(double X,double Y,double X1,double Y1,bool xExp)
        {
            Point p = newPoint(X, Y);
            Point p1 = newPoint(X1, Y1);
            Assert.AreEqual(xExp,p.Equals(p1));
        }
        
        [DataTestMethod()]
        [DataRow(0, 0, "1, 1", false)]
        [DataRow(0, 0, null, false)]
        public void EqualsTest2(double X, double Y, object? o, bool xExp)
        {
            Point p = newPoint(X, Y);
            Assert.AreEqual(xExp, p.Equals(o));
        }
       
        [DataTestMethod()]
        [DataRow(0, 0,  true)]
        [DataRow(1, 1,  false)]
        [DataRow(1, 3,  false)]
        [DataRow(3, 2,  false)]
        public void GetHashCodeTest0(double X, double Y,bool xExpZero )
        {
            Point p = newPoint(X, Y);
            Assert.AreEqual(xExpZero,p.GetHashCode() == ZeroHash);
        }

        [DataTestMethod()]
        [DataRow(0, 0, false)]
        [DataRow(1, 1, true)]
        [DataRow(1, 3, false)]
        [DataRow(3, 2, false)]
        public void GetHashCodeTest1(double X, double Y, bool xExpZero)
        {
            Point p = newPoint(X, Y);
            Assert.AreEqual(xExpZero, p.GetHashCode() == OneOne.GetHashCode());
        }

        [DataTestMethod()]
        [DataRow(0, 0, 1, 1, false)]
        [DataRow(1, 1, 0, 0, false)]
        [DataRow(1, 1, 1, 1, true)]
        [DataRow(1, 2, 1, 2, true)]
        [DataRow(1, 2, 2, 1, false)]
        public void GetHashCodeTestE(double X, double Y, double X1, double Y1, bool xExp)
        {
            var p = newPoint(X, Y);
            Point p1 = newPoint(X1, Y1);
            Assert.AreEqual(xExp, p.GetHashCode()== p1.GetHashCode());
        }
       /*
        [DataTestMethod()]
        [DataRow(0, 0 )]
        [DataRow(1, 1)]
        [DataRow(1, 1)]
        [DataRow(1, 2)]
        [DataRow(1, 2)]
        public void CloneTest(double X, double Y)
        {
            IPoint<double> p = new Point<double>(X, Y);
            var p1 = p.Clone();
            Assert.AreEqual(false, p == p1);
            Assert.AreEqual(true, p.Equals(p1));
        }
        
        [DataTestMethod()]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 1, -1, 1)]
        [DataRow(-1, 1, -1, -1)]
        [DataRow(1, 2, -2, 1)]
        [DataRow(1, -2, 2, 1)]
        public void NormalTest(double X, double Y, double ExpX, double ExpY)
        {
            IPoint<double> p = new Point(X, Y);
            Assert.IsInstanceOfType(p.Normal(), typeof(IPoint));
            Assert.AreEqual(ExpX, p.X,"X");
            Assert.AreEqual(ExpY, p.Y, "Y");
        }
        //*/
    }
}
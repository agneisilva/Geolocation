using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace Core.Tests
{
    public class GeolocationCoreCalculatorTests
    {
        public ICollection<Coordenate> polygon1 { get; private set; } = new HashSet<Coordenate>();
        public ICollection<Coordenate> polygon2 { get; private set; } = new HashSet<Coordenate>();
        public ICollection<Coordenate> polygon3 { get; private set; } = new HashSet<Coordenate>();

        public GeolocationCoreCalculatorTests()
        {
            //Random polygon drawn using https://www.keene.edu/campus/maps/tool/
            polygon1.Add(new Coordenate(-72.2813487, 42.9291224));
            polygon1.Add(new Coordenate(-72.2775400, 42.9292010));
            polygon1.Add(new Coordenate(-72.2774863, 42.9274020));
            polygon1.Add(new Coordenate(-72.2813594, 42.9273392));
            polygon1.Add(new Coordenate(-72.2813702, 42.9291303));
            polygon1.Add(new Coordenate(-72.2813487, 42.9291224));


            //Bacia do Mange - RIO JANEIRO - Polygon
            polygon2.Add(new Coordenate(-43.1947740, -22.9139550));
            polygon2.Add(new Coordenate(-43.1948380, -22.9124230));
            polygon2.Add(new Coordenate(-43.1947960, -22.9120210));
            polygon2.Add(new Coordenate(-43.1960450, -22.9124550));
            polygon2.Add(new Coordenate(-43.1959420, -22.9125900));
            polygon2.Add(new Coordenate(-43.1958250, -22.9129720));
            polygon2.Add(new Coordenate(-43.1956650, -22.9135840));
            polygon2.Add(new Coordenate(-43.1955660, -22.9139280));
            polygon2.Add(new Coordenate(-43.1955180, -22.9141560));
            polygon2.Add(new Coordenate(-43.1954880, -22.9144820));
            polygon2.Add(new Coordenate(-43.1953570, -22.9144450));
            polygon2.Add(new Coordenate(-43.1952900, -22.9146500));
            polygon2.Add(new Coordenate(-43.1948380, -22.9145170));
            polygon2.Add(new Coordenate(-43.1950120, -22.9140910));
            polygon2.Add(new Coordenate(-43.1947740, -22.9139550));

            //Invalid Polygon
            //Got this from excel sheet polygon ref 23
            polygon3.Add(new Coordenate(-43.235698, -22.916006));
            polygon3.Add(new Coordenate(-43.235698, -22.916006));
            polygon3.Add(new Coordenate(-43.235723, -22.915914));
            polygon3.Add(new Coordenate(-43.235934, -22.916374));
            polygon3.Add(new Coordenate(-43.23605,  -22.916275));
            polygon3.Add(new Coordenate(-43.236211, -22.916653));
            polygon3.Add(new Coordenate(-43.236587, -22.916963));
            polygon3.Add(new Coordenate(-43.2369,   -22.916637));
            polygon3.Add(new Coordenate(-43.237525, -22.916272));

        }

        #region Test for polygon ONE


        [Fact]
        public void point_on_polygon1_boundary_should_be_true()
        {
            var boundaryPoint = new Coordenate(-72.2813487, 42.9291224);
            var polygon = polygon1.ToArray();

            var result = Polygon.IsInside(polygon, boundaryPoint);

            Assert.True(result);
        }

        [Theory]
        [InlineData(-72.2813487, 42.9291224)]
        [InlineData(-72.2775400, 42.9292010)]
        [InlineData(-72.2774863, 42.9274020)]
        [InlineData(-72.2813594, 42.9273392)]
        [InlineData(-72.2813702, 42.9291303)]
        public void multiples_points_on_polygon1_boundary_should_be_true(double x, double y)
        {
            var point = new Coordenate(x, y);
            var polygon = polygon1.ToArray();

            var result = Polygon.IsInside(polygon, point);

            Assert.True(result);
        }

        [Theory]
        [InlineData(-72.2823572, 42.9281640)]
        [InlineData(-72.2759414, 42.9283212)]
        [InlineData(-72.2796965, 42.9346997)]
        [InlineData(-72.4047089, 42.9265143)]
        [InlineData(-70.1641846, 42.8799895)]
        [InlineData(-37.6171875, 42.2285174)]
        [InlineData(-72.2794450, 42.9291701)]
        public void multiples_points_DONT_belongs_to_polygon1_should_be_false(double x, double y)
        {
            var point = new Coordenate(x, y);
            var polygon = polygon1.ToArray();

            var result = Polygon.IsInside(polygon, point);

            Assert.False(result);
        }

        [Theory]
        [InlineData(-72.2811985, 42.9289732)]
        [InlineData(-72.2811878, 42.9288082)]
        [InlineData(-72.2807533, 42.9287296)]
        [InlineData(-72.2795838, 42.9288514)]
        [InlineData(-72.2785968, 42.9289614)]
        [InlineData(-72.2782481, 42.9286157)]
        [InlineData(-72.2776848, 42.9290832)]
        [InlineData(-72.2777867, 42.9283644)]
        [InlineData(-72.2790688, 42.9280266)]
        [InlineData(-72.2794980, 42.9285922)]
        [InlineData(-72.2809732, 42.9275945)]
        [InlineData(-72.2803670, 42.9274727)]
        [InlineData(-72.2809410, 42.9284665)]
        public void multiples_points_BELONGS_TO_polygon1_should_be_true(double x, double y)
        {
            var point = new Coordenate(x, y);
            var polygon = polygon1.ToArray();

            var result = Polygon.IsInside(polygon, point);

            Assert.True(result);
        }

        #endregion


        #region Test for polygon TWO

        [Fact]
        public void point_on_polygon2_boundary_should_be_true()
        {
            var boundaryPoint = new Coordenate(-43.1947740, -22.9139550);
            var polygon = polygon2.ToArray();

            var result = Polygon.IsInside(polygon, boundaryPoint);

            Assert.True(result);
        }

        [Theory]
        [InlineData(-43.1947740, -22.9139550)]
        [InlineData(-43.1948380, -22.9124230)]
        [InlineData(-43.1947960, -22.9120210)]
        [InlineData(-43.1960450, -22.9124550)]
        [InlineData(-43.1959420, -22.9125900)]
        [InlineData(-43.1958250, -22.9129720)]
        [InlineData(-43.1956650, -22.9135840)]
        [InlineData(-43.1955660, -22.9139280)]
        [InlineData(-43.1955180, -22.9141560)]
        [InlineData(-43.1954880, -22.9144820)]
        [InlineData(-43.1953570, -22.9144450)]
        [InlineData(-43.1952900, -22.9146500)]
        [InlineData(-43.1948380, -22.9145170)]
        [InlineData(-43.1950120, -22.9140910)]

        public void multiples_points_on_polygon2_boundary_should_be_true(double x, double y)
        {
            var point = new Coordenate(x, y);
            var polygon = polygon2.ToArray();

            var result = Polygon.IsInside(polygon, point);

            Assert.True(result);
        }

        [Theory]
        [InlineData(-43.1938434, -22.9130214)]
        [InlineData(-43.1936342, -22.9134216)]
        [InlineData(-43.1966060, -22.9136292)]
        [InlineData(-43.1970137, -22.9131153)]
        [InlineData(-43.1983280, -22.9128633)]
        [InlineData(-43.1973356, -22.9118256)]
        [InlineData(-43.1960964, -22.9117911)]
        public void multiples_points_DONT_belongs_to_polygon2_should_be_false(double x, double y)
        {
            var point = new Coordenate(x, y);
            var polygon = polygon2.ToArray();

            var result = Polygon.IsInside(polygon, point);

            Assert.False(result);
        }

        [Theory]
        [InlineData(-43.1949216, -22.9121616)]
        [InlineData(-43.1958282, -22.9124878)]
        [InlineData(-43.1957853, -22.9125816)]
        [InlineData(-43.1953400, -22.9125322)]
        [InlineData(-43.1952542, -22.9139306)]
        [InlineData(-43.1953883, -22.9143852)]
        [InlineData(-43.1950825, -22.9144790)]
        [InlineData(-43.1949162, -22.9144790)]
        public void multiples_points_BELONGS_TO_polygon2_should_be_true(double x, double y)
        {
            var point = new Coordenate(x, y);
            var polygon = polygon2.ToArray();

            var result = Polygon.IsInside(polygon, point);

            Assert.True(result);
        }

        #endregion

        #region Tests for polygon Three
        [Fact]
        public void point_on_polygon3_boundary_should_be_true()
        {
            var boundaryPoint = new Coordenate(-43.235698, -22.916006);
            var polygon = polygon3.ToArray();

            var result = Polygon.IsInside(polygon, boundaryPoint);

            Assert.True(result);
        }

        [Fact]
        public void multiples_points_DONT_belongs_to_polygon3_should_be_false()
        {
            var boundaryPoint = new Coordenate(-43.2365066, -22.9165098);
            var polygon = polygon3.ToArray();

            var result = Polygon.IsInside(polygon, boundaryPoint);

            Assert.True(result);
        }
        #endregion
    }
}

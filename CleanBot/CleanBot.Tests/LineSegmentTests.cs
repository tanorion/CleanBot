using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Xunit;

namespace CleanBot.Tests
{
    public class LineSegmentTests
    {
        [Fact]
        public void combine_segments_should_work_with_two_vertical_that_overlap()
        {
            var seg1=new LineSegment()
            {
                Start = new Position(0,0),
                End = new Position(0,1),
                StaticPositionValue = 0,
                Vertical = true
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(0, 2),
                StaticPositionValue = 0,
                Vertical = true
            };
            LineSegment output;
            Assert.True(LineSegment.TryCombineSegment(seg1,seg2, out output));
            Assert.Equal(0, output.Start.X);
            Assert.Equal(0, output.Start.Y);
            Assert.Equal(0, output.End.X);
            Assert.Equal(2, output.End.Y);
            Assert.Equal(0, output.StaticPositionValue);
            Assert.True(output.Vertical);
        }

        [Fact]
        public void combine_segments_should_work_with_two_vertical_that_overlap_only_in_point()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(0, 1),
                StaticPositionValue = 0,
                Vertical = true
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(0, 1),
                End = new Position(0, 2),
                StaticPositionValue = 0,
                Vertical = true
            };
            LineSegment output;
            Assert.True(LineSegment.TryCombineSegment(seg1, seg2, out output));
            Assert.Equal(0, output.Start.X);
            Assert.Equal(0, output.Start.Y);
            Assert.Equal(0, output.End.X);
            Assert.Equal(2, output.End.Y);
            Assert.Equal(0, output.StaticPositionValue);
            Assert.True(output.Vertical);
        }

        [Fact]
        public void combine_segments_should_work_with_two_vertical_that_overlap_inside()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(0, 1),
                StaticPositionValue = 0,
                Vertical = true
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(0, -1),
                End = new Position(0, 2),
                StaticPositionValue = 0,
                Vertical = true
            };
            LineSegment output;
            Assert.True(LineSegment.TryCombineSegment(seg1, seg2, out output));
            Assert.Equal(0, output.Start.X);
            Assert.Equal(-1, output.Start.Y);
            Assert.Equal(0, output.End.X);
            Assert.Equal(2, output.End.Y);
            Assert.Equal(0, output.StaticPositionValue);
            Assert.True(output.Vertical);
        }

        [Fact]
        public void combine_segments_should_work_with_two_vertical_that_dont_overlap()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(0, 1),
                StaticPositionValue = 0,
                Vertical = true
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(0, 2),
                End = new Position(0, 3),
                StaticPositionValue = 0,
                Vertical = true
            };
            LineSegment output;
            Assert.False(LineSegment.TryCombineSegment(seg1, seg2, out output));
       
        }

        [Fact]
        public void combine_segments_should_work_with_two_horizontal_that_overlap()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(1, 0),
                StaticPositionValue = 0,
                Vertical = false
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(2, 0),
                StaticPositionValue = 0,
                Vertical = false
            };
            LineSegment output;
            Assert.True(LineSegment.TryCombineSegment(seg1, seg2, out output));
            Assert.Equal(0, output.Start.X);
            Assert.Equal(0, output.Start.Y);
            Assert.Equal(0, output.End.Y);
            Assert.Equal(2, output.End.X);
            Assert.Equal(0, output.StaticPositionValue);
            Assert.False(output.Vertical);
        }

        [Fact]
        public void combine_segments_should_work_with_two_horizontal_that_overlap_only_in_point()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(1, 0),
                StaticPositionValue = 0,
                Vertical = false
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(1, 0),
                End = new Position(2, 0),
                StaticPositionValue = 0,
                Vertical = false
            };
            LineSegment output;
            Assert.True(LineSegment.TryCombineSegment(seg1, seg2, out output));
            Assert.Equal(0, output.Start.X);
            Assert.Equal(0, output.Start.Y);
            Assert.Equal(0, output.End.Y);
            Assert.Equal(2, output.End.X);
            Assert.Equal(0, output.StaticPositionValue);
            Assert.False(output.Vertical);
        }

        [Fact]
        public void combine_segments_should_work_with_two_horizontal_that_overlap_inside()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(1, 0),
                StaticPositionValue = 0,
                Vertical = false
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(-1, 0),
                End = new Position(2, 0),
                StaticPositionValue = 0,
                Vertical = false
            };
            LineSegment output;
            Assert.True(LineSegment.TryCombineSegment(seg1, seg2, out output));
            Assert.Equal(0, output.Start.Y);
            Assert.Equal(-1, output.Start.X);
            Assert.Equal(0, output.End.Y);
            Assert.Equal(2, output.End.X);
            Assert.Equal(0, output.StaticPositionValue);
            Assert.False(output.Vertical);
        }

        [Fact]
        public void combine_segments_should_work_with_two_horizontal_that_dont_overlap()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(1, 0),
                StaticPositionValue = 0,
                Vertical = false
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(2, 0),
                End = new Position(3, 0),
                StaticPositionValue = 0,
                Vertical = false
            };
            LineSegment output;
            Assert.False(LineSegment.TryCombineSegment(seg1, seg2, out output));

        }

        [Fact]
        public void intersections_should_work_when_intersect()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(1, 0),
                StaticPositionValue = 0,
                Vertical = false
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(0, 3),
                StaticPositionValue = 0,
                Vertical = true
            };
            Position output;
            Assert.True(LineSegment.Intersect(seg1, seg2, out output));
            Assert.Equal(0,output.X);
            Assert.Equal(0, output.Y);

            seg1 = new LineSegment()
            {
                Start = new Position(-100000, 100000),
                End = new Position(100000, 100000),
                StaticPositionValue = 100000,
                Vertical = false
            };

            seg2 = new LineSegment()
            {
                Start = new Position(100000, 99999),
                End = new Position(100000, 100000),
                StaticPositionValue = 100000,
                Vertical = true
            };
       
            Assert.True(LineSegment.Intersect(seg1, seg2, out output));
            Assert.Equal(100000, output.X);
            Assert.Equal(100000, output.Y);


        }


        [Fact]
        public void intersections_should_work_when_not_intersect()
        {
            var seg1 = new LineSegment()
            {
                Start = new Position(1, 1),
                End = new Position(1, 0),
                StaticPositionValue = 0,
                Vertical = true
            };

            var seg2 = new LineSegment()
            {
                Start = new Position(0, 0),
                End = new Position(0, 3),
                StaticPositionValue = 0,
                Vertical = false
            };
            Position output;
            Assert.False(LineSegment.Intersect(seg1, seg2, out output));

            seg1 = new LineSegment()
            {
                Start = new Position(-100000, 100000),
                End = new Position(100000, 100000),
                StaticPositionValue = 100000,
                Vertical = false
            };

            seg2 = new LineSegment()
            {
                Start = new Position(100000, 99999),
                End = new Position(100000, 99998),
                StaticPositionValue = 100000,
                Vertical = true
            };
            
            Assert.False(LineSegment.Intersect(seg1, seg2, out output));
        }
    }
}

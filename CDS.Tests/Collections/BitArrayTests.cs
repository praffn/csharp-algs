using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using CDS.Collections;
using Xunit;

namespace CDS.Tests.Collections
{
    public class BitArrayTests
    {
        [Fact]
        public void BitArray_with_no_args_creates_BA_of_len_32_with_all_0()
        {
            Assert.Equal("00000000000000000000000000000000", new BitArray().ToString());
        }

        [Fact]
        public void BitArray_given_int_creates_BA_with_len_n_with_all_()
        {
            Assert.Equal("0000", new BitArray(4).ToString());
        }

        [Fact]
        public void BitArray_given_int_throws_if_int_less_than_0()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BitArray(-1));
        }

        [Fact]
        public void BitArray_given_bool_array_creates_BA_with_same_values()
        {
            var bools = new[] {false, true, false, true};
            var ba = new BitArray(bools);
            Assert.Equal("0101", ba.ToString());
        }
        
        [Fact]
        public void Get_returns_bit_at_position()
        {
            var ba = new BitArray();
            Assert.False(ba[12]);
        }

        [Fact]
        public void Get_throws_if_not_in_range()
        {
            var ba = new BitArray(100);
            Assert.Throws<IndexOutOfRangeException>(() => ba[100]);
        }

        [Fact]
        public void Set_sets_bit_at_position()
        {
            var ba = new BitArray();
            Assert.False(ba[1]);
            ba.Set(1);
            Assert.True(ba[1]);
        }

        [Fact]
        public void Set_on_1_bit_changes_nothing()
        {
            var ba = new BitArray();
            ba.Set(1);
            ba.Set(1);
            Assert.True(ba[1]);
        }

        [Fact]
        public void Not_inverst_entire_array()
        {
            var ba = new BitArray(5);
            ba.Set(1);
            ba.Set(3);
            ba.Not();
            Assert.Equal("10101", ba.ToString());
        }

        [Fact]
        public void And_ands_a_BitArray()
        {
            var a = new BitArray(true, true, false, false);
            var b = new BitArray(true, false, true, false);
            a.And(b);
            Assert.Equal("1000", a.ToString());
        }

        [Fact]
        public void And_throws_if_given_ba_is_not_of_equal_length()
        {
            var a = new BitArray(2);
            var b = new BitArray(100);
            Assert.Throws<ArgumentException>(() => a.And(b));
        }

        [Fact]
        public void Or_ors_a_BitArray()
        {
            var a = new BitArray(true, true, false, false);
            var b = new BitArray(true, false, true, false);
            a.Or(b);
            Assert.Equal("1110", a.ToString());
        }
        
        [Fact]
        public void Or_throws_if_given_ba_is_not_of_equal_length()
        {
            var a = new BitArray(2);
            var b = new BitArray(100);
            Assert.Throws<ArgumentException>(() => a.Or(b));
        }

        [Fact]
        public void Xor_xors_a_BitArray()
        {
            var a = new BitArray(true, true, false, false);
            var b = new BitArray(true, false, true, false);
            a.Xor(b);
            Assert.Equal("0110", a.ToString());
        }
        
        [Fact]
        public void Xor_throws_if_given_ba_is_not_of_equal_length()
        {
            var a = new BitArray(2);
            var b = new BitArray(100);
            Assert.Throws<ArgumentException>(() => a.Xor(b));
        }

        [Fact]
        public void BoolArray_returns_array_of_bools_representing_BitArray()
        {
            var ba = new BitArray(true, true, false, true, false);
            Assert.Equal(new [] { true, true, false, true, false }, ba.BoolArray());
        }

        [Fact]
        public void BoolArray_can_be_enumerated()
        {
            var ba = new BitArray(true, false, true, false, false);
            var expected = new[] {true, false, true, false, false};
            Assert.Equal(expected, ba.ToList());
        }
    }
}
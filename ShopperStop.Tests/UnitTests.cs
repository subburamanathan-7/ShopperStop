using System;
using System.Collections.Generic;
using Xunit;

public class UnitTests
{
    private static decimal Total(params ItemType[] items) =>
        Program.CalculateTotal(new List<ItemType>(items), Program.inventory, Program.offers);

    [Fact]
    public void EmptyBasket_CostsZero() =>
        Assert.Equal(0m, Total());

    [Fact]
    public void SpecExample_TwoApplesOneBanana() =>
        Assert.Equal(90m, Total(ItemType.Apple, ItemType.Apple, ItemType.Banana));

    [Theory]
    [InlineData(1, 50)]
    [InlineData(2, 50)]
    [InlineData(3, 100)]
    [InlineData(4, 100)]
    public void Melons_BuyOneGetOneFree(int count, int expected)
    {
        var items = new ItemType[count];
        Array.Fill(items, ItemType.Melon);
        Assert.Equal(expected, Total(items));
    }

    [Theory]
    [InlineData(1, 15)]
    [InlineData(2, 30)]
    [InlineData(3, 30)]
    [InlineData(4, 45)]
    [InlineData(6, 60)]
    [InlineData(7, 75)]
    public void Limes_ThreeForTwo(int count, int expected)
    {
        var items = new ItemType[count];
        Array.Fill(items, ItemType.Lime);
        Assert.Equal(expected, Total(items));
    }

    [Fact]
    public void MixedBasket_OrderDoesNotMatter()
    {
        var a = Total(ItemType.Apple, ItemType.Banana, ItemType.Melon, ItemType.Melon,
                      ItemType.Lime, ItemType.Lime, ItemType.Lime);
        var b = Total(ItemType.Lime, ItemType.Melon, ItemType.Apple, ItemType.Lime,
                      ItemType.Banana, ItemType.Lime, ItemType.Melon);
        Assert.Equal(135m, a);
        Assert.Equal(a, b);
    }

    [Fact]
    public void OfferWithZeroQuantity_Throws() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new OfferDetails(0, 1));
}
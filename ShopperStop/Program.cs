using System;
using System.Collections.Generic;

enum ItemType
{
    Apple = 1,
    Banana,
    Melon,
    Lime
}

class OfferDetails
{
    public int Quantity { get; }
    public int PaidQuantity { get; }

    public OfferDetails() { }

    public OfferDetails(int quantity, int paidQuantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Offer quantity must be positive.");

        Quantity = quantity;
        PaidQuantity = paidQuantity;
    }
}

class Program
{
    // Inventory
    internal static readonly Dictionary<ItemType, decimal> inventory = new()
        {
           { ItemType.Apple, 35m},
           { ItemType.Banana, 20m },
           { ItemType.Melon, 50m },
           { ItemType.Lime, 15m },
        };

    // Offers
    internal static readonly Dictionary<ItemType, OfferDetails> offers = new()
        {
            {ItemType.Melon, new OfferDetails(2, 1)},
            {ItemType.Lime, new OfferDetails(3, 2)}
        };

    static void Main(string[] args)
    {
        List<ItemType> cart = new List<ItemType>();

        // The application is named as ShopperStop
        Console.WriteLine("Welcome to ShopperStop");

        // We can bypass the input entry below and directly use the AddRange below.
        // cart.AddRange([ItemType.Lime, ItemType.Lime, ItemType.Lime, ItemType.Lime, ItemType.Lime,ItemType.Lime]);

        while (true)
        {
            Console.WriteLine("Type the numbers displayed next to the item you would like to pick");
            Console.WriteLine(" 1 - Apple");
            Console.WriteLine(" 2 - Banana");
            Console.WriteLine(" 3 - Melon");
            Console.WriteLine(" 4 - Lime");
            Console.WriteLine(" 5 - Exit shopping and bill");

            Console.Write("Enter the option you would like to choose : ");
            string? inputChoice = Console.ReadLine();
            if (inputChoice is null)
                break;

            if (!int.TryParse(inputChoice.Trim(), out int option) || option < 1 || option > 5)
            {
                Console.WriteLine("Invalid input, please enter a number from 1 to 5.");
                continue;
            }

            if (option == 5)
            {
                Console.WriteLine("Completed Shopping; Proceeding to billing");
                break;
            }

            cart.Add((ItemType)option);
        }

        decimal totalCost = CalculateTotal(cart, inventory, offers);
        Console.WriteLine(cart.Count == 0 ?
                        "Your basket is empty. Total Cost: 0p"
                        : $"Total Cost: {totalCost}p");
    }

    internal static decimal CalculateTotal(
    List<ItemType> cart,
    Dictionary<ItemType, decimal> inventory,
    Dictionary<ItemType, OfferDetails> offers)
    {
        var counts = new Dictionary<ItemType, int>();
        foreach (var item in cart)
            counts[item] = counts.GetValueOrDefault(item) + 1;

        decimal total = 0m;
        foreach (var (item, count) in counts)
        {
            bool hasPrice = inventory.TryGetValue(item, out decimal price);
            if (!hasPrice)
                throw new InvalidOperationException($"No price configured for {item}.");

            bool hasOffer = offers.TryGetValue(item, out var offer);
            if (hasOffer && offer is not null)
            {
                int groups = count / offer.Quantity;
                int remainder = count % offer.Quantity;
                total += (groups * offer.PaidQuantity + remainder) * price;
            }
            else
            {
                total += count * price;
            }
        }
        return total;
    }
}


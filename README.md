# ShopperStop

A C# console app that works out the total price of a shopping basket.

Prices:

- Apple – 35p
- Banana – 20p
- Melon – 50p, buy one get one free
- Lime – 15p, three for the price of two

## Running it

```
cd ShopperStop
dotnet run
```

Pick items by typing the number next to them. Type 5 when you're done and it prints the total in pence.

## Running the tests

```
cd ..
dotnet test ShopperStop.Tests
```

The test cases are listed in `ShopperStop.Tests/TestCases.md`.

## How I approached it

I split the problem into two parts: collecting the items, and pricing them. The pricing is the part that matters, so I kept it in its own method (`CalculateTotal`) that takes the basket, the price list and the offers. That way it doesn't depend on the console at all, and the tests can call it directly.

Pricing works like this:

1. Count how many of each item are in the basket. The order they were added in doesn't matter.
2. For each item, look up its price.
3. If the item has no offer, it's just count × price.
4. If it has an offer, split the count into full offer groups and leftovers. Each full group is charged for the "paid" quantity, and leftovers are charged at the normal price.

For example, 7 limes on 3-for-2 is 2 full groups (pay for 4) plus 1 leftover, so 5 × 15p = 75p.

Both offers are really the same rule – "buy X, pay for Y". Buy one get one free is 2 for 1, and three for two is 3 for 2. So instead of writing a special case for each fruit, an offer is stored as two numbers. Adding a new offer, like 4 for 3 on bananas, is one line in the offers list.

Prices are kept in pence as `decimal` to avoid rounding issues.

## Checks

- Invalid menu input (letters, empty, out of range) asks again instead of crashing.
- An item with no price throws an error rather than being treated as free.
- An offer with a quantity of 0 is rejected when it's created, since it would cause a divide by zero.

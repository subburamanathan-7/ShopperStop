# ShopperStop – Test Cases

Given Constraint
Prices: Apple 35p, Banana 20p, Melon 50p (buy one get one free), Lime 15p (three for the price of two).

| Group | Basket | Expected |
|---|---|---|
| Empty | `[]` | 0 |
| No offer | Apple | 35 |
| No offer | Banana | 20 |
| Spec example | Apple, Apple, Banana | 90 |
| Melon (buy one get one free) | 1 / 2 / 3 / 4 Melons | 50 / 50 / 100 / 100 |
| Lime (3 for 2) | 1 / 2 / 3 / 4 / 6 / 7 Limes | 15 / 30 / 30 / 45 / 60 / 75 |
| Mixed | Apple, Banana, 2 Melon, 3 Lime | 135 |
| Order doesn't matter | same items, shuffled | same total |
| Bad offer | `new OfferDetails(0, 1)` | throws `ArgumentOutOfRangeException` |

## Running

```powershell
cd ShopperStop
dotnet test ShopperStop.Tests
```

const long start = 0;
const long maxCount = 100000;

int silverFirst = 0;
int twoGold = 0;
int goldSilver = 0;

Parallel.For(start, maxCount, i =>
{
    Console.WriteLine($"Draw {i}: SF = {silverFirst}, GG={twoGold}, GS={goldSilver}.");
    // We fill the box with either two gold or one gold and one silver.
    List<Coin> box = Helper.Choose();
    Console.WriteLine($"Box {i}: {box[0]} and {box[1]}.");
    // We now pick one of the coins and remove it.
    var pick = Helper.Rnd.Next(0, box.Count);
    var coin1 = box[pick];
    box.RemoveAt(pick);
    // We now pick the other coin.
    var coin2 = box.First();
    if (coin1 == Coin.Silver)
    {
        // If the first coin is silver, we increment the silverFirst counter. 
        Interlocked.Increment(ref silverFirst);
    }
    else if (coin2 == Coin.Gold)
    {
        // The first is gold. If the second coin is gold, we increment the twoGold counter.
        Interlocked.Increment(ref twoGold);
    }
    else
    {
        // The first is gold and the second coin is silver, so we increment the goldSilver counter.
        Interlocked.Increment(ref goldSilver);
    }
});

Console.WriteLine($"We have drawn silver first {silverFirst} times.");
Console.WriteLine($"We have drawn two gold {twoGold} times.");
Console.WriteLine($"We have drawn one gold and one silver {goldSilver} times.");

public enum Coin { Gold = 1, Silver = 2 }

public static class Helper
{
    public static readonly Random Rnd = new Random();
    private static Coin GetCoin(int value) => value == 1 ? Coin.Gold : Coin.Silver;
    private static Coin RandomCoin() => GetCoin(Rnd.Next(0, 2)); private static List<Coin> MakeBox() => [RandomCoin(), RandomCoin()];
    private static bool TwoSilver(this List<Coin> box) => box[0] == box[1] && box[0] == Coin.Silver;

    public static List<Coin> Choose()
    {
        List<Coin> result;
        while ((result = MakeBox()).TwoSilver()) { };
        return result;
    }
}
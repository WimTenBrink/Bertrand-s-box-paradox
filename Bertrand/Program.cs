const long start = 0;
const long maxCount = 1000000;
Random rnd = new Random();

int silverFirst = 0;
int twoGold = 0;
int goldSilver = 0;

Parallel.For(start, maxCount, i =>
{
    Console.WriteLine($"Draw {i}: SF = {silverFirst}, GG={twoGold}, GS={goldSilver}.");
    // We create two boxes, one with two gold coins and the other with one gold and one silver coin.
    List<List<Coin>> boxes = [[Coin.Gold, Coin.Gold], [Coin.Gold, Coin.Silver]];
    // We now pick a box.
    var pickBox = rnd.Next(0, boxes.Count);
    List<Coin> box = boxes[pickBox];
    // Show the box.
    Console.WriteLine($"Box {i}: {box[0]} and {box[1]}.");
    // We now pick one of the coins and remove it.
    var pick = rnd.Next(0, box.Count);
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
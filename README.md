This simple project shows a variation of the [Bertrand's box paradox]([url](https://en.wikipedia.org/wiki/Bertrand%27s_box_paradox)) where you only have two boxes. See  for details.

The principle is simple. In the normal situation, you would have three boxers, each with two coins. Each coin is either gold or silver. 
You draw a coin and then check the chance of the other coin being the same color. This would normally be a 2 in 3 chance.

But this silulation uses a difference. One coin is always gold and the other is either silver or gold. And here things are a bit different...

We start with defining a coin: `public enum Coin { Gold = 1, Silver = 2 }`

So we create two boxes. One with two gold and one with gold and silver: `List<List<Coin>> boxes = [[Coin.Gold, Coin.Gold], [Coin.Gold, Coin.Silver]];`

We now pick one of these boxes: `var pickBox = rnd.Next(0, boxes.Count); List<Coin> box = boxes[pickBox];`

We now remove a coin from the box: `var pick = rnd.Next(0, box.Count); var coin1 = box[pick]; box.RemoveAt(pick);`

We then get the second coin: `var coin2 = box.First();`

We now have three options: 
* First is silver and second is gold.
* First is gold and second is silver.
* Both are gold.

Now, if we draw silver first then we know the second draw can't be silver. It must be gold. So we ignore this outcome.

If we draw gold first then we would expect there's a 50% chance that the next one will also be gold. 
This is what the simulation is going to prove by maintaining counters while drawing a million times.
This is a slow process so we use the Parallel.For() method to use maximum processing power.
The expectation is that we have about 250,000 times where we get silver first. And 375,000 times of two gold and the same for gold and silver.
So, what does the simulation say?
`We have drawn silver first 249946 times.`
`We have drawn two gold 499845 times.`
`We have drawn one gold and one silver 250209 times.`

That's surprising, isn't it? But why is this the case? Well, that's the Bertrand's box paradox. :) 

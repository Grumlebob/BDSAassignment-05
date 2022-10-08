using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Program
    {
        public IList<Item> Items;

        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 10,
                        Quality = 49
                    },
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 5,
                        Quality = 49
                    },
                    //works
                    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };

            for (var i = 0; i < 31; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }

                Console.WriteLine("");
                app.UpdateQuality();
            }
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                switch (Items[i].Name)
                {
                    case "Aged Brie":
                        HandleAgedBrie(Items[i]);
                        break;
                    case "Backstage passes to a TAFKAL80ETC concert":
                        HandleBackstagePasses(Items[i]);
                        break;
                    case "Sulfuras, Hand of Ragnaros":
                        break;
                    case "Conjured Mana Cake":
                        HandleConjuredItems(Items[i]);
                        break;
                    default:
                        HandleDefaultItem(Items[i]);
                        break;
                }
            }
        }

        private void HandleDefaultItem(Item item)
        {
            if (item.Quality > 0) item.Quality--;
            if (item.SellIn <= 0 && item.Quality > 0)  item.Quality--;

            item.SellIn--;
        }
        
        
        private void HandleConjuredItems(Item item)
        {
            if (item.Quality > 0) item.Quality -= 2;
            if (item.SellIn <= 0 && item.Quality > 0)  item.Quality -= 2;

            item.SellIn--;
        }
        
        private void HandleAgedBrie(Item i)
        {
            if(i.Quality < 50 && i.SellIn > 0)         i.Quality++;
            else if (i.SellIn <= 0 && i.Quality + 2 >= 50) i.Quality = 50;
            else if(i.Quality < 49 && i.SellIn <= 0)   i.Quality += 2;

            i.SellIn--;
        }
        
        private void HandleBackstagePasses(Item i)
        {
            if(i.SellIn > 10)       i.Quality++;
            else if(i.SellIn > 5)   i.Quality += 2;
            else if(i.SellIn > 0)   i.Quality += 3;
            else                    i.Quality = 0;
        }


    }
    
    public class Item
    {
        public string Name { get; set; }

        //Number of days to sell-in
        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}
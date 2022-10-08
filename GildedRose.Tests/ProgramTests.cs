namespace GildedRose.Tests;

public class ProgramTests
{
    private Program p;
    public ProgramTests()
    {
        p = new Program();
        p.Items = new List<Item>();
    }
    
    
    [Fact]
    public void QualityDegradesTwiceAsFastWhenPassedSellBy()
    {
        //Once the sell by date has passed, Quality degrades twice as fast
        var item = new Item { Name = "Sword", SellIn = 0, Quality = 10 };
        
        p.Items.Add(item);
        p.UpdateQuality();
        
        item.Quality.Should().Be(8);
    }
    
    [Fact]
    public void QualityIsNeverNegative()
    {
        //The Quality of an item is never negative
        p.Items.Add(new Item { Name = "Sword", SellIn = 0, Quality = 0 });
        p.UpdateQuality();
        p.UpdateQuality();
        p.UpdateQuality();
        p.UpdateQuality();
        
        p.Items[0].Quality.Should().Be(0);
        
    }
    
    [Fact]
    public void AgedBrieIncreasesInQuality()
    {
        //“Aged Brie” actually increases in Quality the older it gets
        p.Items.Add(new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 });
        p.UpdateQuality();
        p.Items[0].Quality.Should().Be(1);
        p.UpdateQuality();
        p.Items[0].Quality.Should().Be(2);
        //0 >= item.quality
        p.UpdateQuality();
        p.Items[0].Quality.Should().Be(4);
    }
    
    [Fact]
    public void QualityIsNeverMoreThan50()
    {
        //The Quality of an item is never more than 50
        p.Items.Add(new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 });
        p.UpdateQuality();
        p.Items[0].Quality.Should().Be(50);
        p.UpdateQuality();
        p.Items[0].Quality.Should().Be(50);
    }
    
    [Fact]
    public void SulfurasNeverHasToBeSoldOrDecreasesInQuality()
    {
        //“Sulfuras”, being a legendary item, never has to be sold or decreases in Quality
        var sulf = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
        p.Items.Add(sulf);
        p.UpdateQuality();
        p.UpdateQuality();
        
        p.Items[0].Quality.Should().Be(80);
        p.Items[0].SellIn.Should().Be(0);
    }
    
    [Fact]
    public void BackstagePassesIncreaseInQualityAsSellInApproaches()
    {
        //“Backstage passes”, like aged brie, increases in Quality as its SellIn value approaches;
        var pass1 = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 };
        var pass2 = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 };
        p.Items.Add(pass1);
        p.Items.Add(pass2);
        p.UpdateQuality();
        
        p.Items[0].Quality.Should().Be(22);
        p.Items[1].Quality.Should().Be(23);
    }
    
    [Fact]
    public void BackstagePassesQualityDropsToZeroAfterConcert()
    {
        //Quality drops to 0 after the concert
        var pass = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 };
        p.Items.Add(pass);
        p.UpdateQuality();

        p.Items[0].Quality.Should().Be(0);
    }
    
    [Fact]
    public void ConjuredItemsDegradeTwiceAsFast()
    {
    // "Conjured" items degrade in Quality twice as fast as normal items
        var conjured = new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 20 };
        var conjured2 = new Item { Name = "Conjured Mana Cake", SellIn = 1, Quality = 20 };
        p.Items.Add(conjured);
        p.Items.Add(conjured2);
        p.UpdateQuality();
        p.Items[0].Quality.Should().Be(18);
        p.Items[1].Quality.Should().Be(18);
        p.UpdateQuality();
        p.Items[0].Quality.Should().Be(16);
        p.Items[1].Quality.Should().Be(14);
        
    }
    
    [Fact]
    public void MainMethodTest()
    {
        Program.Main(new string[0] { });

        true.Should().BeTrue();
    }
    
}
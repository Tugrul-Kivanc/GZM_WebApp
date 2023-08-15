using DatabaseModel.Models;
using Microsoft.EntityFrameworkCore;

var context = new GzmdatabaseContext();

#region Add Categories

//context.Categories.Add(new Category() { Name = "Muadil Siyah", Price = 180 });
//context.Categories.Add(new Category() { Name = "Muadil Turuncu", Price = 120 });
//context.Categories.Add(new Category() { Name = "Tabula Rasa", Price = 400 });
//context.Categories.Add(new Category() { Name = "Baracco", Price = 700 });
//context.Categories.Add(new Category() { Name = "Vücut Spreyi", Price = 120 });
//context.Categories.Add(new Category() { Name = "Pati Parfüm", Price = 150 });
//context.Categories.Add(new Category() { Name = "Mum", Price = 140 });
//context.Categories.Add(new Category() { Name = "Kolonya", Price = 90 });
//context.Categories.Add(new Category() { Name = "Kumaş Sprey", Price = 120 });
//context.Categories.Add(new Category() { Name = "Oto Kokusu Askı", Price = 50 });
//context.Categories.Add(new Category() { Name = "Oto Kokusu Sprey", Price = 70 });
//context.Categories.Add(new Category() { Name = "Oda Kokusu 100ml", Price = 120 });
//context.Categories.Add(new Category() { Name = "Oda Kokusu 120ml", Price = 180 });
//context.Categories.Add(new Category() { Name = "Oda Kokusu Tabula Rasa", Price = 350 });
//context.SaveChanges();

#endregion

#region Add Perfumes as Products
/*
var query = context.Perfumes.ToList();

List<Product> products = new List<Product>();

foreach (var item in query)
{
    context.Products.Add(new Product()
    {
        Name = item.Code,
        Price = 180,
        Stock = 0,
        TotalSales = 0,
    });
}

var result = context.SaveChanges();
Console.WriteLine(result);
*/
#endregion

#region Link PerfumeId and ProductId in PerfumeProduct
/*
//PerfumeIds and ProductIds were the same on initial db creation

var query = context.Products.Select(a => a.ProductId).ToList();

var perfumeProduct = new PerfumeProduct();

foreach (var item in query)
{
    context.PerfumeProducts.Add(new PerfumeProduct()
    {
        PerfumeId = item,
        ProductId = item
    });
}

var result = context.SaveChanges();
Console.WriteLine(result);
*/
#endregion

#region Change Prices on Specific Items
/*
var turuncuParfumler = context.Products.Where(a => a.Name.EndsWith("T")).ToList();
turuncuParfumler.ForEach(a =>
{
    a.Price = 120;
    a.Type = "Parfüm Turuncu";
});

var tabulaParfumler = context.Products.Where(a => a.Name.StartsWith("TR")).ToList();
tabulaParfumler.ForEach(a =>
{
    a.Price = 400;
    a.Type = "Parfüm Niş";
});

var baraccoParfumler = context.Products.Where(a => a.Name.StartsWith("BR")).ToList();
baraccoParfumler.ForEach(a =>
{
    a.Price = 700;
    a.Type = "Parfüm Baracco";
});

var siyahParfumler = context.Products.Where(a => a.Price == 180).ToList();
siyahParfumler.ForEach(a =>
{
    a.Type = "Parfüm Siyah";
});

var result = context.SaveChanges();
Console.WriteLine(result);
*/
#endregion

#region Transfer Products from old Database

//var oldContext = new GzmwebAppDbContext();

//var siyahParfumler = oldContext.Products.Where(p => p.Price == 180);
//var turuncuParfumler = oldContext.Products.Where(p => p.Price == 120);
//var tabulaParfumler = oldContext.Products.Where(p => p.Price == 400);
//var baraccoParfumler = oldContext.Products.Where(p => p.Price == 700);

//foreach(var item in siyahParfumler)
//{
//    context.Products.Add(new DatabaseModel.Models.Product()
//    {
//        Name = item.Name,
//        Stock = 0,
//        TotalSales = 0,
//        CategoryId = 1,
//    });
//}
//foreach (var item in turuncuParfumler)
//{
//    context.Products.Add(new DatabaseModel.Models.Product()
//    {
//        Name = item.Name,
//        Stock = 0,
//        TotalSales = 0,
//        CategoryId = 2,
//    });
//}
//foreach (var item in tabulaParfumler)
//{
//    context.Products.Add(new DatabaseModel.Models.Product()
//    {
//        Name = item.Name,
//        Stock = 0,
//        TotalSales = 0,
//        CategoryId = 3,
//    });
//}
//foreach (var item in baraccoParfumler)
//{
//    context.Products.Add(new DatabaseModel.Models.Product()
//    {
//        Name = item.Name,
//        Stock = 0,
//        TotalSales = 0,
//        CategoryId = 4,
//    });
//}

//context.SaveChanges();

#endregion

#region Transfer Perfumes from old Database

//var oldContext = new GzmwebAppDbContext();

//foreach (var item in oldContext.Perfumes)
//{
//    context.Perfumes.Add(new DatabaseModel.Models.Perfume()
//    {
//        ProductId = item.PerfumeId,
//        Code = item.Code,
//        Brand = item.Brand,
//        Type = item.Type,
//        Smell = item.Smell,
//        Gender = item.Gender,
//        Sillage = item.Sillage,
//        Info = item.Description,
//        Weather = item.Weather,
//        Link = item.Link,
//    });
//}

//context.SaveChanges();

#endregion

#region Fix PerfumeId on Product Table

//foreach(var item in context.Products)
//{
//    item.PerfumeId = item.ProductId;
//}

//context.SaveChanges();

#endregion


Console.WriteLine("End of Execution");
Console.ReadKey();
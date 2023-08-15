using DatabaseModel.Models;
using Microsoft.EntityFrameworkCore;

var context = new GzmdatabaseContext();

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



Console.ReadKey();
using GraphQL.Types;

namespace testGQL.BL
{
    public class Item
    {
        public string Barcode { get; set; }
        public string Title { get; set; }
        public decimal SellingPrice { get; set; }
    }

    public class ItemType : ObjectGraphType<Item>
    {
        public ItemType()
        {
            // no more type declaration as follows: Field<T>
            // instead just Field
            Field(i => i.Barcode);
            Field(i => i.Title);
            Field(i => i.SellingPrice);
        }
    }
}
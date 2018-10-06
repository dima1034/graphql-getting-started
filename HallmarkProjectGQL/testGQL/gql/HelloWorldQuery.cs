using GraphQL.Types;
using testGQL.BL;

namespace testGQL
{
    //Fields are defined in the constructor function of the query class.
    public class HelloWorldQuery : ObjectGraphType
    {
        public HelloWorldQuery()
        {
//            Field<StringGraphType>(
//                name: "hello",
//                resolve: context => "fuckoff"
//            );

            Field<ItemType>(
                name: "itemtype", 
                //There could be a list of arguments; some required and some optional.
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>{  Name = "barcode" }),
                resolve: context =>
                {

                    var barcode = context.GetArgument<string>("barcode");
                    
                    return new Item
                    {
                        Barcode = barcode,
                        Title = "Headphone",
                        SellingPrice = 12.59m
                    };
                });
            
//            Field<ItemType>(
//                name: "itemtype2", 
//                resolve: context =>
//                {
//                    return new Item
//                    {
//                        Barcode = "1",
//                        Title = "Headphone",
//                        SellingPrice = 12.59m
//                    };
//                });
        }

    }
    
    
}
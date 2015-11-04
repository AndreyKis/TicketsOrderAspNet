using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;

namespace AccesToTicketsDB
{
    
    public partial class AccessToTicketsDB
    {
        //Вытягиваем из БД по-сложному, с помощью провайдера.
        public List<Common.Price> GetAllPrices()
        {
            if (dataBase.Price == null)
                return null;
            List<Common.Price> prices = new List<Common.Price>();
            foreach (var currPrice in dataBase.Price)
            {
                Common.Price price = new Common.Price();
                price.ID = currPrice.pprice_id;
                price.Value = currPrice.price_name;
                prices.Add(price);
            }
            return prices;
        }

        public bool AddPrice(Common.Price price)
        {
            bool canAddPrice = IsUniquePrice(price);
            if (canAddPrice)
            {
                Price priceToAdd = new Price();
                priceToAdd.price_name = price.Value;
                dataBase.Price.Add(priceToAdd);
                dataBase.SaveChanges();
                return true;
            }
            return false;
        }

        bool IsUniquePrice(Common.Price price)
        {
            var priceUsedInPrice = from c in dataBase.Price where c.price_name == price.Value select c;
            if (priceUsedInPrice.Count() > 0)
                return false;
            return true;
        }

        public bool DeletePrice(Common.Price price)
        {
            bool canDeletePrice = IsUsedPrice(price);
            if (canDeletePrice)
            {
                var priceToDelete = (from c in dataBase.Price where c.price_name == price.Value select c).FirstOrDefault();
                dataBase.Price.Remove(priceToDelete);
                dataBase.SaveChanges();
                return true;
            }
            return false;
        }

        public int GetPriceIDByName (string Name)
        {
            int priceValue = Int32.Parse(Name);
            var searched = (from c in dataBase.Price where c.price_name == priceValue select c).First();
            return searched.pprice_id;
        }

        public Common.Price GetPriceByID(int id)
        {
            var searched = (from c in dataBase.Price where c.pprice_id == id select c).First();
            Common.Price pr = new Common.Price(searched.pprice_id, searched.price_name);
            return pr;

        }

        bool IsUsedPrice (Common.Price price)
        {
            var priceUsedIntickets = from c in dataBase.Ticket where c.Price.price_name == price.Value select c;
            if (priceUsedIntickets.Count() > 0)
                return false;
            return true;
        }

        /*Пример как пользовать свой собственный адаптер
        public Price getPriceByID(int id)
        {

            Price tmp = null;

            var res = priceOneAdapter.GetData(id);

            if (res == null) return tmp;

            foreach (var r in res)
            {

                tmp = new Price(r.price_name, r.price_enter_date);

            }

            return tmp;
        }*/

    }
}


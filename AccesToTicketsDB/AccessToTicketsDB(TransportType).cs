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
        public List<Common.TransportType> GetAllTransportTypes()
        {
            if (dataBase.Type == null)
                return null;
            List<Common.TransportType> types = new List<Common.TransportType>();
            foreach (var currType in dataBase.Type)
            {
                Common.TransportType type = new Common.TransportType();
                type.Name = currType.ttype_name;
                type.ID = currType.ttype_id;
                types.Add(type);
            }
            return types;
        }

        public int GetTypeIDByName(string Name)
        {
            var searched = (from c in dataBase.Type where c.ttype_name == Name select c).First();
            return searched.ttype_id;
        }

        public Common.TransportType GetTypeByID(int id)
        {
            var searched = (from c in dataBase.Type where c.ttype_id == id select c).First();
            Common.TransportType type = new Common.TransportType(searched.ttype_id, searched.ttype_name);
            return type;

        }
        public bool AddType(Common.TransportType type)
        {
            bool canAddType = IsUniqueType(type);
            if (canAddType)
            {
                Type typeDB = new Type();
                typeDB.ttype_name = type.Name;
                dataBase.Type.Add(typeDB);
                dataBase.SaveChanges();
                return true;
            }
            return false;
        }

        bool IsUniqueType(Common.TransportType type)
        {
            var typeRows = from c in dataBase.Type where c.ttype_name == type.Name select c;
            if (typeRows.Count() > 0)
                return false;
            return true;
        }

        public bool DeleteType(Common.TransportType type)
        {
            bool canDeleteType = IsUsedType(type);
            if (canDeleteType)
            {
                var typeToDelete = (from c in dataBase.Type where c.ttype_name == type.Name select c).FirstOrDefault();
                dataBase.Type.Remove(typeToDelete);
                dataBase.SaveChanges();
                return true;
            }
            return false;
        }

        bool IsUsedType(Common.TransportType type)
        {
            var typesUsedInTicketRows = from c in dataBase.Ticket where c.Type.ttype_name == type.Name select c;
            if (typesUsedInTicketRows.Count() > 0)
                return false;
            return true;
        }
    }
}
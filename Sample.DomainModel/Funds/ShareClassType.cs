using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.DomainModel.Funds
{
    public class ShareClassType
    {
        public enum TypeName
        {
            Open,
            Closed
        };

        public ShareClassType(TypeName type)
        {
            this.Name = type;
        }

        public static ShareClassType CreateFromString(string typeName)
        {
            TypeName name = (TypeName)Enum.Parse(typeof(TypeName), typeName);
            return new ShareClassType(name);
        }

        public TypeName Name { get; private set; }

        public bool IsLinkable
        {
            get
            {
                return this.Name == TypeName.Open;
            }
        }
    }
}

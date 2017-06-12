using System;
using System.Collections.Generic;
using System.Text;

namespace HsServiceCore
{
    class UrlFavourite
    {
        string _name;
        string _favUrl;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string FavName
        {
            get { return _favUrl; }
            set { _favUrl = value; }
        }
    }
}

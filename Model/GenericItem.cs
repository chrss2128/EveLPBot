using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    //TODO: This may end up being an interface
    class GenericItem
    {
        public long id { get; set; }

        public string name { get; set; }

        public GenericItem() //no arg constructor for serialization
        {

        }
    }
}

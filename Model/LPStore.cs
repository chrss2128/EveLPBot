using System;
using System.Collections.Generic;
using System.Text;

namespace EveLPBot.Model
{
    public class LPStore
    {
        public long npcCorporationId { get; }

        public string npcCorporationName { get; }

        public List<LPStoreItem> lpStoreItems { get; }

        public LPStore(long npcCorporationId, string npcCorporationName, List<LPStoreItem> lpStoreItems)
        {
            this.npcCorporationId = npcCorporationId;
            this.npcCorporationName = npcCorporationName;
            this.lpStoreItems = lpStoreItems;
        }
    }
}

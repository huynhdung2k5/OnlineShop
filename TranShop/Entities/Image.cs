using System;
using System.Collections.Generic;

#nullable disable

namespace TranShop.Entities
{
    public partial class Image
    {
        public long Id { get; set; }
        public byte[] Image1 { get; set; }
        public long? IdAlbum { get; set; }

        public virtual Album IdAlbumNavigation { get; set; }
    }
}

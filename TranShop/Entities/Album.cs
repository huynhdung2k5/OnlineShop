using System;
using System.Collections.Generic;

#nullable disable

namespace TranShop.Entities
{
    public partial class Album
    {
        public Album()
        {
            Images = new HashSet<Image>();
        }

        public long Id { get; set; }
        public string AlbumName { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}

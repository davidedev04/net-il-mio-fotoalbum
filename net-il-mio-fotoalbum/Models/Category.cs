﻿namespace net_il_mio_fotoalbum.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Photo> Photos { get; set; }

        public Category() { }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int InStock { get; set; }
    }
}

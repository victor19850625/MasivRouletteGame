﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasivRoulette.Entities.BindingModels
{
    public class ResponseServiceModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }
    }
}

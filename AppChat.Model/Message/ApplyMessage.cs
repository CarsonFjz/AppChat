﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Model.Message
{
   public class ApplyMessage
    {
        public Guid applyid { get; set; }
        public int userid { get; set; }
        public string applyname { get; set; }
        public string applyavatar { get; set; }
        public int applyim { get; set; }
        public string msg { get; set; }
        public string targetid { get; set; }
        public string addtime { get; set; }
        public int result { get; set; }
        public int applytype { get; set; }
        public bool isself { get; set; }

    }
}

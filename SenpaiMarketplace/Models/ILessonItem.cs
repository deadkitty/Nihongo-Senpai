﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiMarketplace.Models
{
    public interface ILessonItem
    {
        String ToCreateNewString();
        void Fill(String properties);
    }
}

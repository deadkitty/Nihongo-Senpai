﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiCreationKit.Utilities
{
    public class AppStreamWriter : StreamWriter
    {
        private int currentLine = 0;

        public int CurrentLine
        {
            get { return currentLine; }
        }

        public AppStreamWriter(Stream stream)
            : base(stream)
        {

        }

        public AppStreamWriter(String file)
            : base(file)
        {

        }

        public override void WriteLine()
        {
            ++currentLine;
            base.WriteLine();
        }
    }
}
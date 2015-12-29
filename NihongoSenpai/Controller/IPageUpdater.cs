using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NihongoSenpai.Controller
{
    public interface IPageUpdater
    {
        void UpdatePage();
        void RoundFinished();
        void EndPractice();
    }
}

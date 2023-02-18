using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Views
{
    public class FlyoutControlFlyoutMenuItem
    {
        public FlyoutControlFlyoutMenuItem()
        {
            TargetType = typeof(FlyoutControlFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
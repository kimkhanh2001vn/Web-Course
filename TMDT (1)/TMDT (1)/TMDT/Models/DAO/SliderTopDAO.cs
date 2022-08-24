using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMDT.Models.DAO
{
    public class SliderTopDAO
    {
        private TapChiDB db = new TapChiDB();
        public IEnumerable<SliderTop> getSliderImages()
        {
            return db.SliderTops.Where(x => x.Status == true).ToList();
        }
    }
}
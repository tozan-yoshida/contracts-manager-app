using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts_manager_app
{
    public class PageButton : Button
    {
        public InquiryScreen inquiryScreen { get; set; }

        public int pageNumber { get; set; }

        // ボタンへのイベントをセットする
        public void eventMaking()
        {
            this.Click += new EventHandler(doClickEvent);
        }

        // ボタンへのイベントを解除する
        public void eventSuspend()
        {
            this.Click -= new EventHandler(doClickEvent);
        }

        // クリックイベントの実態
        public void doClickEvent(object sender, EventArgs e)
        {
            inquiryScreen.currentPageNumber = pageNumber;
            inquiryScreen.ScreenDisplay();
        }
    }
}

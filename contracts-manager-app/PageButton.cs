using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contracts_manager_app
{
    public class PageButton : Button
    {
        int pageNumber;

        // ボタンへのイベントをセットする
        public void eventMaking(int pageNumber)
        {
            this.pageNumber = pageNumber;
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
            MessageBox.Show($"{pageNumber}");
        }
    }
}

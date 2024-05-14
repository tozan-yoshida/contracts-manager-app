using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace contracts_manager_app
{

    /// <summary>
    /// 連絡先の属性を持つクラス
    /// </summary>
    public class Contact
    {
        public string id {  get; set; }
        public string name { get; set; }
        public string tel {  get; set; }
        public string address {  get; set; }
        public string remark {  get; set; }
        public string imagePass {  get; set; }

        public Contact(string id, string name, string tel, string address, string remark)
        {
            this.id = id;
            this.name = name;
            this.tel = tel;
            this.address = address;
            this.remark = remark;
            imagePass = "";
        }

        public Contact(string id, string name, string tel, string address, string remark, string imagePass)
        {
            this.id = id;
            this.name = name;
            this.tel = tel;
            this.address = address;
            this.remark = remark;
            this.imagePass = imagePass;
        }

    }
}
